using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBlogLibrary.Objects
{
	public class Tag
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "This field is required")]
		[MaxLength(100)]
		[Column(TypeName = "varchar(100)")]
		public string Name { get; set; }

		[Required]
		[MaxLength(100)]
		[Column(TypeName = "varchar(100)")]
		public string UrlSlug { get; set; }

		public virtual ICollection<Post> Posts { get; } = new List<Post>();

        public Tag() { }
        public Tag(string name, string slug)
        {
			Name = name;
			UrlSlug = slug;
        }
    }
}
