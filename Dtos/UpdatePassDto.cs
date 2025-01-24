namespace movecareAPI.Dtos
{
    public class UpdatePassDto
    {
        public int UserId { get; set; } // Kullanıcının id'si
        public string CurrentPassword { get; set; } // Mevcut şifre
        public string NewPassword { get; set; } // Yeni şifre
        public string ConfirmNewPassword { get; set; } // Yeni şifre doğrulama
    }
}