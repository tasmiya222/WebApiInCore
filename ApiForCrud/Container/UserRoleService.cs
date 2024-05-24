using ApiForCrud.Helper;
using ApiForCrud.Model;
using ApiForCrud.Services;
using Microsoft.EntityFrameworkCore;

namespace ApiForCrud.Container
{
    public class UserRoleService : IUserRoleService
    {
        private readonly AppDBContext _appDBContext;
        private readonly IEmailService _emailService;
        public UserRoleService(AppDBContext appDBContext, IEmailService emailService)
        {
            this._appDBContext = appDBContext;
            this._emailService = emailService;

        }
        public async Task<APIResponse> AssignRolePermissin(List<RolePermission> _data)
        {
            APIResponse response = new APIResponse();
            int processcount = 0;
            try
            {
                using (var dbtransaction = await this._appDBContext.Database.BeginTransactionAsync())
                {
                    if (_data.Count > 0)
                    {
                        _data.ForEach(item =>
                        {
                            var userdata = this._appDBContext.rolesPermission.FirstOrDefault(item1 => item1.UserRole == item.UserRole && item1.menucode == item.menucode);
                            if (userdata != null)
                            {
                                userdata.haveView = item.haveView;
                                userdata.havedelete = item.havedelete;
                                userdata.haveAdd = item.haveAdd;
                                userdata.haveedit = item.haveedit;
                                processcount++;
                            }
                            else
                            {
                                this._appDBContext.rolesPermission.Add(item);
                                processcount++;
                            }
                        });
                        if (_data.Count == processcount)
                        {
                            await this._appDBContext.SaveChangesAsync();
                            await dbtransaction.CommitAsync();
                            response.Result = "pass";
                            response.Message = "Saved successfully.";
                        }
                        else
                        {
                            await dbtransaction.RollbackAsync();

                        }

                    }
                    else
                    {
                        response.Result = "fail";
                        response.Message = "please proceed within minimum 1 record";
                    }
                }
            }
            catch (Exception ex)
            {

                response.Result = "Error";
                response.Message = ex.Message;
            }
            return response;
        }


        public async Task<List<AppMenu>> GetAllMenubyrole(string userrole)
        {
            List<AppMenu> appmenus = new List<AppMenu>();

            var accessdata = (from menus in this._appDBContext.rolesPermission.Where(o => o.UserRole == userrole && o.haveView)
                              join m in this._appDBContext.menu on menus.menucode equals m.CodeId into _jointable
                              from p in _jointable.DefaultIfEmpty()
                              select new { code = menus.menucode, name = p.Name }).ToList();
            if (accessdata.Any())
            {
                accessdata.ForEach(item =>
                {
                    appmenus.Add(new AppMenu()
                    {
                        code = item.code,
                        Name = item.name
                    });
                });
            }

            return appmenus;
        }

        public async Task<List<Menu>> GetAllMenus()
        {
            return await this._appDBContext.menu.ToListAsync();
        }

        public async Task<List<Role>> GetAllRole()
        {
            return await this._appDBContext.roles.ToListAsync();

        }

        public async Task<MenuPermission> GetMenuPermissionByRole(string userrole, string menucode)
        {
            MenuPermission menupermission = new MenuPermission();
            var _data = await this._appDBContext.rolesPermission.FirstOrDefaultAsync(o => o.UserRole == userrole && o.haveView
            && o.menucode == menucode);
            if (_data != null)
            {
                menupermission.code = _data.menucode;
                menupermission.haveView = _data.haveView;
                menupermission.haveAdd = _data.haveAdd;
                menupermission.haveedit = _data.haveedit;
                menupermission.havedelete = _data.havedelete;
            }

            return menupermission;
        }
    }
}

