using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiForCrud.Model
{
    public  class RolePermission
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("userrole")]
        [StringLength(50)]
        public string UserRole { get; set; }
        [Column("menucode")]
        [StringLength(50)]
        public string menucode { get; set; }
        [Column("haveview")]
        public bool haveView { get; set; }
        public bool haveAdd{ get; set; }
        public bool haveedit{ get; set; }
        public bool havedelete{ get; set; }
    }
}
