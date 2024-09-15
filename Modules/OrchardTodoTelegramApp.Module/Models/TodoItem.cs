using Microsoft.Azure.Cosmos.Table;

namespace OrchardTodoTelegramApp.Module.Models
{
    public class TodoItem : TableEntity
    {
        public string Title { get; set; }
        public bool IsCompleted { get; set; }

        // Boş Constructor (Azure Table Storage için gereklidir)
        public TodoItem() { }

        // TodoItem'ı başlatan constructor
        public TodoItem(string partitionKey, string rowKey, string title, bool isCompleted)
        {
            PartitionKey = partitionKey;
            RowKey = rowKey;
            Title = title;
            IsCompleted = isCompleted;
        }

        // Azure Table Storage'da optimistik eşzamanlılık kontrolü için ETag özelliği
        public string ETag { get; set; } = "*";
    }
}
