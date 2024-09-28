namespace MVC.Areas.AdminPanel.Models.VMs.Report
{
    public class GetPaymentReportVM
    {
        public int OfferCartId { get; set; }
        public decimal TotalPrice { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime? CompletedDate { get; set; }
    }
}
