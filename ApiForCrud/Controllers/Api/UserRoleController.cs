using ApiForCrud.Model;
using ApiForCrud.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiForCrud.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {

        private readonly IUserRoleService userRoleService;

        public UserRoleController(IUserRoleService _userroleservice)
        {
            userRoleService = _userroleservice;
        }
        [HttpPost("assignpermission")]
        public async Task<IActionResult> assignpermission(List<RolePermission> rolepermission)
        {
            var data = await this.userRoleService.AssignRolePermissin(rolepermission);
            return Ok(data);
        }

        [HttpGet("GetAllMenus")]
        public async Task<IActionResult> GetAllMenus()
        {
            var data = await this.userRoleService.GetAllMenus();
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpGet("GetAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var data = await this.userRoleService.GetAllRole();
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }
    }


}

