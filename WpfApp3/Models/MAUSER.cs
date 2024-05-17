using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WpfApp3.Models
{

    [Table("MAUSERS")]
    public partial class MAUSER
    {
        public int ID { get; set; }

        [StringLength(100)]
        public string? FullName { get; set; }

        [StringLength(50)]
        public string? USERNAME { get; set; }

        [StringLength(100)]
        public string? UPASSWD { get; set; }

        public DateTime? CDATE { get; set; }

        public bool? STATUS { get; set; }

        [StringLength(100)]
        public string? EMAIL { get; set; }

        [StringLength(15)]
        public string? MOBILENO { get; set; }
    }
}
