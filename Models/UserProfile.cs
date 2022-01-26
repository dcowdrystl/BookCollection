using System.ComponentModel.DataAnnotations;

namespace BookCollection.Models
{
    public class UserProfile
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }

        public UserProfile(string userName)
        {
            UserName = userName;
        }

        public UserProfile() { }
    }
}
