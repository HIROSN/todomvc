using System.Collections.Generic;

namespace TodoMvc.Models
{
    public interface ITodoRepository
    {
        IEnumerable<Todo> GetAllTodos(string userName);
        void AddTodo(ref Todo todo);
        Todo FindTodoById(int id, string userName);
        void UpdateTodo(Todo todo);
        void DeleteTodo(Todo todo);
        void DeleteCompletedTodos(string userName);
        bool SaveAll();
    }
}
