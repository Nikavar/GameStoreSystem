using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Model.Models
{
    public class RoleAccount
    {
        [Key]
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int RoleId { get; set; }       

        // relations
        public Account? Account { get; set; }
        public Role? Role { get; set; }
    }
}
