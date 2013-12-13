using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;

namespace michaeltroth.blog.Models.InputModel
{
    public class BlogCreationRequestInputModel
    {
        public BlogCreationRequestInputModel()
        {
            this.PublishDate = DateTime.Now;
        }

        [Required(AllowEmptyStrings = false, ErrorMessage="Please enter a blog title")]
        public string BlogTitle { get; set; }

        
        [MaxLength(200, ErrorMessage="Short text can not contain more then 200 characters")]
        public string BlogShortText { get; set; }

        [Required(AllowEmptyStrings=false, ErrorMessage="You must supply a body")]
        public string BodyText { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString="YYYY-MM-dd")]
        public DateTime PublishDate { get; set; }

        public string DelimetedListOfTags { get; set; }

        public virtual Blog ToBlog()
        {
            return new Blog()
            {
                PublishDate = this.PublishDate,
                 ShortSummary = this.BlogShortText,
                  TextBody = this.BodyText,
                   Title = this.BlogTitle,
                   Slug = this.BlogTitle.ToSlug(),
                   Tags = GetCommaList(this.DelimetedListOfTags)
            };
        }

        private string[] GetCommaList(string nonSplitedString)
        {
            return nonSplitedString.Split(',').ToArray();
        }


    }

    public class BlogEditRequestInputModel : BlogCreationRequestInputModel
    {
        public string Id { get;set;}

        
        public string FormatedDate
        {
            get
            {
                return this.PublishDate.ToString("yyyy-MM-dd");
            }
        }

        internal BlogEditRequestInputModel ToInputModel(Blog blog)
        {
            return new BlogEditRequestInputModel()
            {
                 Id = blog.Id.ToString(),
                  BlogShortText = blog.ShortSummary,
                   BlogTitle = blog.Title,
                    BodyText = blog.TextBody,
                     PublishDate = blog.PublishDate,
                     DelimetedListOfTags = FromCommaList(blog.Tags)
            };

        }

        public override Blog ToBlog()
        {
            var model = base.ToBlog();
            model.Id = ObjectId.Parse(this.Id);
            return model;
        }

        private string FromCommaList(string[] splicedData)
        {
            string concatedString = string.Empty;

            foreach (var input in splicedData)
            {
                concatedString += input + ",";
            }

            return concatedString.Trim(new char[] { ',' });
        }

    }

    public class BlogDeleteRequestInputModel
    {
        public string Id { get; set; }
    }

}