using MoneyFlowApplication.Entity.Base;
using MoneyFlowApplication.Enums;

namespace MoneyFlowApplication.Entity
{
    public class Transaction : EntityBase
    {
        public string Description { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string ReferenceNumber { get; set; }
        public string CurrencyCode { get; set; }
        public TransactionType Type { get; set; }
        public int BankStatementId { get; set; }
        public virtual BankStatement? BankStatement { get; set; }
    }

    public class TransactionDeserialize
    {
        public string desc { get; set; }
        public string amount { get; set; }
        public DateTime date { get; set; }
        public string type { get; set; }
    }

    public class TransactionsDeserialized
    {
        public List<TransactionDeserialize>? data { get; set; }
    }
}