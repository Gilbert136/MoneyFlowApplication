namespace MoneyFlowApplication.Json
{
    public class QueryJson
    {
        public int? Id { get; set; }
        public string? Query { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
