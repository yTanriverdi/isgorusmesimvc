using MVC.Areas.AdminPanel.Models.VMs.Report;
using MVC.Areas.AdminPanel.Models.VMs.User;

namespace MVC.Areas.AdminPanel.Models.VMs.OfferCart
{
    public class GetAllOfferCartsVM
    {
        public int OfferCartId { get; set; }
        public int AppUserId { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public string Address { get; set; }
        public decimal TotalPrice { get; set; }
        public bool IsRefundRequest { get; set; } = false; // iade talebi
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

        public DateTime AddedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public bool IsActive { get; set; }

        public Product Product { get; set; }
        public AppUser AppUser { get; set; }
        public OfferCartMessage OfferCartMessage { get; set; }
    }
}
