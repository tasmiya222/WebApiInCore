namespace ApiForCrud.Model
{
    public class UserRegister
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
    }

    public class Confirmpassword
    {
        public int userid { get; set; }
        public string username { get; set; }
        public string otptext { get; set; }
    }
}
