using ApiForCrud.Helper;
using ApiForCrud.Model;
using ApiForCrud.Services;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using static System.Net.WebRequestMethods;

namespace ApiForCrud.Container
{
    public class UserServices : IUserService
    {
        private readonly AppDBContext _appDBContext;
        private readonly IEmailService _emailService;
        public UserServices(AppDBContext appDBContext, IEmailService emailService)
        {
            this._appDBContext = appDBContext;
            this._emailService = emailService;

        }
        #region Confrim User Register
        public async Task<APIResponse> ConfrimRegister(int userid, string username, string otp)
        {
            APIResponse response = new APIResponse();
            bool OtpResponse = await ValidateOtp(username, otp);
            if (!OtpResponse)
            {
                response.Result = "Fail";
                response.Message = "Invalid Otp Code";
            }
            else
            {
                var _TempData = await this._appDBContext.tempUsers.FirstOrDefaultAsync(item => item.Id == userid);
                var _User = new User()
                {
                    username = username,
                    name = _TempData.name,
                    password = _TempData.password,
                    email = _TempData.email,
                    phone = _TempData.phone,
                    failattempt = 0,
                    isactive = true,
                    islocked = false,
                    role = "User"

                };
                await this._appDBContext.users.AddAsync(_User);
                await this._appDBContext.SaveChangesAsync();
                await UpdatePasswordManage(username, _TempData.password);
                response.Result = "pass";
                response.Message = "Register SuccessFully";
            }
            return response;
        }
        #endregion

        #region Insert Temp User Register
        public async Task<APIResponse> UserRegisteration(UserRegister userRegister)
        {
            APIResponse response = new APIResponse();
            int userid = 0;
            bool isValid = true;
            try
            {
                //Duplicate User && Email Verfication
                var _UserName = await this._appDBContext.users.Where(item => item.username == userRegister.UserName).ToListAsync();
                if (_UserName.Count > 0)
                {
                    isValid = false;
                    response.Result = "Fail";
                    response.Message = "This username already exits";
                }

                var _UserEmail = await this._appDBContext.users.Where(item => item.email == userRegister.Email).ToListAsync();
                if (_UserEmail.Count > 0)
                {
                    isValid = false;
                    response.Result = "Fail";
                    response.Message = "This Email already exits";
                }

                if (userRegister != null && isValid)
                {
                    var _tempUser = new TempUser()
                    {
                        code = userRegister.UserName,
                        name = userRegister.Name,
                        email = userRegister.Email,
                        password = userRegister.Password,
                        phone = userRegister.PhoneNumber,

                    };
                    await this._appDBContext.tempUsers.AddAsync(_tempUser);
                    await this._appDBContext.SaveChangesAsync();
                    string OtpText = GetRandomNumber();
                    // Send OTP via email
                    //var mailRequest = new MailRequest
                    //{
                    //    ToEmail = userRegister.Email,
                    //    Subject = "Thank You for Registeration",
                    //    Body = $"Here is Your OTP: {OtpText} for complete Registeration"
                    //};
                    //await _emailService.SendEmailAsync(mailRequest);
                    await this.UpdateOtp(userRegister.UserName, OtpText, "Register");
                    await SendOtpMail(userRegister.Email, OtpText, userRegister.Name);
                    response.Result = "pass";
                    userid = _tempUser.Id;
                    response.Message = userid.ToString();
                }
            }
            catch (Exception ex)
            {
                response = new APIResponse();
            }
            return response;
        }
        #endregion

        #region Update Otp Code
        private async Task UpdateOtp(string username, string otptext, string otptype)
        {
            var _otp = new OtpManager()
            {
                username = username,
                Otptext = otptext,
                expiration = DateTime.Now.AddMinutes(30),
                Createdate = DateTime.Now,
                OtpType = otptype
            };
            await this._appDBContext.otpManagers.AddAsync(_otp);
            await this._appDBContext.SaveChangesAsync();
        }
        #endregion

        #region For Random Number Otp

        private string GetRandomNumber()
        {
            Random random = new Random();
            string randomNumber = random.Next(0, 1000000).ToString("D6");
            return randomNumber;
        }
        #endregion

        #region for Validate Otp
        private async Task<bool> ValidateOtp(string username, string OtpText)
        {
            bool response = false;
            var currentTime = DateTime.Now;
            var currentTimeString = currentTime.ToString("yyyy-MM-dd HH:mm:ss");
            var _data = await this._appDBContext.otpManagers.FirstOrDefaultAsync(item => item.Otptext == OtpText && item.username == username && item.expiration> currentTime);
             
            if (_data != null)
            {
                response = true;
              }
            return response;
        }
        #endregion

        #region Update password

        private async Task UpdatePasswordManage(string username, string password)
        {
            var _PasswordPass = new PasswordManager()
            {
                username = username,
                password = password,
                ModifyDate = DateTime.Now,
            };
            await this._appDBContext.passwordManagers.AddAsync(_PasswordPass);
            await this._appDBContext.SaveChangesAsync();

        }

        #endregion

        #region password reset
        public async Task<APIResponse> ResetPassword(string username, string oldPassowrd, string newpassword)
        {
            APIResponse response = new APIResponse();

            var _users = await this._appDBContext.users.FirstOrDefaultAsync(item => item.username == username && item.password == oldPassowrd && item.isactive == true);
            if (_users != null)
            {
                var _passwordHistory = await ValidatePasswordHistory(username, newpassword);
                if (_passwordHistory)
                {
                    response.Result = "fail";
                    response.Message = "Dont use the Same Password that Used last three Transaction";
                }
                else
                {

                    _users.password = newpassword;
                    await this._appDBContext.SaveChangesAsync();
                    await UpdatePasswordManage(username, newpassword);
                    response.Result = "pass";
                    response.Message = "Your Password Has been Updated";
                }

            }
            else
            {
                response.Result = "fail";
                response.Message = "your have not been updated ! something Wrong";
            }

            return response;
        }
        #endregion


        private async Task<bool> ValidatePasswordHistory(string username, string password)
        {
            bool respone = false;
            var _pwd = await this._appDBContext.passwordManagers.Where(item => item.username == username).OrderByDescending(p => p.ModifyDate).Take(3).ToListAsync();
            if (_pwd.Count > 0)
            {
                var Validate = _pwd.Where(o => o.password == password);
                if (Validate.Any())
                {
                    respone = true;
                }
            }
            return respone;
        }

        public async Task<APIResponse> ForgetPassword(string username)
        {
            APIResponse response = new APIResponse();
            var _user = await this._appDBContext.users.FirstOrDefaultAsync(item => item.username == username && item.isactive == true);
            if (_user != null)
            {
                string otptext = GetRandomNumber();
                await UpdateOtp(username, otptext, "forgetpassword");
                await SendOtpMail(_user.email, otptext, _user.name);
                response.Result = "pass";
                response.Message = "OTP sent";

            }
            else
            {
                response.Result = "fail";
                response.Message = "Invalid User";
            }
            return response;
        }

        public async Task<APIResponse> UpdatePassword(string username, string password, string otpText)
        {
            APIResponse response = new APIResponse();

            bool otpvalidation = await ValidateOtp(username, otpText);
            if (otpvalidation)
            {
                bool pwdhistory = await ValidatePasswordHistory(username, password);
                if (pwdhistory)
                {
                    response.Result = "fail";
                    response.Message = "Don't use the same password that used in last 3 transaction";
                }
                else
                {
                    var _user = await this._appDBContext.users.FirstOrDefaultAsync(item => item.username == username && item.isactive == true);
                    if (_user != null)
                    {
                        _user.password = password;
                        await this._appDBContext.SaveChangesAsync();
                        await UpdatePasswordManage(username, password);
                        response.Result = "pass";
                        response.Message = "Password changed";
                    }
                }
            }
            else
            {
                response.Result = "fail";
                response.Message = "Invalid OTP";
            }
            return response;
        }
        private async Task SendOtpMail(string useremail, string OtpText, string Name)
        {

        }

        public async  Task<APIResponse> UpdateStatus(string username, bool userstatus)
        {
            APIResponse response = new APIResponse();
            var _users = await this._appDBContext.users.FirstOrDefaultAsync(item => item.username == username);
            if(_users != null )
            {
                _users.isactive = userstatus;
                await this._appDBContext.SaveChangesAsync();

                response.Result = "pass";
                response.Message = "user Status are change Now";
            }
            else
            {
                response.Result = "Fail";
                response.Message = "Invalid User";
            }

            return response;
        }

        public async Task<APIResponse> UpdateRole(string username, string userrole)
        {

             APIResponse response = new APIResponse();
            var _users = await this._appDBContext.users.FirstOrDefaultAsync(item => item.username == username && item.isactive == true);
            if (_users != null)
            {
                _users.role = userrole;
                await this._appDBContext.SaveChangesAsync();

                response.Result = "pass";
                response.Message = "user role are change Now";
            }
            else
            {
                response.Result = "Fail";
                response.Message = "Invalid User";
            }

            return response;
        }
    }
}
