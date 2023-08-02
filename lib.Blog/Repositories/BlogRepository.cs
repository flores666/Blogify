﻿using lib.Blog.DataAccess;
using lib.Blog.Interfaces;
using lib.Blog.Objects;
using lib.Extentions;
using Microsoft.EntityFrameworkCore;

namespace lib.Blog.Repositories
{
    public class BlogRepository : IBlogRepository
    {

        /// <param name="pageNum">Starts from 0</param>
        public List<Post> GetLatestPosts(int pageNum, int pageSize)
        {
            using (var db = new BlogContext())
            {
                return db.Posts
                    .Where(p => p.Published)
                    .OrderByDescending(p => p.PostedOn)
                    .Skip(pageNum * pageSize)
                    .Take(pageSize)
                    .Include(p => p.Tags)
                    .ToList();
            }
        }

        public int CountTotalPosts()
        {
            using (var db = new BlogContext())
            {
                return db.Posts.Count(p => p.Published == true);
            }
        }

        public List<Post> GetLatestPostsForTag(int pageNum, int pageSize, string tag)
        {
            using (var db = new BlogContext())
            {
                return db.Posts
                    .Where(p => p.Published && p.Tags.Any(t => t.Name == tag))
                    .OrderByDescending(p => p.PostedOn)
                    .Skip(pageNum * pageSize)
                    .Take(pageSize)
                    .Include(p => p.Tags)
                    .ToList();
            }
        }

        public List<Post> GetLatestPostsForSearch(int pageNum, int pageSize, string search)
        {
            using (var db = new BlogContext())
            {
                return db.Posts
                    .Where(p => p.Published && p.Tags.Any(t => t.Name == search) ||
                                                p.Title.Contains(search) ||
                                                p.Text.Contains(search))
                    .OrderByDescending(p => p.PostedOn)
                    .Skip(pageNum * pageSize)
                    .Take(pageSize)
                    .Include(p => p.Tags)
                    .ToList();
            }
        }

        public Post ShowPost(int id)
        {
            using (var db = new BlogContext())
            {
                var a = db.Posts.Where(p =>p.Id == id).Include(p => p.Tags).FirstOrDefault();
                return a;
            }
        }
    }
}
