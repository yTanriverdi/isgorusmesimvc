namespace MVC.Areas.AdminPanel.Models.DTOs.OfferCartMessage
{
    public class OfferCartMessageDTO
    {
        public int OfferCartMessageId { get; set; }
        public int AppUserId { get; set; }
        public int OfferCartId { get; set; }
        public string? Message { get; set; }
        public DateTime? AddedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public bool IsActive { get; set; }
    }
}
