using Finan.Web.Clients;
using Finan.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace Finan.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITransactionClient _TransactionClient;

        public HomeController(ILogger<HomeController> logger, ITransactionClient TransactionClient)
        {
            _logger = logger;
            _TransactionClient = TransactionClient;
        }
        [Authorize]
        public async Task<IActionResult> IndexAsync(DashBoardViewModel dashBoardViewModel)
        {
            //var months = System.Globalization.DateTimeFormatInfo.CurrentInfo.MonthNames
            //            .Where(m => !string.IsNullOrEmpty(m))
            //            .Select((nome, index) => new SelectListItem { Value = (index + 1).ToString(), Text = char.ToUpper(nome[0]) + nome.Substring(1) });

            //ViewBag.Months = months;

            //var years = Enumerable.Range(DateTime.Now.Year - 5, 6).Select(x => new SelectListItem { Value = x.ToString(), Text = x.ToString() });

            //ViewBag.Years = years;

            //ViewBag.SelectedMonth = dashBoardViewModel.Month > 0 ? dashBoardViewModel.Month : DateTime.Now.Month;
            //ViewBag.SelectedYear = dashBoardViewModel.Year > 0 ? dashBoardViewModel.Year : DateTime.Now.Year;

            //int month = dashBoardViewModel.Month > 0 ? dashBoardViewModel.Month : DateTime.Now.Month;
            //int year = dashBoardViewModel.Year > 0 ? dashBoardViewModel.Year : DateTime.Now.Year;

            //var receivableSumamary = await _receivableClient.GetReceivableSummaryByMonthYear(month, year);
            //var TransactionSummary = await _TransactionClient.GetTransactionSummaryByMonthYear(month, year);

            //ViewBag.ReceivableSummary = receivableSumamary;
            //ViewBag.TransactionSummary = TransactionSummary;
            //ViewBag.Balance = receivableSumamary.TotalAmount - TransactionSummary.TotalAmount;

            //var receivableSummaryClassification = await _receivableClient.GetReceivableSummaryClassificationByMonthYear(month, year);
            //var TransactionSummaryClassification = await _TransactionClient.GetTransactionSummaryClassificationByMonthYear(month, year);

            //ViewBag.ReceivableSummaryClassification = receivableSummaryClassification;
            //ViewBag.TransactionSummaryClassification = TransactionSummaryClassification;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
