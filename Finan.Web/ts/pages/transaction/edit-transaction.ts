import { BaseTransaction } from "./base-transaction.js";

export class EditTransaction {

    private baseTransaction: BaseTransaction;
    constructor(private statusPaidId: string, private statusId: string) {
        this.baseTransaction = new BaseTransaction({
            statusPaidId: this.statusPaidId,
            isReadonlyOnPaid: this.statusId === this.statusPaidId
        });
    }

    public init(): void {
        this.baseTransaction.init();
    }
}

document.addEventListener("DOMContentLoaded", () => {

    const root = document.getElementById("transaction-edit-root");
    if (!root) return;

    const controller = new EditTransaction(
        root.dataset.statusPaidId!,
        root.dataset.statusId!
    );

    controller.init();
});