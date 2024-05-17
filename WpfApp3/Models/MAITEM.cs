using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WpfApp3.Models
{
    [Table("MAITEMS")]
    public partial class MAITEM
    {
        public int ID { get; set; }

        [StringLength(100)]
        public string? ITEMNAME { get; set; }

        [StringLength(50)]
        public string? UNIT { get; set; }

        [StringLength(50)]
        public string? CODE { get; set; }

        [StringLength(50)]
        public string? ARTICLENO { get; set; }

        public DateTime? CDATE { get; set; }

        public bool? STATUS { get; set; }

        [StringLength(255)]
        public string? REMARKS { get; set; }
    }
}
