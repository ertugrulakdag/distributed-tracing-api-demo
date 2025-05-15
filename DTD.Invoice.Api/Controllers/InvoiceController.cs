using DTD.Invoice.Api.Service;
using Microsoft.AspNetCore.Mvc;

namespace DTD.Invoice.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class InvoiceController : Controller
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetInvoiceById(int id)
        {
            var invoice = await _invoiceService.GetInvoiceById(id);
            return Ok(invoice);
        }
    }
}
