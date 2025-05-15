using DTD.Model;
using System.Net.Http.Json;

namespace DTD.Core.Clients
{
    public class InvoiceClient
    {
        private readonly HttpClient _httpClient;

        public InvoiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<InvoiceDto?> GetInvoiceAsync(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<InvoiceDto>($"/Invoice/GetInvoiceById/{id}");
            return response;
        }
    }
}
