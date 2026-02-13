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
    /// Transaction Controller
    /// Handles CRUD operations for income and expense transactions
    /// </summary>
    [AuthorizeUser]
    public class TransactionController : BaseController
    {
        private readonly ITransactionService _transactionService;
        private readonly IUnitOfWork _unitOfWork;

        public TransactionController(ITransactionService transactionService, IUnitOfWork unitOfWork)
        {
            _transactionService = transactionService;
            _unitOfWork = unitOfWork;
        }

        // GET: Transaction/Index
        public ActionResult Index(DateTime? startDate, DateTime? endDate, int? categoryId, TransactionType? type)
        {
            var userId = GetCurrentUserId();
            var transactions = _transactionService.GetUserTransactions(userId).ToList();

            // Apply filters
            if (startDate.HasValue)
                transactions = transactions.Where(t => t.Date >= startDate.Value).ToList();

            if (endDate.HasValue)
                transactions = transactions.Where(t => t.Date <= endDate.Value).ToList();

            if (categoryId.HasValue)
                transactions = transactions.Where(t => t.CategoryId == categoryId.Value).ToList();

            if (type.HasValue)
                transactions = transactions.Where(t => t.Type == type.Value).ToList();

            var viewModel = new TransactionHistoryViewModel
            {
                Transactions = transactions,
                Categories = _unitOfWork.Categories.GetAll().ToList(),
                StartDate = startDate,
                EndDate = endDate,
                CategoryId = categoryId,
                Type = type
            };

            return View(viewModel);
        }

        // GET: Transaction/Create
        [HttpGet]
        public ActionResult Create()
        {
            var viewModel = new TransactionViewModel
            {
                AvailableCategories = _unitOfWork.Categories.GetAll().ToList(),
                Date = DateTime.Today
            };

            return View(viewModel);
        }

        // POST: Transaction/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TransactionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.AvailableCategories = _unitOfWork.Categories.GetAll().ToList();
                return View(model);
            }

            var userId = GetCurrentUserId();
            var success = _transactionService.CreateTransaction(model, userId);

            if (!success)
            {
                ModelState.AddModelError("", "Failed to create transaction");
                model.AvailableCategories = _unitOfWork.Categories.GetAll().ToList();
                return View(model);
            }

            TempData["SuccessMessage"] = "Transaction added successfully!";
            return RedirectToAction("Index");
        }

        // POST: Transaction/CreateAjax (for AJAX requests)
        [HttpPost]
        public JsonResult CreateAjax(TransactionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Invalid data" });
            }

            var userId = GetCurrentUserId();
            var success = _transactionService.CreateTransaction(model, userId);

            if (success)
            {
                return Json(new { success = true, message = "Transaction added successfully!" });
            }

            return Json(new { success = false, message = "Failed to add transaction" });
        }

        // GET: Transaction/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var userId = GetCurrentUserId();
            var transaction = _transactionService.GetById(id, userId);

            if (transaction == null)
            {
                return HttpNotFound();
            }

            var viewModel = new TransactionViewModel
            {
                TransactionId = transaction.TransactionId,
                Description = transaction.Description,
                Amount = transaction.Amount,
                CategoryId = transaction.CategoryId,
                Type = transaction.Type,
                Date = transaction.Date,
                AvailableCategories = _unitOfWork.Categories.GetAll().ToList()
            };

            return View(viewModel);
        }

        // POST: Transaction/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TransactionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.AvailableCategories = _unitOfWork.Categories.GetAll().ToList();
                return View(model);
            }

            var userId = GetCurrentUserId();
            var success = _transactionService.UpdateTransaction(model, userId);

            if (!success)
            {
                ModelState.AddModelError("", "Failed to update transaction");
                model.AvailableCategories = _unitOfWork.Categories.GetAll().ToList();
                return View(model);
            }

            TempData["SuccessMessage"] = "Transaction updated successfully!";
            return RedirectToAction("Index");
        }

        // POST: Transaction/Delete/5
        [HttpPost]
        public JsonResult Delete(int id)
        {
            var userId = GetCurrentUserId();
            var success = _transactionService.DeleteTransaction(id, userId);

            if (success)
            {
                return Json(new { success = true, message = "Transaction deleted successfully!" });
            }

            return Json(new { success = false, message = "Failed to delete transaction" });
        }
    }
}
