namespace DTD.Model
{
    public class ProductCategory
    {
        public int ProductId { get; set; }
        public ProductDto Product { get; set; } = default!;

        public int CategoryId { get; set; }
        public CategoryDto Category { get; set; } = default!;
    }
}
