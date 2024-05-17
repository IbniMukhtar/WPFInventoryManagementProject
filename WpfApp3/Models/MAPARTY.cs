using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WpfApp3.Models
{

    [Table("MAPARTIES")]
    public partial class MAPARTY
    {
        public int ID { get; set; }

        [StringLength(100)]
        public string? PNAME { get; set; }

        [StringLength(150)]
        public string? ADDRESS { get; set; }

        [StringLength(15)]
        public string? MOBILENO { get; set; }

        [StringLength(100)]
        public string? CONPERSON { get; set; }

        [StringLength(100)]
        public string? CITY { get; set; }

        [StringLength(100)]
        public string? STATE { get; set; }

        public bool? STATUS { get; set; }

        public DateTime? CDATE { get; set; }
    }
}
