using NLog;
using System;
using System.Data.Entity;
using System.Linq;

namespace BlogsConsole.Models
{
    public class BloggingContext : DbContext
    {
        public BloggingContext() : base("name=BlogContext") { }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }


        public void DisplayBlogs()
        {
            var query = Blogs.ToList();
            Console.WriteLine("All Blogs in the database: ");
            Console.WriteLine("{0}-{1}", "Blog ID", "Blog Name");

            foreach(var x in query)
            {
                Console.WriteLine("{0}-{1}", x.BlogId, x.Name);
            }
            Console.WriteLine("\n");
            this.SaveChanges();
        }

        public void DisplayPosts()
        {
            var query = Posts.ToList();
            Console.WriteLine("All the Posts in the database: ");
            Console.WriteLine("{0}-{1}-{2}", "Post ID", "Post Title", "Post Content");

            foreach(var x in query)
            {
                Console.WriteLine("{0}-{1}-{2}", x.PostId, x.Title, x.Content);
            }
        }

        public void AddBlogs()
        {
            Blog x = new Blog();
            Console.WriteLine("Enter in the new Blog name: ");
            x.Name = Console.ReadLine();

            this.Blogs.Add(x);
            this.SaveChanges();
            Console.WriteLine("\n");
        }

        public void CreatePost()
        {
            DisplayBlogs();
            Console.WriteLine("Choose a Blog you would like to add a post to by ID: ");
            int choice = Int32.Parse(Console.ReadLine());

            try
            {
                
                var blog = this.Blogs.FirstOrDefault(t => t.BlogId == choice);
                Post x = new Post();
                x.BlogId = blog.BlogId;

                Console.WriteLine("Enter in a new Post Title: ");
                x.Title = Console.ReadLine();

                 
                Console.WriteLine("Enter the new Post Content: ");
                x.Content = Console.ReadLine();

                this.Posts.Add(x);
                this.SaveChanges();
                Console.WriteLine("\n");
            }
            catch
            {
                Console.WriteLine("This is not a valid ID");
            }
          
        }
    }
}
