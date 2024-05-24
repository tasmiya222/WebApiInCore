using System.ComponentModel.DataAnnotations;

namespace ApiForCrud.Model
{
    public class User
    {
        [Key]
        public int id { get; set; }
        public string username { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string password { get; set; }
        public bool isactive { get; set; }
        public string role { get; set; }
        public bool islocked { get; set; }
        public int failattempt { get; set; }
    }
}
