using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace OrchardTodoTelegramApp.Services
{
    public class TelegramBotService
    {
        private readonly TelegramBotClient _botClient;
        private readonly string _botToken;

        public TelegramBotService(string botToken)
        {
            _botToken = botToken;
            _botClient = new TelegramBotClient(_botToken);


            // Botu dinlemeye başla
            StartReceiving();
        }

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == UpdateType.Message)
            {
                var message = update.Message;
                if (message.Text.ToLower() == "/start")
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Bot başarıyla çalışıyor!");
                }
                else if (message.Text.ToLower() == "/todo")
                {
                    //await botClient.SendTextMessageAsync(message.Chat.Id, "TO-DO listeniz buraya gelecek!");


                    var todoList = await GetTodoListFromApi();
                    await botClient.SendTextMessageAsync(message.Chat.Id, todoList, cancellationToken: cancellationToken);

                }
                else if (message.Text.ToLower() == "/help")
                {
                    string helpMessage = "Kullanabileceğiniz komutlar:\n" +
                                         "/start - Botu başlatır.\n" +
                                         "/todo - Yapılacaklar listenizi gösterir.\n" +
                                         "/help - Yardım mesajını gösterir.";
                    await botClient.SendTextMessageAsync(message.Chat.Id, helpMessage, cancellationToken: cancellationToken);
                }
            }
        }

        public Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var errorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(errorMessage);
            return Task.CompletedTask;
        }
        private async Task<string> GetTodoListFromApi()
        {

            try
            {

                string protocol;
                string baseUrl;
                string endpoint = "/GetTodoListForBot";

                string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

                if (environment == "Development")
                {
                    if (Environment.GetEnvironmentVariable("USE_HTTPS") == "true")
                    {
                        protocol = "https";
                        baseUrl = "localhost:5001"; // HTTPS portu
                    }
                    else
                    {
                        protocol = "http";
                        baseUrl = "localhost:5000"; // HTTP portu
                    }
                }
                else if (environment == "Production")
                {
                    protocol = "https";
                    baseUrl = "localhost:44383"; // IIS Express HTTPS portu
                }
                else
                {
                    protocol = "http";
                    baseUrl = "localhost:5000";
                }
                var uri = new Uri($"{protocol}://{baseUrl}{endpoint}");



                //var uri = new Uri("https://localhost:44383/GetTodoListForBot");

                using (var client = new HttpClient())
                {
                    //var response = await client.GetStringAsync("/GetTodoListForBot");

                    var response = await client.GetStringAsync(uri);
                    return response;
                }

            }
            catch (Exception ex)
            {
                return $"Bir hata oluştu: {ex.Message}";
            }

        }
        public void StartReceiving()
        {
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = Array.Empty<UpdateType>()
            };

            _botClient.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                CancellationToken.None
            );
        }
     
    }
}
