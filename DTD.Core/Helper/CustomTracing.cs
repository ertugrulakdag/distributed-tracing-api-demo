using System.Diagnostics;

namespace DTD.Core.Helper
{
    public static class AuthenticationTracing
    {
        public static readonly ActivitySource ActivitySource = new("AuthenticationTracing");
    }
    public static class InvoiceTracing
    {
        public static readonly ActivitySource ActivitySource = new("InvoiceTracing");
    }
    public static class ShopTracing
    {
        public static readonly ActivitySource ActivitySource = new("ShopTracing");
    }
}
