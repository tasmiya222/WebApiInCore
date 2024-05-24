using System.ComponentModel.DataAnnotations;

namespace ApiForCrud.Model
{
    public class PasswordManager
    {
        [Key]
        public int id { get; set; }
        public string username { get; set; }
        public string  password { get; set; }
        public DateTime ModifyDate { get; set; }
    }
}
