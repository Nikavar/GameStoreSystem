using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Model.Models
{
    public class PaymentType
    {
        [Key]
        public int Id { get; set; }
        public string PaymentName { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
