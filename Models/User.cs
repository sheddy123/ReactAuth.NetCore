using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ReactAuth.NetCore.Models
{
    public class User
    {
       [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="User name is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage ="Email field is required")]
        public string Email { get; set; }
        [JsonIgnore]
        [Required(ErrorMessage ="Password field is required")]
        public string Password { get; set; }
        [NotMapped]
        public string ErrorMessage { get; set; }
    }
}