using MoneyFlowApplication.Entity;
using MoneyFlowApplication.Enums;

namespace MoneyFlowApplication.Data
{
    public static class StaticData
    {
        public static List<Customer> Customers = new List<Customer>
        {
            new Customer
            {
                Id = 1,
                Code = "0001",
                FirstName = "Gilbert",
                LastName = "Adu",
                CreatedBy = "Admin",
                CreatedDate = DateTime.UtcNow,
            }
        };

        public static List<BankStatement> BankStatements = new List<BankStatement>
        {
            new BankStatement
            {
                Id = 1,
                CustomerId = 1,
                Customer = Customers?.FirstOrDefault(x => x.Id == 1) ?? new Customer(),
                CreatedBy = "Admin",
                CreatedDate = DateTime.UtcNow,
                Transactions = Transactions.Where(x => x.BankStatementId == 1).ToList() ?? new List<Transaction>()
            }
        };

        public static List<Transaction> Transactions = new List<Transaction>
        {
            new Transaction
            {
                Id = 1,
                Description = "C/O 2 PG STMT-23499930K499",
                ReferenceNumber = "STMT-23499930K499",
                CurrencyCode = "GHS",
                TransactionDate = new DateTime(2022, 02, 03),
                TotalAmount = 200,
                Type = TransactionType.Credit,
                BankStatementId = 1,
                BankStatement = BankStatements?.FirstOrDefault(x => x.Id == 1) ?? new BankStatement(),
                CreatedBy = "Admin",
                CreatedDate = DateTime.UtcNow,
            }
        };

        
    }
}
