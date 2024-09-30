﻿namespace MVC.Areas.AdminPanel.Models.VMs.Product
{
    public class GetProductDetailsVM
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string Picture { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
    }
}