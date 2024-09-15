using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using OrchardTodoTelegramApp.Services;

var builder = WebApplication.CreateBuilder(args);

var botToken = builder.Configuration["TelegramBotToken"];
builder.Services.AddSingleton<TelegramBotService>(sp => new TelegramBotService(botToken));

builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddControllers();

builder.Services.AddAuthorization();
// Orchard Core CMS'i ve modülleri ekle
builder.Services.AddOrchardCms();

var app = builder.Build();

// Orchard Core'u pipeline'a ekle
app.UseOrchardCore();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
//app.MapControllers();


// Özel rota tanýmlamadan modülün rotalarýný kullanabilmek için bu yeterlidir
app.Run();
