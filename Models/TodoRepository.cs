using System.Collections.Generic;
using System.Linq;

namespace TodoMvc.Models
{
    public class TodoRepository : ITodoRepository
    {
        private TodoContext _context;

        public TodoRepository(TodoContext context)
        {
            _context = context;
        }

        public void AddTodo(ref Todo todo)
        {
            _context.Add(todo);
        }

        public void DeleteCompletedTodos(string userName)
        {
            var completedTodos = _context.Todos.Where(t => t.Completed && t.UserName == userName);
            _context.RemoveRange(completedTodos);
        }

        public void DeleteTodo(Todo todo)
        {
            _context.Remove(todo);
        }

        public Todo FindTodoById(int id, string userName)
        {
            return _context.Todos.Where(t => t.Id == id && t.UserName == userName).FirstOrDefault();
        }

        public IEnumerable<Todo> GetAllTodos(string userName)
        {
            return _context.Todos.Where(t => t.UserName == userName).ToList();
        }

        public bool SaveAll()
        {
            return (_context.SaveChanges() > 0);
        }

        public void UpdateTodo(Todo todo)
        {
            _context.Todos.Update(todo);
        }
    }
}
