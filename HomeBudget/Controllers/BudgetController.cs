using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeBudget.Models;
using Microsoft.AspNetCore.Mvc;

namespace HomeBudget.Controllers
{
    public class BudgetController : Controller
    {
        BudgetDataAcessLayer objexpense = new BudgetDataAcessLayer();
        public IActionResult Index(string searchString)
        {
            List<BudgetReport> lstEmployee = new List<BudgetReport>();
            lstEmployee = objexpense.GetAllExpences().ToList();
            if (!String.IsNullOrEmpty(searchString))
            {
                lstEmployee = objexpense.GetSearchResult(searchString).ToList();
            }
            return View(lstEmployee);
        }
        public ActionResult AddEditExpenses(int itemId)
        {
            BudgetReport model = new BudgetReport();
            if (itemId > 0)
            {
                model = objexpense.GetExpenseData(itemId);
            }
            return PartialView("_expenseForm", model);
        }
        [HttpPost]
        public ActionResult Create(BudgetReport newExpense)
        {
            if (ModelState.IsValid)
            {
                if (newExpense.ItemId > 0)
                {
                    objexpense.UpdateBudget(newExpense);
                }
                else
                {
                    objexpense.AddBudget(newExpense);
                }
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            objexpense.DeleteBudget(id);
            return RedirectToAction("Index");
        }
        public ActionResult BudgetSummary()
        {
            return PartialView("_BudgetReport");
        }
        public JsonResult GetMonthlyBudget()
        {
            Dictionary<string, decimal> monthlyExpense = objexpense.CalculateMonthlyExpense();
            return new JsonResult(monthlyExpense);
        }
        public JsonResult GetWeeklyBudget()
        {
            Dictionary<string, decimal> weeklyExpense = objexpense.CalculateWeeklyExpense();
            return new JsonResult(weeklyExpense);
        }
    }
}