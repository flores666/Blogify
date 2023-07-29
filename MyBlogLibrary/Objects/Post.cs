using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBlogLibrary.Objects
{
	public class Post
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "This field is required")]
		[MaxLength(100)]
		[Column(TypeName = "varchar(100)")]
		public string Title { get; set; }

		[Required(ErrorMessage = "This field is required")]
		[Column(TypeName = "text")]
		[DisplayName("Text")]
		public string Text { get; set; }

		[Required]
		[MaxLength(100)]
		[Column(TypeName = "varchar(100)")]
		public string UrlSlug { get; set; }

		[Required]
		public bool Published { get; set; }

		[Required]
		[DisplayName("Posted on")]
		public DateTime PostedOn { get; set; }

		[DisplayName("Modiefied on")]
		public DateTime? Modiefied { get; set; }

		public virtual ICollection<Tag> Tags { get; } = new List<Tag>();

        public Post() { }
        public Post(string title, string text, string urlSlug, bool published, 
			DateTime postedOn, DateTime? modiefied, ICollection<Tag> tags)
		{ 
			Title = title;
			Text = text;
			UrlSlug = urlSlug;
			Published = published;
			PostedOn = postedOn;
			Modiefied = modiefied;
			Tags = tags;
		}
	}
}
