namespace MVC.Areas.AdminPanel.Models.VMs.Report
{
    public class GetRejectedOffersReportVM
    {
        public int OfferCartId { get; set; }
        public string ProductName { get; set; }
        public DateTime AddedDate { get; set; }
        public string RejectionReason { get; set; }
    }
}
