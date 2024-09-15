using OrchardCore.Navigation;
using Microsoft.Extensions.Localization;  // IStringLocalizer'ı ekliyoruz
using System.Threading.Tasks;

namespace OrchardTodoTelegramApp.Module
{
    public class AdminMenu : INavigationProvider
    {
        private readonly IStringLocalizer T;  // IStringLocalizer alanı

        public AdminMenu(IStringLocalizer<AdminMenu> localizer)
        {
            T = localizer;  // Constructor'da IStringLocalizer'ı kullan
        }

        public ValueTask BuildNavigationAsync(string name, NavigationBuilder builder)
        {
            if (!string.Equals(name, "admin", System.StringComparison.OrdinalIgnoreCase))
            {
                return ValueTask.CompletedTask;
            }

            builder
                .Add(T["Orchard To-Do App Management"], menu => menu  // T[""] yerelleştirme için kullanılıyor
                    .AddClass("todo")
                    .Action("Index", "Todo", new { area = "OrchardTodoTelegramApp.Module" })
                    .LocalNav()
                );

            return ValueTask.CompletedTask;
        }
    }
}
