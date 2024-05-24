using System.ComponentModel.DataAnnotations;

namespace ApiForCrud.Model
{
    public class Menu
    {
        [Key]
        public string CodeId { get; set; }
        public string Name { get; set; }
        public int status { get; set; }
    }
}
