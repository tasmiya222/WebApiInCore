using ApiForCrud.Helper;
using ApiForCrud.Model;
using System.Globalization;

namespace ApiForCrud.Services
{
    public interface IUserRoleService
    {
        Task<APIResponse> AssignRolePermissin(List<RolePermission> _data);
        Task<List<Role>> GetAllRole();

        Task<List<Menu>> GetAllMenus();

        Task<List<AppMenu>> GetAllMenubyrole(string userrole);

        Task<MenuPermission> GetMenuPermissionByRole(string userrole, string menucode);

    }
}
