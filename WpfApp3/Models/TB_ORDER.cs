using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace WpfApp3.Models
{
    public class TB_ORDER
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 
        public int PartyId { get; set; }
        public string? Remarks { get; set; }
        public DateTime OrderDate { get; set; }
        public string? OrderNumber { get; set; }
       // public ObservableCollection<TB_ORDERDTL> OrderDetails { get; set; }
    }
}
