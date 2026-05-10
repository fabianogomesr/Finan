import { Utils } from "../../shared/utils.js";
export class BaseTransaction {
    constructor(config) {
        this.config = config;
        this.utils = new Utils();
        this.statusDropdown = document.querySelector("select[name='StatusId']");
        this.paymentSection = document.querySelector("#paymentSection");
        this.paidAmountInput = document.querySelector("input[name='PaidValue']");
        this.paymentDateInput = document.querySelector("input[name='PaidDate']");
        this.paymentAccountInput = document.querySelector("select[name='AccountId']");
        this.valueInput = document.querySelector("input[name='Value']");
        this.discountInput = document.querySelector("input[name='Discount']");
        this.lateFeeInput = document.querySelector("input[name='LateFee']");
    }
    init() {
        this.bindDates();
        this.bindEvents();
        this.toggle();
        this.bindDropdowns();
    }
    bindDates() {
        const issueDate = this.utils.qs("input[name='IssueDate']");
        const accrual = this.utils.qs("input[name='AccrualPeriodDate']");
        issueDate?.addEventListener("change", () => {
            if (issueDate.value && accrual && !accrual.value) {
                accrual.value = issueDate.value;
            }
        });
        const dueDate = this.utils.qs("input[name='DueDate']");
        const cashFlowDate = this.utils.qs("input[name='CashFlowDate']");
        dueDate?.addEventListener("change", () => {
            if (dueDate.value && cashFlowDate && !cashFlowDate.value) {
                cashFlowDate.value = dueDate.value;
            }
        });
    }
    bindDropdowns() {
        const groupDropdown = this.utils.qs("select[name='GroupId']");
        const classificationDropdown = this.utils.qs("select[name='ClassificationId']");
        const typeDropdown = this.utils.qs("select[name='TypeId']");
        groupDropdown?.addEventListener("change", async () => {
            const groupId = groupDropdown.value;
            if (!classificationDropdown)
                return;
            if (groupId) {
                try {
                    const response = await fetch(`/Transaction/GetClassifications?groupId=${groupId}`);
                    const classifications = await response.json();
                    classificationDropdown.innerHTML = "";
                    this.utils.addDefaultOption(classificationDropdown, "Selecione uma classificação");
                    classifications.forEach(c => {
                        const option = new Option(c.name, String(c.id));
                        classificationDropdown.add(option);
                    });
                }
                catch (error) {
                    console.error("Erro ao carregar classificações:", error);
                }
            }
            else {
                classificationDropdown.innerHTML = "<option value=''>Selecione um Grupo primeiro</option>";
            }
        });
        typeDropdown?.addEventListener("change", async () => {
            const typeId = typeDropdown.value;
            if (!groupDropdown)
                return;
            if (typeId) {
                try {
                    const response = await fetch(`/Transaction/GetGroups?TypeId=${typeId}`);
                    const groups = await response.json();
                    groupDropdown.innerHTML = "";
                    this.utils.addDefaultOption(groupDropdown, "Selecione um grupo");
                    groups.forEach(g => {
                        const option = new Option(g.name, String(g.id));
                        groupDropdown.add(option);
                    });
                }
                catch (error) {
                    console.error("Erro ao carregar grupos:", error);
                }
            }
            else {
                groupDropdown.innerHTML = "<option value=''>Selecione um tipo primeiro</option>";
            }
        });
    }
    bindEvents() {
        this.statusDropdown?.addEventListener("change", () => this.toggle());
        [this.valueInput, this.discountInput, this.lateFeeInput]
            .forEach(input => input?.addEventListener("input", () => this.autoFill()));
        this.paidAmountInput?.addEventListener("input", () => {
            if (this.paidAmountInput) {
                this.paidAmountInput.dataset.userEdited = "true";
            }
        });
    }
    toggle() {
        if (!this.statusDropdown || !this.paymentSection)
            return;
        const isPaid = this.statusDropdown.value === this.config.statusPaidId;
        this.paymentSection.style.display = isPaid ? "block" : "none";
        if (isPaid) {
            this.setRequired(true);
            this.autoFill();
            if (this.config.isReadonlyOnPaid) {
                this.setReadonly(true);
            }
        }
        else {
            this.setRequired(false);
            this.clear();
            this.setReadonly(false);
        }
    }
    autoFill() {
        if (!this.paidAmountInput || this.paidAmountInput.value)
            return;
        const total = this.utils.parseDecimal(this.valueInput?.value ?? null);
        const discount = this.utils.parseDecimal(this.discountInput?.value ?? null);
        const lateFee = this.utils.parseDecimal(this.lateFeeInput?.value ?? null);
        const result = Math.max(0, (total - discount) + lateFee);
        this.paidAmountInput.value = this.utils.formatCurrency(result);
    }
    setRequired(required) {
        const fields = [
            this.paymentAccountInput,
            this.paymentDateInput,
            this.paidAmountInput
        ];
        fields.forEach(f => {
            if (!f)
                return;
            required ? f.setAttribute("required", "required") : f.removeAttribute("required");
        });
    }
    clear() {
        if (this.paidAmountInput)
            this.paidAmountInput.value = "";
        if (this.paymentDateInput)
            this.paymentDateInput.value = "";
        if (this.paymentAccountInput)
            this.paymentAccountInput.value = "";
    }
    setReadonly(isReadonly) {
        const fields = [
            this.paidAmountInput,
            this.paymentDateInput,
            this.paymentAccountInput
        ];
        fields.forEach(field => {
            if (!field)
                return;
            if (isReadonly) {
                field.setAttribute("disabled", "disabled");
                if (field.name) {
                    const hidden = document.createElement("input");
                    hidden.type = "hidden";
                    hidden.name = field.name;
                    hidden.value = field.value;
                    field.parentElement?.appendChild(hidden);
                }
            }
            else {
                field.removeAttribute("disabled");
            }
        });
    }
}
//# sourceMappingURL=base-transaction.js.map