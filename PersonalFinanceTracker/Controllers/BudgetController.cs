using System;
using System.Linq;
using System.Web.Mvc;
using PersonalFinanceTracker.Filters;
using PersonalFinanceTracker.Models;
using PersonalFinanceTracker.Repositories;
using PersonalFinanceTracker.Services;
using PersonalFinanceTracker.ViewModels;

namespace PersonalFinanceTracker.Controllers
{
    /// <summary>
    /// Budget Controller
    /// Handles budget management and tracking
    /// </summary>
    [AuthorizeUser]
    public class BudgetController : BaseController
    {
        private readonly IBudgetService _budgetService;
        private readonly IUnitOfWork _unitOfWork;

        public BudgetController(IBudgetService budgetService, IUnitOfWork unitOfWork)
        {
            _budgetService = budgetService;
            _unitOfWork = unitOfWork;
        }

        // GET: Budget
        public ActionResult Index(int? month, int? year)
        {
            var userId = GetCurrentUserId();
            var currentMonth = month ?? DateTime.Now.Month;
            var currentYear = year ?? DateTime.Now.Year;

            var viewModel = _budgetService.GetBudgetListViewModel(userId, currentMonth, currentYear);

            return View(viewModel);
        }

        // GET: Budget/Create
        [HttpGet]
        public ActionResult Create()
        {
            var viewModel = new BudgetViewModel
            {
                AvailableCategories = _unitOfWork.Categories
                    .Find(c => c.Type == CategoryType.Expense || c.Type == CategoryType.Both)
                    .ToList(),
                Month = DateTime.Now.Month,
                Year = DateTime.Now.Year
            };

            return View(viewModel);
        }

        // POST: Budget/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BudgetViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.AvailableCategories = _unitOfWork.Categories
                    .Find(c => c.Type == CategoryType.Expense || c.Type == CategoryType.Both)
                    .ToList();
                return View(model);
            }

            var userId = GetCurrentUserId();
            var success = _budgetService.CreateBudget(model, userId);

            if (!success)
            {
                ModelState.AddModelError("", "Budget already exists for this category and month");
                model.AvailableCategories = _unitOfWork.Categories
                    .Find(c => c.Type == CategoryType.Expense || c.Type == CategoryType.Both)
                    .ToList();
                return View(model);
            }

            TempData["SuccessMessage"] = "Budget created successfully!";
            return RedirectToAction("Index");
        }

        // GET: Budget/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var userId = GetCurrentUserId();
            var budget = _budgetService.GetById(id, userId);

            if (budget == null)
            {
                return HttpNotFound();
            }

            var viewModel = new BudgetViewModel
            {
                BudgetId = budget.BudgetId,
                CategoryId = budget.CategoryId,
                LimitAmount = budget.LimitAmount,
                Month = budget.Month,
                Year = budget.Year,
                AvailableCategories = _unitOfWork.Categories
                    .Find(c => c.Type == CategoryType.Expense || c.Type == CategoryType.Both)
                    .ToList()
            };

            return View(viewModel);
        }

        // POST: Budget/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BudgetViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.AvailableCategories = _unitOfWork.Categories
                    .Find(c => c.Type == CategoryType.Expense || c.Type == CategoryType.Both)
                    .ToList();
                return View(model);
            }

            var userId = GetCurrentUserId();
            var success = _budgetService.UpdateBudget(model, userId);

            if (!success)
            {
                ModelState.AddModelError("", "Failed to update budget");
                model.AvailableCategories = _unitOfWork.Categories
                    .Find(c => c.Type == CategoryType.Expense || c.Type == CategoryType.Both)
                    .ToList();
                return View(model);
            }

            TempData["SuccessMessage"] = "Budget updated successfully!";
            return RedirectToAction("Index");
        }

        // POST: Budget/Delete/5
        [HttpPost]
        public JsonResult Delete(int id)
        {
            var userId = GetCurrentUserId();
            var success = _budgetService.DeleteBudget(id, userId);

            if (success)
            {
                return Json(new { success = true, message = "Budget deleted successfully!" });
            }

            return Json(new { success = false, message = "Failed to delete budget" });
        }
    }
}
