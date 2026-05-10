import { BaseTransaction } from "./base-transaction.js";
export class NewTransaction {
    constructor(statusPaidId) {
        this.statusPaidId = statusPaidId;
        this.baseTransaction = new BaseTransaction({
            statusPaidId: this.statusPaidId,
            isReadonlyOnPaid: false
        });
    }
    init() {
        this.baseTransaction.init();
    }
}
document.addEventListener("DOMContentLoaded", () => {
    const root = document.getElementById("transaction-new-root");
    if (!root)
        return;
    const controller = new NewTransaction(root.dataset.statusPaidId);
    controller.init();
});
//# sourceMappingURL=new-transaction.js.map