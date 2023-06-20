using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Model.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }        
        public string? AvatarImage { get; set; }
        public int? UserId { get; set; }       
        public User? User { get; set; }       
        public ICollection<Order>? Orders { get; set; }

    }
}
