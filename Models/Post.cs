using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookCollection.Models
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }

        [Required]
        [Display(Name = "Review")]
        public string Content { get; set; }

        /*[Required]
        public int AlbumRatingNumber {get; set;}*/

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

      // Navigation and foreign keys
      
      //--------------------------------------------------should i change UserId to UserFirstName?
      public string UserId { get; set; }
      //public BookUser PostUser { get; set; }
     // [ForeignKey("ApplicationUserId")]
      public ApplicationUser User { get; set; }

      //[Key]
      public string ApplicationUserId { get; set; }

      
      public int BookId { get; set; }
        public Book PostBook{ get; set; }

      /*public int RatingId { get; set; }
      public AlbumRating PostRating { get; set; }*/

      public List<Like> Likes { get; set; }
      public List<Comment> Comments { get; set; }
    }
}