﻿using GameGenesisApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace GameGenesisApp.Services
{
    public class ApiServices
    {
        HttpClientHandler insecureHandler = GetInsecureHandler();
        static string url = "https://localhost:7084/";
        private HttpClient client = new HttpClient();

        public ApiServices()
        {
            client = new HttpClient(insecureHandler);
        }

        private static HttpClientHandler GetInsecureHandler()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                return true;
            };
            return handler;
        }
        private Response ValidatePassword(string password)
        {
            string specialCh = @"%!@#$%^&*()?/>.<,:;'\|}]{[_~`+=-" + "\"";
            char[] specialChars = specialCh.ToCharArray();
            if (password.Length > 7 && password.Length < 15)
            {
                if (password.Any(char.IsUpper))
                {
                    if (!password.Contains(" "))
                    {
                        if (password.Any(char.IsLower))
                        {
                            foreach (char c in specialChars)
                            {
                                if (password.Contains(c))
                                {
                                    Response responseSuccess = new Response
                                    {
                                        success = true,
                                        message = "Validation success !"
                                    };
                                    return responseSuccess;
                                }
                            }
                        }
                    }
                }

            }
            Response responseFail = new Response
            {
                success = false,
                message = "Password must be between 8 and 14 characters, must contains; - at least 1 upper case, - at least 1 lower case, - at least 1 special characters, - and No white space !"
            };
            return responseFail;
        }

        public async Task<Response> RegisterAsync(string email, string password, string passwordTwo, DateTime birthdate)
        {
            //Debug.WriteLine(password + " " + passwordTwo);
            if (password == passwordTwo)
            {
                var success = ValidatePassword(password);
                if (true)//success.Success) // FOR DEV ONLY MAKE SURE TO CHANGE IT WHEN DONE !!!!
                {
                    var user = new User
                    {
                        email = email,
                        password = password,
                        birthdate = birthdate
                    };

                    var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

                    var responseJson = await client.PostAsync(url + "Auth/Register", content);
                    var data = await responseJson.Content.ReadAsStringAsync();

                    var response = JsonConvert.DeserializeObject<Response>(data);
                    return response;
                }
                else
                {
                    return success;
                }
            }
            else
            {
                Response response = new Response
                {
                    success = false,
                    message = "Passwords do not match !"
                };
                return response;
            }
        }

        public async Task<Response> LoginAsync(string email, string password)
        {
            var user = new User
            {
                email = email,
                password = password
            };

            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url + "Auth/Login", content);

            var data = await response.Content.ReadAsStringAsync();

            var token = JsonConvert.DeserializeObject<Response>(data);

            if (!token.success)
            {
                return token;
            }

            Debug.WriteLine(token.Data + "/////////////////////////////////////////////////////// ICI ");

            await SecureStorage.SetAsync("jwt_token", token.Data);

            return token;
        }
    }
}