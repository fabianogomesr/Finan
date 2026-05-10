using Finan.Contracts.Filters;
using Finan.Contracts.Request;
using Finan.Contracts.Response;
using Finan.Web.Clients;
using Finan.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Finan.Web.Controllers
{
    [Authorize]
    public class TransactionController : BaseController
    {
        private ITransactionClient _TransactionClient;
        private IGroupClient _GroupClient;
        private IClassificationClient _ClassificationClient;
        private ICurrencyClient _CurrencyClient;
        private ICostCenterClient _CostCenterClient;
        private IAccountClient _AccountClient;

        public TransactionController(ITransactionClient TransactionClient,
            IGroupClient groupClient,
            IClassificationClient ClassificationClient,
            ICurrencyClient currencyClient,
            ICostCenterClient costCenterClient,
            IAccountClient accountClient,
            IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _TransactionClient = TransactionClient;
            _GroupClient = groupClient;
            _ClassificationClient = ClassificationClient;
            _CurrencyClient = currencyClient;
            _CostCenterClient = costCenterClient;
            _AccountClient = accountClient;
        }

        public async Task<IActionResult> IndexAsync(TransactionFilter filter)
        {
            var response = await _TransactionClient.GetPageAsync(filter);

            if (response.Unauthorized)
                return await TreatResponseAsync(response);

            if (response.Data.Page > 1 && !response.Data.Items.Any())
                return RedirectToAction("Index", new { pageNumber = 1 });

            if (response.Data != null)
                SetTransactionStatusCounts(response);

            var dateTypes = await _TransactionClient.GetDateTypeList();

            ViewBag.Canceled = filter.Canceled;
            ViewBag.DateTypes = dateTypes.Data?.Select(x => new SelectListItem { Value = x.TypeId.ToString(), Text = x.Name });
            ViewBag.DefaultDateType = filter.DateType;
            ViewBag.DefaultStartDate = filter.StartDate;
            ViewBag.DefaultEndDate = filter.EndDate;

            return await TreatResponseAsync(response);
        }

        private void SetTransactionStatusCounts(ApiResponse<PagedResponse<TransactionResponse>> response)
        {   
            var TransactionList = response.Data;

            var totalEmAberto = TransactionList?.Items != null ? TransactionList.Items.Count(p => p.StatusName != "Pago" && p.DueDate >= DateTime.Now.Date) : 0;
            var totalVencidos = TransactionList?.Items != null ? TransactionList.Items.Count(p => p.StatusName != "Pago" && p.DueDate < DateTime.Now.Date) : 0;
            var totalPagos = TransactionList?.Items != null ? TransactionList.Items.Count(p => p.StatusName == "Pago") : 0;

            ViewBag.TotalEmAberto = totalEmAberto;
            ViewBag.TotalVencidos = totalVencidos;
            ViewBag.TotalPagos = totalPagos;
        }

        public async Task<IActionResult> UpdateAsync(TransactionViewModel Transaction)
        {
            if (!ModelState.IsValid)
                return ValidateModel("Edit");

            var transaction = new TransactionRequest
            {
                Id = Transaction.Id,
                Description = Transaction.Description,
                CostCenterId = Transaction.CostCenterId,
                GroupId = Transaction.GroupId,
                ClassificationId = Transaction.ClassificationId,
                CurrencyId = Transaction.CurrencyId,
                TypeId = (Contracts.Enums.TransactionType)Transaction.TypeId,
                Value = Transaction.Value,
                Discount = Transaction.Discount,
                LateFee = Transaction.LateFee,
                IssueDate = Transaction.IssueDate,
                DueDate = Transaction.DueDate,
                CashFlowDate = Transaction.CashFlowDate,
                AccrualPeriodDate = Transaction.AccrualPeriodDate,
                Observation = Transaction.Observation,
                StatusId = (Contracts.Enums.TransactionStatus)Transaction.StatusId,
                PaidTransaction = Transaction.StatusId == 2 ? new PaidTransactionRequest
                {
                    PaidDate = Transaction.PaidDate ?? DateTime.Now,
                    PaidValue = Transaction.PaidValue ?? 0,
                    AccountId = Transaction.AccountId ?? 0 // Aqui você pode adicionar a lógica para obter a conta selecionada, se necessário
                } : null
            };

            var response = await _TransactionClient.UpdateAsync(transaction);

            return await TreatResponseAsync(response, "Index");
        }

        public async Task<IActionResult> CreateAsync(TransactionViewModel Transaction)
        {
            if (!ModelState.IsValid)
                return ValidateModel("New");

            var transaction = new TransactionRequest
            {
                Id = Transaction.Id,
                Description = Transaction.Description,
                CostCenterId = Transaction.CostCenterId,
                GroupId = Transaction.GroupId,
                ClassificationId = Transaction.ClassificationId,
                CurrencyId = Transaction.CurrencyId,
                TypeId = (Contracts.Enums.TransactionType)Transaction.TypeId,
                Value = Transaction.Value,
                Discount = Transaction.Discount,
                LateFee = Transaction.LateFee,
                IssueDate = Transaction.IssueDate,
                DueDate = Transaction.DueDate,
                CashFlowDate = Transaction.CashFlowDate,
                AccrualPeriodDate = Transaction.AccrualPeriodDate,
                Observation = Transaction.Observation,
                StatusId = (Contracts.Enums.TransactionStatus)Transaction.StatusId,
                PaidTransaction = Transaction.StatusId == 2 ? new PaidTransactionRequest
                {
                    PaidDate = Transaction.PaidDate ?? DateTime.Now,
                    PaidValue = Transaction.PaidValue ?? 0,
                    AccountId = Transaction.AccountId ?? 0 // Aqui você pode adicionar a lógica para obter a conta selecionada, se necessário
                } : null
            };

            var response = await _TransactionClient.CreateAsync(transaction);

            return await TreatResponseAsync(response, "Index");
        }

        public async Task DeleteAsync(short id)
        {
            await _TransactionClient.DeleteAsync(id);
            TempData["Message"] = "Transaction delete successfully!";
            Response.Redirect("/Transaction");
        }

        public async Task<IActionResult> EditAsync(short id)
        {
            var response = await _TransactionClient.GetAsync(id);

            if(response.Data == null)
            {
                TempData["ModalTitle"] = "Erro ao processar a requisição";
                TempData["ModalType"] = "danger";
                TempData["ModalMessages"] = new List<string> { "Transação não encontrada." };
                return RedirectToAction("Index");
            }

            var financialTypes = await _TransactionClient.GetFinancialTypeList();
            var status = await _TransactionClient.GetStatusList();
            var currencies = await _CurrencyClient.GetAllAsync();
            var costcenters = await _CostCenterClient.GetAllAsync();
            var groups = await _GroupClient.GetGroupsByNature(response.Data.TypeId);
            var classifications = await _ClassificationClient.GetClassificationsFromTransactionByGroupIdAsync((int)response.Data.GroupId);
            var accounts = await _AccountClient.GetAllAsync();

            ViewBag.Types = financialTypes.Data?.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Description });
            ViewBag.Groups = groups.Data?.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Description });
            ViewBag.Classifications = classifications.Data?.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Description });
            ViewBag.Status = status.Data?.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Description });
            ViewBag.Currencies = currencies.Data?.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Code });
            ViewBag.CostCenters = costcenters.Data?.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Description });
            ViewBag.Accounts = accounts.Data?.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name });

            ViewBag.StatusPaidId = status.Data?.FirstOrDefault(s => s.Description == "Pago")?.Id;

            return await TreatResponseAsync(response);
        }

        public async Task<IActionResult> NewAsync()
        {
            var financialTypes = await _TransactionClient.GetFinancialTypeList();
            var Groups = new List<GroupResponse>();
            var Classifications = new List<ClassificationResponse>();
            var status = await _TransactionClient.GetStatusList();
            var currencies = await _CurrencyClient.GetAllAsync();
            var costcenters = await _CostCenterClient.GetAllAsync();
            var accounts = await _AccountClient.GetAllAsync();

            ViewBag.Types = financialTypes.Data?.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Description });
            ViewBag.Groups = Groups.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Description });
            ViewBag.Classifications = Classifications.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Description });
            ViewBag.Status = status.Data?.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Description });
            ViewBag.Currencies = currencies.Data?.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Code });
            ViewBag.CostCenters = costcenters.Data?.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Description });
            ViewBag.Accounts = accounts.Data?.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name });

            ViewBag.StatusPaidId = status.Data?.FirstOrDefault(s => s.Description == "Pago")?.Id;

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetClassificationsAsync(int groupId)
        {
            var classifications = await _ClassificationClient.GetClassificationsFromTransactionByGroupIdAsync(groupId);
            return Json(classifications.Data?.Select(c => new { id = c.Id, name = c.Description }).ToList());
        }

        [HttpGet]
        public async Task<IActionResult> GetGroupsAsync(int typeId)
        {
            var groups = await _GroupClient.GetGroupsByNature(typeId);
            return Json(groups.Data?.Select(c => new { id = c.Id, name = c.Description }).ToList());
        }
    }
}
