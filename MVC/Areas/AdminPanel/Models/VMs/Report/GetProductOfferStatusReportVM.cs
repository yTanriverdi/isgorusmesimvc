namespace MVC.Areas.AdminPanel.Models.VMs.Report
{
    public class GetProductOfferStatusReportVM
    {
        public int ProductId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string OfferStatus { get; set; }
    }
}
