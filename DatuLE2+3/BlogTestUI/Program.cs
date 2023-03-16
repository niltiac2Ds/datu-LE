using BlogDataLibrary.Data;
using BlogDataLibrary.Database;
using BlogDataLibrary;
using Microsoft.Extensions.Configuration;
using BlogDataLibrary.Models;

namespace BlogTestUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SqlData db = GetConnection();
            // Authenticate(db);
            // Register(db);
            AddPost(db);


            Console.WriteLine("Press enter to exit...");
            Console.ReadLine();
        }
        static SqlData GetConnection()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            IConfiguration config = builder.Build();
            ISqlDataAccess dbAccess = new SqlDataAccess(config);
            SqlData db = new SqlData(dbAccess);

            return db;
        }

        // user login and authentication
        private static UserModel GetCurrentUser(SqlData db)
        {
            Console.Write("Username: ");
            string username = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            UserModel user = db.Authenticate(username, password);

            return user;
        }


        public static void Authenticate(SqlData db)
        {
            UserModel user = GetCurrentUser(db);
            if (user == null)
            {
                Console.WriteLine("Invalid Credentials.");
            }
            else
            {
                Console.WriteLine($"Welcome, {user.UserName}");
            }
        }

        // user registration
        public static void Register(SqlData db)
        {
            Console.Write("Enter new username: ");
            var username = Console.ReadLine();

            Console.Write("Enter new password: ");
            var password = Console.ReadLine();

            Console.Write("Enter new first name: ");
            var firstName = Console.ReadLine();

            Console.Write("Enter new last name: ");
            var lastName = Console.ReadLine();

            db.Register(username, password, firstName, lastName);

        }

        // add post
        private static void AddPost(SqlData db)
        {
            UserModel user = GetCurrentUser(db);

            Console.Write("Title: ");
            string title = Console.ReadLine();

            Console.Write("Write Body: ");
            string body = Console.ReadLine();

            PostModel post = new PostModel
            {
                Title = title,
                Body = body,
                DateCreated = DateTime.Now,
                UserId = user.Id
            };
            db.addPost(post);
        }




    }
}