using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using MoneyFlowApplication.Data;
using MoneyFlowApplication.Entity;
using MoneyFlowApplication.Enums;
using MoneyFlowApplication.Json;

namespace MoneyFlowApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BankStatementController : ControllerBase
    {
        // GET api/<BankStatementController>/query
        [HttpGet("query")]
        public async Task<ResponseJson<IEnumerable<Transaction>>> GetStatementByQueryExternalApi()
        {
            try
            {
                var jsonResponse = await ApiData.ApiStatementJsonData();
                var deserializeResponse = (JsonSerializer.Deserialize<TransactionsDeserialized>(jsonResponse) ?? new TransactionsDeserialized()).data ?? new List<TransactionDeserialize>();

                var transactionsResult = deserializeResponse.Select(x => new Transaction
                {
                    Description = x.desc,
                    TotalAmount = Convert.ToDecimal(x.amount),
                    TransactionDate = x.date,
                    Type = x.type == "deposit" ? TransactionType.Credit : TransactionType.Debit
                });

                var income = transactionsResult?.Where(x => x.Type == TransactionType.Credit).Sum(x => x.TotalAmount) ?? 0;
                var spending = transactionsResult?.Where(x => x.Type == TransactionType.Debit).Sum(x => x.TotalAmount) ?? 0;

                return new ResponseJson<IEnumerable<Transaction>>
                {
                    State = true,
                    Message = $"Income = {income} | Spending = {spending}",
                    Data = transactionsResult
                };
            }
            catch (Exception ex)
            {
                return new ResponseJson<IEnumerable<Transaction>>
                {
                    State = false,
                    Message = ex.Message,
                    Error = ex
                };
            }
        }

        // GET api/<BankStatementController>/query
        [HttpPost("query")]
        public ResponseJson<string> GetStatementByQuery(QueryJson query)
        {
            try
            {
                if (query.Id == null) throw new Exception("Id must exist for customer!!");

                var staticBankStatementDataContext = StaticData.BankStatements;
                var fromDate = query.FromDate ?? default;
                var toDate = query.ToDate ?? DateTime.Now;
                var customerId = query.Id;

                var bankStatementResult = staticBankStatementDataContext.FirstOrDefault(x => x.CustomerId == customerId && x.LogicalDelete == false);

                if (bankStatementResult == null) throw new Exception("Data does not exist!!");

                var bankStatementTransactionQueryResult = bankStatementResult?.Transactions?.Where(x =>
                    (DateTime.Compare(fromDate, x.TransactionDate) <= 0) &&
                    (DateTime.Compare(toDate, x.TransactionDate) >= 0)).ToList() ?? new List<Transaction>();

                var income = bankStatementTransactionQueryResult?.Where(x => x.Type == TransactionType.Credit).Sum(x => x.TotalAmount) ?? 0;
                var spending = bankStatementTransactionQueryResult?.Where(x => x.Type == TransactionType.Debit).Sum(x => x.TotalAmount) ?? 0;

                var incomeSpending = $"Income = {income} | Spending = {spending}";

                return new ResponseJson<string>
                {
                    State = true,
                    Message = "Success",
                    Data = incomeSpending
                };
            }
            catch (Exception ex)
            {
                return new ResponseJson<string>
                {
                    State = false,
                    Message = ex.Message,
                    Error = ex
                };
            }
        }

        // GET api/<BankStatementController>/customerid/1
        [HttpGet("customerid/{id}")]
        public ResponseJson<BankStatement> GetStatementByCustomerId(int id)
        {
            try
            {
                var staticBankStatementDataContext = StaticData.BankStatements;

                var bankStatementResult = staticBankStatementDataContext.FirstOrDefault(x => x.CustomerId == id && x.LogicalDelete == false) ?? new BankStatement();

                return new ResponseJson<BankStatement>
                {
                    State = true,
                    Message = "Success",
                    Data = bankStatementResult
                };
            }
            catch (Exception ex)
            {
                return new ResponseJson<BankStatement>
                {
                    State = false,
                    Message = ex.Message,
                    Error = ex
                };
            }
        }

        // GET api/<BankStatementController>/5
        [HttpGet("{id}")]
        public ResponseJson<BankStatement> Get(int id)
        {
            try
            {
                var staticBankStatementDataContext = StaticData.BankStatements;

                var bankStatementResult = staticBankStatementDataContext.FirstOrDefault(x => x.Id == id && x.LogicalDelete == false) ?? new BankStatement();

                return new ResponseJson<BankStatement>
                {
                    State = true,
                    Message = "Success",
                    Data = bankStatementResult
                };
            }
            catch (Exception ex)
            {
                return new ResponseJson<BankStatement>
                {
                    State = false,
                    Message = ex.Message,
                    Error = ex
                };
            }
        }

        // GET: api/<BankStatementController>
        [HttpGet]
        public ResponseJson<IEnumerable<BankStatement>> Get()
        {
            try
            {
                var staticBankStatementDataContext = StaticData.BankStatements;

                return new ResponseJson<IEnumerable<BankStatement>>
                {
                    State = true,
                    Message = "Success",
                    Data = staticBankStatementDataContext
                };
            }
            catch (Exception ex)
            {
                return new ResponseJson<IEnumerable<BankStatement>>
                {
                    State = false,
                    Message = ex.Message,
                    Error = ex
                };
            }
        }

        // POST api/<BankStatementController>
        [HttpPost]
        public ResponseJson<IEnumerable<BankStatement>> Post([FromBody] List<BankStatement> bankStatements)
        {
            try
            {
                var staticBankStatementDataContext = StaticData.BankStatements;

                staticBankStatementDataContext.AddRange(bankStatements);

                return new ResponseJson<IEnumerable<BankStatement>>
                {
                    State = true,
                    Message = "Success",
                    Data = bankStatements
                };
            }
            catch (Exception ex)
            {
                return new ResponseJson<IEnumerable<BankStatement>>
                {
                    State = false,
                    Message = ex.Message,
                    Error = ex
                };
            }
        }

        // PUT api/<BankStatementController>/5
        [HttpPut("{id}")]
        public ResponseJson<bool> Put(int id, [FromBody] BankStatement bankStatement)
        {
            try
            {
                var staticBankStatementDataContext = StaticData.BankStatements;

                var bankStatementResult = staticBankStatementDataContext.Find(x => x.Id == id && x.LogicalDelete == false);

                if (bankStatementResult == null) throw new Exception("Data does not exist!!");

                bankStatementResult.CustomerId = bankStatement.CustomerId;

                return new ResponseJson<bool>
                {
                    State = true,
                    Message = "Success",
                    Data = false
                };
            }
            catch (Exception ex)
            {
                return new ResponseJson<bool>
                {
                    State = false,
                    Message = ex.Message,
                    Error = ex
                };
            }
        }

        // DELETE api/<BankStatementController>/5
        [HttpDelete("{id}")]
        public ResponseJson<bool> Delete(int id)
        {
            try
            {
                var staticBankStatementDataContext = StaticData.BankStatements;

                var bankStatementResult = staticBankStatementDataContext.Find(x => x.Id == id && x.LogicalDelete == false);

                if (bankStatementResult == null) throw new Exception("Data does not exist!!");

                bankStatementResult.LogicalDelete = true;

                return new ResponseJson<bool>
                {
                    State = true,
                    Message = "Success",
                    Data = bankStatementResult.LogicalDelete
                };
            }
            catch (Exception ex)
            {
                return new ResponseJson<bool>
                {
                    State = false,
                    Message = ex.Message,
                    Error = ex
                };
            }
        }
    }
}
