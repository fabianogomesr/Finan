export interface TransactionConfig {
    statusPaidId: string;
    isReadonlyOnPaid: boolean;
}
export declare class BaseTransaction {
    private config;
    private utils;
    private statusDropdown;
    private paymentSection;
    private paidAmountInput;
    private paymentDateInput;
    private paymentAccountInput;
    private valueInput;
    private discountInput;
    private lateFeeInput;
    constructor(config: TransactionConfig);
    init(): void;
    private bindDates;
    private bindDropdowns;
    private bindEvents;
    private toggle;
    private autoFill;
    private setRequired;
    private clear;
    private setReadonly;
}
//# sourceMappingURL=base-transaction.d.ts.map