using Finan.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Domain.Entities
{
    public class Statement : MultiTenantEntity
    {
        public DateTime FlowDate { get; set; }
        public DateTime? ReconciledDate { get; set; }
        public decimal Value { get; set; }
        public decimal Balance { get; set; }
        public Transaction? Transaction { get; set; }
        public int? TransactionId { get; set; }
        public BankTransaction? BankTransaction { get; set; }
        public int? BankTransactionId { get; set; }
        public Account? Account { get; set; }
        public int? AccountId { get; set; }
        public bool Reversed { get; set; }

        public Statement()
        {
        }

        public Statement(DateTime flowDate, decimal value, decimal balance, int accountId, Transaction transaction)
        {
            FlowDate = flowDate;

            if(transaction.Type == TransactionType.Expense)
            {
                Value = Math.Abs(value) * -1;
            }
            else if(transaction.Type == TransactionType.Income)
            {
                Value = Math.Abs(value);
            }
            else
            {
                throw new Exception("Tipo de transação inválida para lançamento no extrato.");
            }

            Balance = balance += Value;
            TransactionId = transaction.Id;
            AccountId = accountId;
            Reversed = false;
        }

        public Statement(DateTime flowDate, decimal value, decimal balance, int accountId, BankTransaction bankTransaction)
        {

            FlowDate = flowDate;

            if (bankTransaction.Type == BankTransactionType.Debit)
            {
                Value = Math.Abs(value) * -1;
            }
            else if (bankTransaction.Type == BankTransactionType.Credit)
            {
                Value = Math.Abs(value);
            }
            else if (bankTransaction.Type == BankTransactionType.Transfer && bankTransaction.AccountOutId == accountId)
            {
                Value = Math.Abs(value) * -1;
            }
            else if (bankTransaction.Type == BankTransactionType.Transfer && bankTransaction.AccountInId == accountId)
            {
                Value = Math.Abs(value);
            }
            else
            {
                throw new Exception("Tipo de transação bancária inválida para lançamento no extrato.");

            }

            Balance += Value;
            BankTransactionId = bankTransaction.Id;
            AccountId = accountId;
            Reversed = false;
        }
    }
}
