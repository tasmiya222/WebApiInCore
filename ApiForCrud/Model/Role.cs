using System.ComponentModel.DataAnnotations;

namespace ApiForCrud.Model
{
    public class Role
    {
        [Key]
        public int id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public bool status { get; set; }
    }
}
