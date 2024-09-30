namespace MVC.Areas.AdminPanel.Models.DTOs.Log
{
    public class LogDTO
    {
        public long Id { get; set; } // Bu sefer Id'yi int yerine Long yaptım ömrü uzun olsun diye
        public DateTime? Time { get; set; } = DateTime.Now;
        public string Level { get; set; } // Level olarak Log seviyesini belirt biz Information ve Error olarak 2 tane vereceğiz
        public string Message { get; set; } // Log mesajı
        public string Source { get; set; } // Kaynak hangi Controller ise o girilecek 
        public int? UserId { get; set; } // İstersek Hangi kullanıcı tarafından yapılan işlemde log tutulduğunu tutmamız için
        public string? ExceptionDetails { get; set; } // Hata Mesajını ekle ex..


        public DateTime AddedDate { get; set; } = DateTime.Now;
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
