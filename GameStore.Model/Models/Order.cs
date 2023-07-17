using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Model.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string? PaymentType { get; set; }
        public string? Comments { get; set; }       
        
        // relations
        public Account? Account { get; set; }
        public ICollection<Card>? Cards { get; set; }
    }
}
