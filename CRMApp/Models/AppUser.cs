using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CRMApp.Models
{
    public class AppUser:IdentityUser
    {
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public bool PermissionCompany { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        [ForeignKey("Card")]
        public int CardId { get; set; }
        public Card Card { get; set; }
        public List<MonthlySalary> MonthlySalaries { get; set; }
        public int StaffContractId { get; set; }
        public StaffContract StaffContract { get; set; }

    }
}
