
import { BaseTransaction } from "./base-transaction.js";

export class NewTransaction {

    private baseTransaction: BaseTransaction;
    constructor(private statusPaidId: string) {

        this.baseTransaction = new BaseTransaction({
            statusPaidId: this.statusPaidId,
            isReadonlyOnPaid: false
        });
    }

    public init(): void {
        this.baseTransaction.init();
    }
}

document.addEventListener("DOMContentLoaded", () => {

    const root = document.getElementById("transaction-new-root");
    if (!root) return;

    const controller = new NewTransaction(
        root.dataset.statusPaidId!
    );

    controller.init();
});