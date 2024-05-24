using System.ComponentModel.DataAnnotations;

namespace ApiForCrud.Model
{
    public class OtpManager
    {
        [Key]
        public int id { get; set; }
        public string username { get; set; }
        public string Otptext { get; set; }
        public string OtpType { get; set; }
        public DateTime expiration { get; set; }
        public DateTime Createdate { get; set; }
    }
}
