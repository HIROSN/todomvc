using System.ComponentModel.DataAnnotations;

namespace TodoMvc.Models
{
    public class TodoModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(140)]
        public string Title { get; set; }

        [Required]
        public bool Completed { get; set; }
    }
}
