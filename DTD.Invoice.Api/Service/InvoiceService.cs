using Dapper;
using DTD.Core.Context;
using DTD.Core.Helper;
using DTD.Model;
using System.Diagnostics;

namespace DTD.Invoice.Api.Service
{
    public class InvoiceService : IInvoiceService
    {
        private readonly DapperContext _dapperContext;
        public InvoiceService(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public async Task<InvoiceDto?> GetInvoiceById(int invoiceId)
        {
            using var activity = InvoiceTracing.ActivitySource.StartActivity("GetInvoiceById Veri Hazırlama");
            activity?.AddEvent(new ActivityEvent("başladı"));
            using var connection = await _dapperContext.CreateConnectionAsync();
            var sql = "SELECT * FROM Invoices WHERE Id = @invoiceId";
            var invoice = await connection.QueryFirstOrDefaultAsync<InvoiceDto>(sql, new { invoiceId });
            activity?.AddEvent(new ActivityEvent("bitti"));
            return invoice;
        }
    }
}
