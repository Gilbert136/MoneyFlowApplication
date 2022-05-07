using MoneyFlowApplication.Entity.Base;

namespace MoneyFlowApplication.Entity
{
    /// <summary>
    /// 
    /// </summary>
    public class Customer : EntityBase
    {
        public string Code { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
