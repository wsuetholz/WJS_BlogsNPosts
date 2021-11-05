using System;
using System.Collections.Generic;
using System.Linq;
using WJS_BlogsNPosts.Models;

namespace WJS_BlogsNPosts
{
    class Program
    {
        static void Main(string[] args)
        {
            bool quit = false;
            string resp;
            int rowCount = 0;

            using (var db = new BlogContext())
            {

                Console.WriteLine("Welcome to Blogs and Posts Assignment #10!");

                do
                {
                    Console.WriteLine("\nPlease choose from the following Choices:");
                    Console.WriteLine("1 = Display Blogs, 2 = Add Blog, 3 = Display Posts, 4 = Add Post, 5 = Quit");
                    resp = Console.ReadLine();
                    if (resp.Length > 0)
                    {
                        if (resp.Equals("1"))   // Display Blogs
                        {
                            var blogQuery = db.Blogs.OrderBy(b => b.Name);
                            if (blogQuery.Count() < 1)
                            {
                                Console.WriteLine("No Blogs Defined Yet!");
                            }
                            else
                            {
                                rowCount = 0;
                                foreach (var blog in blogQuery)
                                {
                                    if (rowCount == 0)
                                    {
                                        Console.WriteLine("Blog Names:");
                                        rowCount = 0;
                                    }
                                    Console.WriteLine(blog.Name);
                                    rowCount++;
                                    if (rowCount > 20)
                                    {
                                        Console.WriteLine("\nPlease Press Enter to Continue.  Any Character will Quit.");
                                        resp = Console.ReadLine();
                                        if (resp.Length > 0)
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            rowCount = 0;
                                        }
                                    }
                                }
                            }
                            Console.WriteLine("\nDone Listing Blog Names.");
                        }
                        else if (resp.Equals("2"))      // Add Blog
                        {
                            Console.WriteLine("Please Enter New Blog Name:");
                            resp = Console.ReadLine();
                            if (resp.Length > 0)
                            {
                                var uniqueBlogQuery = db.Blogs.Where(b => b.Name.Equals(resp));
                                if (uniqueBlogQuery.Count() > 0)
                                {
                                    Console.WriteLine($"{resp} already exists!");
                                }
                                else
                                {
                                    var newBlog = new Blog();
                                    newBlog.Name = resp;
                                    newBlog.Posts = new List<Post>();

                                    db.Blogs.Add(newBlog);

                                    db.SaveChanges();
                                }
                            }
                            else
                            {
                                Console.WriteLine("Empty Blog Name!");
                            }
                        }
                        else if (resp.Equals("3"))      // Display Posts
                        {
                            Console.WriteLine("Please Enter Blog Name:");
                            resp = Console.ReadLine();
                            if (resp.Length > 0)
                            {
                                var postsBlogQuery = db.Blogs.Where(b => b.Name.Equals(resp));
                                if (postsBlogQuery.Count() > 0)
                                {
                                    var blog = postsBlogQuery.First();
                                    var blogName = blog.Name;

                                    var postQuery = db.Posts.Where(p => p.BlogId == blog.BlogId);
                                    var postCount = postQuery.Count();
                                    if (postCount < 1)
                                    {
                                        Console.WriteLine($"No Posts Exist For Blog {blog.Name}!");
                                    }
                                    else
                                    {
                                        rowCount = 0;
                                        foreach (var post in postQuery)
                                        {
                                            if (rowCount == 0)
                                            {
                                                Console.WriteLine($"Blog {blog.Name} With {postCount} Posts:");
                                                rowCount = 0;
                                            }
                                            Console.WriteLine($"Title: {post.Content}");
                                            Console.WriteLine($"Content: {post.Content}\n");
                                            rowCount++;
                                            if (rowCount > 10)
                                            {
                                                Console.WriteLine("\nPlease Press Enter to Continue.  Any Character will Quit.");
                                                resp = Console.ReadLine();
                                                if (resp.Length > 0)
                                                {
                                                    break;
                                                }
                                                else
                                                {
                                                    rowCount = 0;
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine($"{resp} doesn't exist!");
                                }
                            }
                        }
                        else if (resp.Equals("4"))      // Add Posts
                        {
                            Console.WriteLine("Please Enter Blog Name:");
                            resp = Console.ReadLine();
                            if (resp.Length > 0)
                            {
                                var postsBlogQuery = db.Blogs.Where(b => b.Name.Equals(resp));
                                if (postsBlogQuery.Count() > 0)
                                {
                                    var blog = postsBlogQuery.First();
                                    var post = new Post();

                                    Console.WriteLine($"{blog.Name} New Post Title:");
                                    resp = Console.ReadLine();
                                    if (resp.Length > 0)
                                    {
                                        post.Title = resp;
                                    }
                                    Console.WriteLine($"{blog.Name} New Post Content:");
                                    resp = Console.ReadLine();
                                    if (resp.Length > 0)
                                    {
                                        post.Content = resp;
                                    }

                                    if (post.Title.Length > 0 || post.Content.Length > 0)
                                    {
                                        post.Blog = blog;
                                        post.BlogId = blog.BlogId;

                                        db.Posts.Add(post);

                                        db.SaveChanges();
                                    }
                                }
                                else
                                {
                                    Console.WriteLine($"{resp} doesn't exist!");
                                }

                            }

                        }
                        else if (resp.Equals("5"))      // Quit
                        {
                            quit = true;
                        }
                    }
                } while (!quit);
            }
        }
    }
}
