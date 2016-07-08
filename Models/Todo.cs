using System;

namespace TodoMvc.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Completed { get; set; }
        public DateTime Created { get; set; }
        public string UserName { get; set; }
    }
}
