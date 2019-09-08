using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBudget.Models
{
    public class BudgetDataAcessLayer
    {
        BudgetDBContext db = new BudgetDBContext();

        public IEnumerable<BudgetReport> GetAllExpences()
        {
            try
            {
                return db.BudgetReport.ToList();
            }
            catch
            {
                throw;
            }
        }

        // To filter out the records based on the search string
        public IEnumerable<BudgetReport> GetSearchResult(string searchString)
        {
            List<BudgetReport> exp = new List<BudgetReport>();
            try
            {
                exp = GetAllExpences().ToList();
                return exp.Where(x => x.ItemName.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) != -1);
            }
            catch
            {
                throw;
            }
        }

        //To Add new Expense record       
        public void AddBudget(BudgetReport budget)
        {
            try
            {
                db.BudgetReport.Add(budget);
                db.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
        //To Update the records of a particluar budget  
        public int UpdateBudget(BudgetReport budget)
        {
            try
            {
                db.Entry(budget).State = EntityState.Modified;
                db.SaveChanges();
                return 1;
            }
            catch
            {
                throw;
            }
        }
        //Get the data for a particular budget  
        public BudgetReport GetExpenseData(int id)
        {
            try
            {
                BudgetReport budget = db.BudgetReport.Find(id);
                return budget;
            }
            catch
            {
                throw;
            }
        }
        //To Delete the record of a particular budget  
        public void DeleteBudget(int id)
        {
            try
            {
                BudgetReport emp = db.BudgetReport.Find(id);
                db.BudgetReport.Remove(emp);
                db.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
        // To calculate last six months budget
        public Dictionary<string, decimal> CalculateMonthlyExpense()
        {
            BudgetDataAcessLayer objexpense = new BudgetDataAcessLayer();
            List<BudgetReport> lstEmployee = new List<BudgetReport>();
            Dictionary<string, decimal> dictMonthlySum = new Dictionary<string, decimal>();
            decimal foodSum = db.BudgetReport.Where
                (cat => cat.Category == "Food" && (cat.ExpenseDate > DateTime.Now.AddMonths(-7)))
                .Select(cat => cat.Amount)
                .Sum();
            decimal shoppingSum = db.BudgetReport.Where
               (cat => cat.Category == "Shopping" && (cat.ExpenseDate > DateTime.Now.AddMonths(-7)))
               .Select(cat => cat.Amount)
               .Sum();
            decimal travelSum = db.BudgetReport.Where
               (cat => cat.Category == "Travel" && (cat.ExpenseDate > DateTime.Now.AddMonths(-7)))
               .Select(cat => cat.Amount)
               .Sum();
            decimal healthSum = db.BudgetReport.Where
               (cat => cat.Category == "Health" && (cat.ExpenseDate > DateTime.Now.AddMonths(-7)))
               .Select(cat => cat.Amount)
               .Sum();
            dictMonthlySum.Add("Food", foodSum);
            dictMonthlySum.Add("Shopping", shoppingSum);
            dictMonthlySum.Add("Travel", travelSum);
            dictMonthlySum.Add("Health", healthSum);
            return dictMonthlySum;
        }
        // To calculate last four weeks budget
        public Dictionary<string, decimal> CalculateWeeklyExpense()
        {
            BudgetDataAcessLayer objexpense = new BudgetDataAcessLayer();
            List<BudgetReport> lstEmployee = new List<BudgetReport>();
            Dictionary<string, decimal> dictWeeklySum = new Dictionary<string, decimal>();
            decimal foodSum = db.BudgetReport.Where
                (cat => cat.Category == "Food" && (cat.ExpenseDate > DateTime.Now.AddDays(-7)))
                .Select(cat => cat.Amount)
                .Sum();
            decimal shoppingSum = db.BudgetReport.Where
               (cat => cat.Category == "Shopping" && (cat.ExpenseDate > DateTime.Now.AddDays(-28)))
               .Select(cat => cat.Amount)
               .Sum();
            decimal travelSum = db.BudgetReport.Where
               (cat => cat.Category == "Travel" && (cat.ExpenseDate > DateTime.Now.AddDays(-28)))
               .Select(cat => cat.Amount)
               .Sum();
            decimal healthSum = db.BudgetReport.Where
               (cat => cat.Category == "Health" && (cat.ExpenseDate > DateTime.Now.AddDays(-28)))
               .Select(cat => cat.Amount)
               .Sum();
            dictWeeklySum.Add("Food", foodSum);
            dictWeeklySum.Add("Shopping", shoppingSum);
            dictWeeklySum.Add("Travel", travelSum);
            dictWeeklySum.Add("Health", healthSum);
            return dictWeeklySum;
        }
    }
}