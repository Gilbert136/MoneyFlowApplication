using MoneyFlowApplication.Entity.Base;

namespace MoneyFlowApplication.Entity
{
    /// <summary>
    /// 
    /// </summary>
    public class BankStatement : EntityBase
    {
        public int CustomerId { get; set; }
        public virtual Customer? Customer { get; set; }
        public virtual List<Transaction>? Transactions { get; set; }
    }
}
