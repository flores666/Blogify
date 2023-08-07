using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace lib.Blog.Objects
{
	[Table("posts", Schema = "blog")]
	public class Post
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "This field is required")]
		[Column(TypeName = "varchar(100)")]
		public string Title { get; set; }

		[Required(ErrorMessage = "This field is required")]
		[Column(TypeName = "text")]
		[DisplayName("Text")]
		public string Text { get; set; }

		[Required]
		[Column(TypeName = "varchar(120)")]
		public string UrlSlug { get; set; }

		[Required]
		public bool Published { get; set; }

		[Required]
		[DisplayName("Posted on")]
		public DateTime PostedOn { get; set; }

		[DisplayName("Modified on")]
		public DateTime? Modified { get; set; }

		public ICollection<Tag> Tags { get; set; } = new List<Tag>();

		[Required(ErrorMessage = "Укажите автора")]
		public AppUser AppUser { get; set; }
	}
}
