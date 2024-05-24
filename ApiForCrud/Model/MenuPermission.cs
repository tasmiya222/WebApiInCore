using System.ComponentModel.DataAnnotations.Schema;

namespace ApiForCrud.Model
{
    public class MenuPermission
    {
        public string code { get;set; }
        public string Name { get; set; }
        [Column("haveview")]
        public bool haveView { get; set; }
        public bool haveAdd { get; set; }
        public bool haveedit { get; set; }
        public bool havedelete { get; set; }
    }
}
