using System.ComponentModel.DataAnnotations;

namespace ApiForCrud.Model
{
    public class Employee
    {
        [Key]
        public int id { get; set; }
        public string Name {  get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int Salary { get; set; }
        public int Age{ get; set; }
    }
}
    