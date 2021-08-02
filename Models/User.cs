using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ReactAuth.NetCore.Models
{
    public class User
    {
       [Required]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
    }
}