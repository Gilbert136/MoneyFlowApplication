namespace MoneyFlowApplication.Json
{
    /// <summary>
    /// The json model that will be returned by all API action methods
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    public class ResponseJson<TData>
    {
        /// <summary>Determines if the request was successful</summary>
        public bool? State { get; set; }

        /// <summary>The message to give feedback to the customer</summary>
        public string? Message { get; set; } = "Success";

        /// <summary>The data to return as the response</summary>
        public TData? Data { get; set; }

        /// <summary>The error information when the request a problem</summary>
        public object? Error { get; set; }
    }
}