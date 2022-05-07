using System.Net.Http.Headers;
using System.Text;

namespace MoneyFlowApplication.Data
{
    public static class ApiData
    {
        public static async Task<string> ApiStatementJsonData()
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("https://api.sandbox.treasuryprime.com");

                var byteArray = new UTF8Encoding().GetBytes("key_1g2b17keqba_sandbox_001:i3w2AnatqKL13PdyH2oIvJqZKjHK7XO6");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                var response = await client.GetAsync("transaction");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
