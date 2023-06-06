using GameGenesisApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using System.Text.RegularExpressions;

namespace GameGenesisApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductCategory>().HasData(
                new ProductCategory { Id = 1, Name = "Action ", Description = "Les jeux d'action mettent l'accent sur les défis physiques, y compris la coordination œil-main et les temps de réaction." },
                new ProductCategory { Id = 2, Name = "Aventure ", Description = "Ces jeux mettent souvent l'accent sur l'exploration, la résolution de problèmes, l'interaction avec l'environnement et les personnages non joueurs." },
                new ProductCategory { Id = 3,Name = "RPG (Role-Playing Games) ", Description = " Dans ces jeux, les joueurs assument le rôle d'un personnage, souvent dans un cadre fantastique, et prennent des décisions qui affectent le déroulement de l'histoire." },
                new ProductCategory { Id = 4, Name = "Simulation ", Description = "Ces jeux tentent de simuler la réalité. Cela peut aller de la gestion d'une ferme à la conduite d'un avion." },
                new ProductCategory { Id = 5, Name = "Stratégie ", Description = "Ces jeux exigent que le joueur pense et planifie stratégiquement pour atteindre la victoire. Ils peuvent être en temps réel ou tour par tour." },
                new ProductCategory { Id = 6, Name = "Puzzle ", Description = "Ces jeux mettent l'accent sur la résolution de problèmes." },
                new ProductCategory { Id = 7, Name = "Sport ", Description = "Ces jeux simulent des sports réels comme le football, le basket-ball, etc. " },
                new ProductCategory { Id = 8, Name = "Course ", Description = " Ces jeux impliquent généralement des courses de véhicules contre des adversaires ou contre le temps." },
                new ProductCategory { Id = 9, Name = "Horreur ", Description = "Ces jeux mettent l'accent sur la survie du joueur dans un environnement effrayant, généralement avec des ressources limitées." },
                new ProductCategory { Id = 10, Name = "MOBA ", Description = "Dans ces jeux, deux équipes s'affrontent dans une arène. Chaque joueur contrôle un seul personnage avec des compétences uniques. " },
                new ProductCategory { Id = 11, Name = "MMORPG", Description = "Ce sont des RPG joués en ligne avec un grand nombre de personnes. " },
                new ProductCategory { Id = 12, Name = "Battle Royale ", Description = " Il s'agit d'un genre de jeu en ligne où un grand nombre de joueurs se bat pour être le dernier survivant." },
                new ProductCategory { Id = 13, Name = "Plateforme ", Description = "Ces jeux impliquent principalement de naviguer le personnage du joueur à travers divers obstacles." },
                new ProductCategory { Id = 14, Name = "Roguelike ", Description = "Ce sont des jeux généralement caractérisés par des niveaux générés aléatoirement et une mort permanente. " },
                new ProductCategory { Id = 15, Name = "FPS", Description = "Ce sont des jeux d'action vus à la première personne où le joueur utilise des armes à feu et combat des ennemis." },
                new ProductCategory { Id = 16, Name = "TPS ", Description = "Semblables aux FPS, mais vus à la troisième personne, ce qui donne au joueur une vision plus large de l'environnement du jeu." },
                new ProductCategory { Id = 17, Name = "Beat 'em up / Brawler", Description = "Dans ces jeux, un personnage se bat contre un grand nombre d'ennemis. Ces jeux sont souvent coopératifs." },
                new ProductCategory { Id = 18, Name = "Stealth ", Description = "Ces jeux encouragent le joueur à éviter les ennemis plutôt que de les affronter directement." },
                new ProductCategory { Id = 19, Name = "Survival ", Description = "Ce genre de jeux met l'accent sur la survie. Le joueur commence généralement avec un minimum de ressources et doit collecter des ressources et des objets tout en évitant, ou en confrontant, les menaces." },
                new ProductCategory { Id = 20, Name = "Idle / Clicker / Incremental ", Description = "Ce sont des jeux qui impliquent des actions simples et répétitives, comme cliquer sur l'écran, pour gagner des points et progresser dans le jeu. " },
                new ProductCategory { Id = 21, Name = "Rythme ", Description = "Ces jeux mettent l'accent sur la musique et exigent que les joueurs appuient sur des boutons en rythme avec la musique." },
                new ProductCategory { Id = 22, Name = "Visual Novel", Description = " Il s'agit d'un genre de jeu narratif, généralement basé sur du texte, où les joueurs lisent une histoire et font parfois des choix qui affectent le déroulement de l'histoire." },
                new ProductCategory { Id = 23, Name = "Sandbox ", Description = "Ces jeux permettent aux joueurs d'interagir avec l'environnement du jeu de manière créative et sans objectif précis." },
                new ProductCategory { Id = 24, Name = "Tower Defense", Description = " Dans ces jeux, les joueurs placent des tours ou des unités défensives pour arrêter les vagues d'ennemis qui avancent sur un chemin prédéfini." }
                );

            modelBuilder.Entity<Shop>().HasData(
                new Shop { Id = 1}
                );
        }

        public DbSet<Account> Accounts => Set<Account>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Library> Librarys => Set<Library>();
        public DbSet<Basket> Baskets => Set<Basket>();
        public DbSet<Shop> Shops => Set<Shop>();
        public DbSet<ProductCategory> ProductCategorys => Set<ProductCategory>();
        public DbSet<Image> Images => Set<Image>();

    }
}
