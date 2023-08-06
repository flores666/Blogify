using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lib.Blog.Objects
{
	[Table("tags")]
	public class Tag
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "This field is required")]
		[Column(TypeName = "varchar(20)")]
		public string Name { get; set; }

		[Required]
		[Column(TypeName = "varchar(30)")]
		public string UrlSlug { get; set; }
		
		public ICollection<Post> Posts { get; } = new List<Post>();
	}
}
