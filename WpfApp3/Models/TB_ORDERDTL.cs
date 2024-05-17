using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.ObjectModel;

namespace WpfApp3.Models
{
    public class TB_ORDERDTL
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int MId { get; set; }
        public int ItemID { get; set; }

        [NotMapped]
        public string? ItemName {  get; set; }   
        public string? Unit { get; set; }
        public int Quantity { get; set; }

        [NotMapped]
        public string? SelectedItemName { get; set; }

        [NotMapped]
        public ObservableCollection<TB_ORDERDTL>? OrderDetails { get; set; }

    }
}
