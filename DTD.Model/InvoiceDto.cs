namespace DTD.Model
{
    public class InvoiceDto
    {
        public int Id { get; set; }

        public string InvoiceNumber { get; set; } = default!;
        public string CustomerName { get; set; } = default!;
        public DateTime InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int CreatedId { get; set; }
        public UserDto? CreatedByUser { get; set; }  
    }
}
