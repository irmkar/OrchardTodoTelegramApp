using Microsoft.Azure.Cosmos.Table;
using System.Threading.Tasks;
using System.Collections.Generic;
using OrchardTodoTelegramApp.Module.Models;
using Microsoft.Azure.Documents;

namespace OrchardTodoTelegramApp.Module.Repositories
{
    public class TodoRepository
    {
        private readonly CloudTable _table;

        public TodoRepository(CloudTable table)
        {
            _table = table;
        }

        // Get All Todo Items
        public async Task<List<TodoItem>> GetAllTodoItemsAsync()
        {
            try
            {
                var query = new TableQuery<TodoItem>();
                var segment = await _table.ExecuteQuerySegmentedAsync(query, null);
                return segment.Results;
            }
            catch (StorageException ex)
            {
                // Hata yönetimi (loglama veya hata fırlatma)
                throw new System.Exception("Error retrieving Todo items", ex);
            }
        }

        // Create a new Todo Item
        public async Task CreateTodoItemAsync(TodoItem item)
        {
            try
            {
                var insertOperation = TableOperation.Insert(item);
                await _table.ExecuteAsync(insertOperation);
            }
            catch (StorageException ex)
            {
                // Hata yönetimi
                throw new System.Exception("Error creating Todo item", ex);
            }
        }

        // Update an existing Todo Item
        public async Task UpdateTodoItemAsync(TodoItem item)
        {
            try
            {


                var replaceOperation = TableOperation.Replace(item);
                await _table.ExecuteAsync(replaceOperation);
                //var retrieveOperation = TableOperation.Retrieve<TodoItem>(item.PartitionKey, item.RowKey);
                //TableResult tr = await _table.ExecuteAsync(retrieveOperation);

                //retrieveOperation = TableOperation.InsertOrMerge(item);

            }
            catch (StorageException ex)
            {
                // Hata yönetimi
                throw new System.Exception("Error updating Todo item", ex);
            }
        }

        // Delete a Todo Item
        public async Task DeleteTodoItemAsync(string partitionKey, string rowKey)
        {
            try
            {
                var retrieveOperation = TableOperation.Retrieve<TodoItem>(partitionKey, rowKey);
                var retrievedResult = await _table.ExecuteAsync(retrieveOperation);
                var deleteEntity = (TodoItem)retrievedResult.Result;
                if (deleteEntity != null)
                {
                    var deleteOperation = TableOperation.Delete(deleteEntity);
                    await _table.ExecuteAsync(deleteOperation);
                }
                else
                {
                    throw new System.Exception("Todo item not found.");
                }
            }
            catch (StorageException ex)
            {
                // Hata yönetimi
                throw new System.Exception("Error deleting Todo item", ex);
            }
        }
    }
}
