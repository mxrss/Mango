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
    public class Blog
    {
        
        [BsonId]
        public BsonObjectId Id { get; set; }

        [BsonElement("blog_title")]
        public string Title { get; set; }

        [BsonDateTimeOptions(DateOnly=true, Kind=DateTimeKind.Utc)]
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
            
            MongoClient client = new MongoClient(Configuration.MongoServer);

            var server = client.GetServer();
            server.Connect();
            var collection = server.GetDatabase("blog").GetCollection<Blog>("blog_entries");


            collection.Insert<Blog>(blogInstance);

            return blogInstance;
        }

        public void DeleteBlog(BlogDeleteRequestInputModel model)
        {
            
        }


        public static void EditBlog(BlogEditRequestInputModel model)
        {
            MongoClient client = new MongoClient(Configuration.MongoServer);

            var server = client.GetServer();
            server.Connect();
            var collection = server.GetDatabase("blog").GetCollection<Blog>("blog_entries");
            
            if (collection.FindOneById(ObjectId.Parse(model.Id))!= null)
            {
                collection.Save<Blog>(model.ToBlog());
            }
            
        }


        public static IEnumerable<Blog> GetBlogs(int page = 0, int maxPerPage = 10)
        {
            ICollection<Blog> blogs = new List<Blog>();
            MongoClient client = new MongoClient(Configuration.MongoServer);

            var server = client.GetServer();
            server.Connect();
            var collection = server.GetDatabase("blog").GetCollection<Blog>("blog_entries");

            blogs = collection.AsQueryable<Blog>().Skip(page * maxPerPage)
                .Take(maxPerPage).ToList();

            return blogs;
        }

        public static Blog GetBlogById(string Id)
        {
            MongoClient client = new MongoClient(Configuration.MongoServer);

            var server = client.GetServer();
            server.Connect();
            var collection = server.GetDatabase("blog").GetCollection<Blog>("blog_entries").FindOneById(ObjectId.Parse(Id));
            return collection;
        }

    }
}