
using ApiForCrud.Helper;
using ApiForCrud.Model;

namespace ApiForCrud.Services;

public interface IUserService
{
    Task<APIResponse> UserRegisteration(UserRegister userRegister);

    Task<APIResponse> ConfrimRegister(int userid, string username, string otp);

    Task<APIResponse> ResetPassword(string username,string oldPassowrd , string newpassword);

    Task<APIResponse> ForgetPassword(string username);

    Task<APIResponse> UpdatePassword(string username, string password, string otpText);

    Task<APIResponse> UpdateStatus(string username , bool userstatus);

    Task<APIResponse> UpdateRole(string username, string userrole);


}
