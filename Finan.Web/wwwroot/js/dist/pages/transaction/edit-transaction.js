import { BaseTransaction } from "./base-transaction.js";
export class EditTransaction {
    constructor(statusPaidId, statusId) {
        this.statusPaidId = statusPaidId;
        this.statusId = statusId;
        this.baseTransaction = new BaseTransaction({
            statusPaidId: this.statusPaidId,
            isReadonlyOnPaid: this.statusId === this.statusPaidId
        });
    }
    init() {
        this.baseTransaction.init();
    }
}
document.addEventListener("DOMContentLoaded", () => {
    const root = document.getElementById("transaction-edit-root");
    if (!root)
        return;
    const controller = new EditTransaction(root.dataset.statusPaidId, root.dataset.statusId);
    controller.init();
});
//# sourceMappingURL=edit-transaction.js.map