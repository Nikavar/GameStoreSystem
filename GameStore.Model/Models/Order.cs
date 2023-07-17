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

        [Required]
        public string? PaymentType { get; set; }

        [MaxLength(length:600)]
        public string? Comments { get; set; }       
        
        // relations
        public Account? Account { get; set; }
        public ICollection<Card>? Cards { get; set; }
    }
}
