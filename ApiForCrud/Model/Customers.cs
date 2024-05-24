using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiForCrud.Model
{
    public class Customers
    {
        [Key]
        public int Id { get; set; }
        public string code { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        [Column(TypeName = "decimal(18, 2)")] 
        public decimal Creditlimit { get; set; }
        public int IsActive { get; set; }
        public  int Taxcode { get; set; }
    }
}
