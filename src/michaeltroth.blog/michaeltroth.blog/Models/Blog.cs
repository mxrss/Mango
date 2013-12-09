using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using michaeltroth.blog.Models.InputModel;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace michaeltroth.blog.Models
{
    public enum BlogStatus
    {
        Published = 0,
        All = 1
    }


    public class Blog
    {
        
        [BsonId]
        public BsonObjectId Id { get; set; }

        [BsonElement("blog_title")]
        public string Title { get; set; }

        [BsonDateTimeOptions()]
        public DateTime PublishDate { get; set; }

        [BsonElement("blog_shortText")]
        public string ShortSummary { get; set; }

        [BsonElement("blog_body")]
        public string TextBody { get; set; }

        [BsonElement("blog_tags")]
        public string[] Tags { get; set; }

        public static Blog CreateBlog(BlogCreationRequestInputModel model)
        {
            var blogInstance = model.ToBlog();
            var collection = GetConnection().GetDatabase("blog").GetCollection<Blog>("blog_entries");


            collection.Insert<Blog>(blogInstance);

            return blogInstance;
        }

        public void DeleteBlog(BlogDeleteRequestInputModel model)
        {
            
        }


        public static void EditBlog(BlogEditRequestInputModel model)
        {
            var collection = GetConnection().GetDatabase("blog").GetCollection<Blog>("blog_entries");   
            if (collection.FindOneById(ObjectId.Parse(model.Id))!= null)
            {
                collection.Save<Blog>(model.ToBlog());
            }
            
        }


        public static IEnumerable<Blog> GetBlogs(BlogStatus status = BlogStatus.Published,  int page = 0, int maxPerPage = 10)
        {
            var collection = GetConnection().GetDatabase("blog").GetCollection<Blog>("blog_entries");

            var blogCollection = collection.AsQueryable<Blog>().Skip(page * maxPerPage)
                .Take(maxPerPage);

            if (status == BlogStatus.Published)
                blogCollection = blogCollection.Where(x => x.PublishDate <= DateTime.Now);

            return blogCollection.OrderByDescending(x=> x.PublishDate).ToList();
        }

        public static Blog GetBlogById(string Id)
        {;
            var collection = GetConnection().GetDatabase("blog")
                .GetCollection<Blog>("blog_entries").FindOneById(ObjectId.Parse(Id));
            return collection;
        }

        private static MongoServer GetConnection()
        {
            MongoClient client = new MongoClient(Configuration.MongoServer);
            var server = client.GetServer();
            server.Connect();
            return server;
        }


        internal static Blog GetBlogById(DateTime publishedDate, string topic)
        {
            var blogInstances = GetConnection().GetDatabase("blog").GetCollection<Blog>("blog_entries");

            var blog = blogInstances.AsQueryable().Where(x => (x.PublishDate >= publishedDate.AddDays(-1) &&  x.PublishDate <= publishedDate.AddDays(1))
                && x.Title == topic).SingleOrDefault();

            return blog;
        }
    }
}