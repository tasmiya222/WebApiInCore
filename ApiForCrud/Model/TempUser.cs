using System.ComponentModel.DataAnnotations;

namespace ApiForCrud.Model
{
    public class TempUser
    {
        [Key]
        public int Id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string password { get; set; }
    }
}
