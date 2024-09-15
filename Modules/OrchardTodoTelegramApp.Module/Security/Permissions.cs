using OrchardCore.Security.Permissions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrchardTodoTelegramApp.Module.Security
{
    public class Permissions : IPermissionProvider
    {
        public static readonly Permission ManageTodos = new Permission("ManageTodos", "Manage Todo items");

        public Task<IEnumerable<Permission>> GetPermissionsAsync()
        {
            return Task.FromResult(new[]
            {
                ManageTodos
            } as IEnumerable<Permission>);
        }

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes()
        {
            return new[]
            {
                new PermissionStereotype
                {
                    Name = "Administrator",
                    Permissions = new[] { ManageTodos }
                }
            };
        }
    }
}
