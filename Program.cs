using NLog;
using BlogsConsole.Models;
using System;
using System.Linq;

namespace BlogsConsole
{
    class MainClass
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public static void Main(string[] args)
        {
            //logger.Info("Program started");
            //try
            //{

            bool inProgram = true;

            do
            {
                Console.WriteLine("Enter your selection: ");
                Console.WriteLine("1) Display all Blogs");
                Console.WriteLine("2) Add Blog");
                Console.WriteLine("3) Create Post");
                Console.WriteLine("4) Display Posts");
                Console.WriteLine("Enter q to quit");
                string response = Console.ReadLine();

                switch (response)
                {
                    case "1":
                        {
                            var db = new BloggingContext();
                            db.DisplayBlogs();
                            db.SaveChanges();
                            break;
                        }
                    case "2":
                        {
                            var db = new BloggingContext();
                            db.AddBlogs();
                            db.SaveChanges();
                            break;
                        }
                    case "3":
                        {
                            var db = new BloggingContext();
                            db.CreatePost();
                            db.SaveChanges();
                            break;
                        }
                    case "4":
                        {
                            var db = new BloggingContext();
                            db.DisplayBlogs();
                            db.SaveChanges();
                            break;
                        }
                    case "q":
                        {
                            inProgram = false;
                            break;
                        }
                }

            } while (inProgram);



            //    // Display all Blogs from the database
            //    var query = db.Blogs.OrderBy(b => b.Name);

            //    Console.WriteLine("All blogs in the database:");
            //    foreach (var item in query)
            //    {
            //        Console.WriteLine(item.Name);
            //    }
        }
        //catch (Exception ex)
        //{
        //    logger.Error(ex.Message);
        //}
        //logger.Info("Program ended");
    }
}

//    }
//}
