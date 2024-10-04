using MVC.Areas.AdminPanel.Models.DTOs.OfferCartMessage;
using MVC.Areas.AdminPanel.Models.VMs.OfferCart;
using MVC.Areas.AdminPanel.Models.VMs.Report;
using MVC.Areas.AdminPanel.Models.VMs.User;

namespace MVC.Areas.CustomerServicePanel.Models.DTOs.OfferCart
{
    public class UpdateOfferCartDTO
    {
        public int OfferCartId { get; set; }
        public int AppUserId { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public string Address { get; set; }
        public decimal TotalPrice { get; set; }
        public bool IsRefundRequest { get; set; } = false; // iade talebi (visitor)
        public bool RefundCustomerService { get; set; } = false; // iade işlemi onayı (Customer Service)
        public bool AcceptRefundRequest { get; set; } = false; // iade işlemi onayı (Admin)
        public DateTime? CompletedDate { get; set; }

        //onaylanma durumları
        public bool AcceptAdmin { get; set; } = false;
        public bool AcceptContentManager { get; set; } = false;
        public bool AcceptCustomerService { get; set; } = false;
        public bool AcceptVisitor { get; set; } = false;

        //üretim aşamaları 
        public bool IsApproved { get; set; } = false;
        public bool IsSample { get; set; } = false;
        public bool IsMold { get; set; } = false;
        public bool IsFinalization { get; set; } = false;

        public DateTime AddedDate { get; set; } = DateTime.Now;
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public bool IsActive { get; set; }

        //navigation property
        public AppUser? AppUser { get; set; }
        public AdminPanel.Models.VMs.Category.Product? Product { get; set; }
        public ICollection<OfferCartMessage> OfferCartMessages { get; set; }
    }
}
