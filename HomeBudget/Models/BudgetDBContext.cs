using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBudget.Models
{
    public class BudgetDBContext : DbContext
    {
        public virtual DbSet<BudgetReport> BudgetReport { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=LAPTOP-JLEK9VEU\\SQLEXPRESS;Integrated Security=True");
            }
        }
    }
}

