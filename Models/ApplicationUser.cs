using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace BookCollection.Models
{
    public class ApplicationUser : IdentityUser
    {
        //public int BookId { get; set; }
        
        //public Book UserBook { get; set; }

        //[Required]
        //[Display(Name = "First Name")]
        //public string FirstName { get; set; }

        //[Required]
        //[Display(Name = "Last Name")]
        //public string LastName { get; set; }

        //[NotMapped]
        //[Display(Name = "Full Name")]
        //public string FullName => $"{FirstName} {LastName}";

        public ApplicationUser()
        {

        }
    }

    
}
