using Microsoft.AspNetCore.Mvc;
using OrchardTodoTelegramApp.Module.Models;
using OrchardTodoTelegramApp.Module.Repositories;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Azure.Documents;
using Castle.Core.Resource;

namespace OrchardTodoTelegramApp.Module.Controllers
{
    //[Route("Module/[controller]/[action]")]
    public class TodoController : Controller
    {
        private readonly TodoRepository _repository;
        private readonly string _partitionKey = "TodoList";

        public TodoController(TodoRepository repository)
        {
            _repository = repository;
        }

        // Tüm Todo öğelerini döndüren action (Vue.js için JSON formatında)
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<TodoItem> todos = await _repository.GetAllTodoItemsAsync();
            return Json(todos); // JSON formatında geri dön
        }

        [HttpGet("GetTodoListForBot")]
        public async Task<string> GetTodoListForBot()
        {
            try
            {
                List<TodoItem> todos = await _repository.GetAllTodoItemsAsync();

                if (todos.Count == 0)
                {
                    return "Yapılacaklar listenizde hiçbir öğe bulunmamaktadır.";
                }

                string todoList = "Yapılacaklar Listeniz:\n";
                foreach (var todo in todos)
                {
                    // Eğer tamamlandıysa ✓ işareti, değilse "Tamamlanmadı" ekle
                    string completionStatus = todo.IsCompleted ? "✓" : "Tamamlanmadı";
                    todoList += $"- {todo.Title}: {completionStatus}\n";
                }

                return todoList;
            }
            catch (Exception ex)
            {
                return $"Bir hata oluştu: {ex.Message}";
            }
        }


        [HttpGet]
        public IActionResult Index()
        {
            return View();  
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Create([FromBody] TodoItem item)
        {
            if (ModelState.IsValid)
            {

                TodoItem todoItem = new TodoItem(_partitionKey, Guid.NewGuid().ToString(), item.Title, item.IsCompleted);
                await _repository.CreateTodoItemAsync(todoItem);
                return Ok();
            }
            return BadRequest(ModelState);
        }

        public async Task<IActionResult> Get(string id)
        {
            List<TodoItem> todos = await _repository.GetAllTodoItemsAsync();
            var item = todos.First(m => m.PartitionKey == _partitionKey && m.RowKey == id);
            return Json(item);
        }

        // Mevcut bir Todo öğesini güncelleme (Vue.js ile)


        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            return View();
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Edit([FromBody] TodoItem item)
        {

            ModelState.Remove("ETag");
            if (ModelState.IsValid)
            {
                List<TodoItem> todos = await _repository.GetAllTodoItemsAsync();
                var currentData = todos.First(m => m.PartitionKey == _partitionKey && m.RowKey == item.RowKey);

                currentData.Title = item.Title;
                currentData.IsCompleted = item.IsCompleted;

                await _repository.UpdateTodoItemAsync(currentData);

                return Ok();
            }
            return BadRequest(ModelState);
        }

        // Bir Todo öğesini silme (Vue.js ile)
        [HttpDelete]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            await _repository.DeleteTodoItemAsync(_partitionKey, id);
            return Ok();
        }
    }
}
