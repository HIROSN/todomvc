using System.ComponentModel.DataAnnotations;

namespace TodoMvc.Models
{
    public class TodoUserModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
