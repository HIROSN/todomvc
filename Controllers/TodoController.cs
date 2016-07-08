using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using TodoMvc.Models;

namespace TodoMvc.Controllers
{
    [Authorize]
    public class TodoController : Controller
    {
        private ITodoRepository _repository;

        public TodoController(ITodoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("api/todos")]
        public JsonResult Get()
        {
            var todos = _repository.GetAllTodos(User.Identity.Name);
            if (todos != null)
            {
                return Json(Mapper.Map<IEnumerable<TodoModel>>(todos));
            }

            return Json(new {});
        }

        [HttpPost("api/todos")]
        public JsonResult Post([FromBody]TodoModel model)
        {
            Response.StatusCode = (int)HttpStatusCode.NotFound;

            if (ModelState.IsValid)
            {
                var todo = Mapper.Map<Todo>(model);
                todo.Created = DateTime.UtcNow;
                todo.UserName = User.Identity.Name;
                _repository.AddTodo(ref todo);

                if (_repository.SaveAll())
                {
                    Response.StatusCode = (int)HttpStatusCode.OK;
                    return Json(new { id = todo.Id });
                }
            }

            return Json(new {});
        }

        [HttpPut("api/todos/{id}")]
        public void Put(int id, [FromBody]TodoModel model)
        {
            Response.StatusCode = (int)HttpStatusCode.NotFound;

            if (ModelState.IsValid)
            {
                var todo = _repository.FindTodoById(id, User.Identity.Name);
                if (todo != null)
                {
                    todo.Title = model.Title;
                    todo.Completed = model.Completed;
                    _repository.UpdateTodo(todo);

                    if (_repository.SaveAll())
                    {
                        Response.StatusCode = (int)HttpStatusCode.OK;
                    }
                }
            }
        }

        [HttpDelete("api/todos/{id}")]
        public void Delete(int id)
        {
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            var todo = _repository.FindTodoById(id, User.Identity.Name);
            if (todo != null)
            {
                _repository.DeleteTodo(todo);
                if (_repository.SaveAll())
                {
                    Response.StatusCode = (int)HttpStatusCode.OK;
                }
            }
        }

        [HttpDelete("api/todos")]
        public void Delete()
        {
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            _repository.DeleteCompletedTodos(User.Identity.Name);
            if (_repository.SaveAll())
            {
                Response.StatusCode = (int)HttpStatusCode.OK;
            }
        }
    }
}
