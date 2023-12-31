﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Model.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string? RoleName { get; set; }       

        // relations
        public ICollection<RoleAccount>? RoleAccounts { get; set; }
    }
}
