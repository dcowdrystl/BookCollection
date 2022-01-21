using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BookCollection.Models
{
    public class BookUser
    {
        //public int Id { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }

        ////[Key]
        //public string RUserId { get; set; }

        //[ForeignKey("ApplicationUserId")]
        public ApplicationUser User { get; set; }
        public string ApplicationUserId { get; set; }

        public BookUser()
        {

        }

        
    }
}
