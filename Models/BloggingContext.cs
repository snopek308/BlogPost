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
            if(Blogs.Count() > 0)
            {
                var query = Blogs.ToList();
                Console.WriteLine("All Blogs in the database: ");
                Console.WriteLine("{0}-{1}", "Blog ID", "Blog Name");

                foreach (var x in query)
                {
                    Console.WriteLine("{0}-{1}", x.BlogId, x.Name);
                }
                Console.WriteLine(Blogs.Count() + " Blogs Returned");
                Console.WriteLine("\n");
                this.SaveChanges();
            }
            else
            {
                Console.WriteLine(Blogs.Count() + " Blogs Returned");
            }
            

            
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
            var nameIsValid = false;
            do
            {
                Blog x = new Blog();
                Console.WriteLine("Enter in the new Blog name: ");
                x.Name = Console.ReadLine();

                if (x.Name is null)
                {
                    Console.WriteLine("Name cannot be null");
                }
                else
                {
                    nameIsValid = true;
                    this.Blogs.Add(x);
                    this.SaveChanges();
                    Console.WriteLine("\n");
                }
            } while(!nameIsValid);
            
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

        public void EditBlog()
        {
            DisplayBlogs();
            Console.WriteLine("Which Blog do you want to edit by ID: ");
            int blogId = Convert.ToInt32(Console.ReadLine());

            var existingBlog = Blogs.Where(b => b.BlogId == blogId).FirstOrDefault();

            if (existingBlog != null)
            {
                Console.WriteLine("Enter a new Blog Title: ");
                existingBlog.Name = Console.ReadLine();
                this.SaveChanges();
                Console.WriteLine("\n");
            }

            else
            {
                Console.WriteLine("Not a valid ID. Please pick a valid ID");
                EditBlog();
            }

        }

        public void DeleteBlog()
        {
            DisplayBlogs();
            Console.WriteLine("Which Blog do you want to delete by ID: ");
            int blogId = Convert.ToInt32(Console.ReadLine());

            var existingBlog = Blogs.Where(b => b.BlogId == blogId).FirstOrDefault();

            if (existingBlog != null)
            {
                Blogs.Remove(existingBlog);
                this.SaveChanges();
            }

            else
            {
                Console.WriteLine("Not a valid ID. Please pick a valid ID");
                DeleteBlog();
            }


        }

        public void EditPost()
        {
            DisplayPosts();
            Console.WriteLine("Which Post do you want to edit by ID: ");
            int postId = Convert.ToInt32(Console.ReadLine());

            var existingPost = Posts.Where(b => b.PostId == postId).FirstOrDefault();

            if (existingPost != null)
            {
                Console.WriteLine("What would you like to change? ");
                Console.WriteLine("1) Title");
                Console.WriteLine("2) Post Content");
                Console.WriteLine("3) Edit Both");
                string response = Console.ReadLine();

                switch(response)
                {
                    case "1":
                        {
                            Console.WriteLine("Enter a new Post Title: ");
                            existingPost.Title = Console.ReadLine();
                            this.SaveChanges();
                            Console.WriteLine("\n");
                            break;
                        }
                    case "2":
                        {
                            Console.WriteLine("Enter new Post Content: ");
                            existingPost.Content = Console.ReadLine();
                            this.SaveChanges();
                            Console.WriteLine("\n");
                            break;
                        }
                    case "3":
                        {
                            //This is not the best way to do this, I should be writing some methods and calling
                            //them in this switch.
                            Console.WriteLine("Enter a new Post Title: ");
                            existingPost.Title = Console.ReadLine();
                            this.SaveChanges();
                            Console.WriteLine("\n");

                            Console.WriteLine("Enter new Post Content: ");
                            existingPost.Content = Console.ReadLine();
                            this.SaveChanges();
                            Console.WriteLine("\n");

                            break;
                        }
                }
            }

            else
            {
                Console.WriteLine("Not a valid ID. Please pick a valid ID");
                EditPost();
            }

        }

        public void DeletePost()
        {
            DisplayPosts();
            Console.WriteLine("Which Post do you want to delete by ID: ");
            int postId = Convert.ToInt32(Console.ReadLine());

            var existingPost = Posts.Where(b => b.PostId == postId).FirstOrDefault();

            if (existingPost != null)
            {
                Posts.Remove(existingPost);
                this.SaveChanges();
            }

            else
            {
                Console.WriteLine("Not a valid ID. Please pick a valid ID");
                DeleteBlog();
            }
        }
    }
}
