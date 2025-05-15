using DTD.Model;

namespace DTD.Invoice.Api.Service
{
    public interface IInvoiceService
    {
        Task<InvoiceDto?> GetInvoiceById(int invoiceId);
    }
}
