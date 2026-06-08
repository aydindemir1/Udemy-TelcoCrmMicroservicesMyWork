using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Auth.Constants
{
    public static class AuthMessages
    {
        public const string UserEmailAlreadyExists = "Böyle bir e-posta mevcut";
        public const string UserDontExists = "Böyle bir kullanıcı mevcut değil";
        public const string UserShouldNotBeLocked = "Hesabınız kilitlendi. Lütfen daha sonra tekrar deneyiniz.";
        public const string AccountLocked = "Hesabınız çok fazla başarısız giriş denemesinden dolayı kilitlendi. Lütfen daha sonra tekrar deneyiniz.";
        public const string PasswordDontMatch = "E-postanız veya şifreniz yanlış";
        public const string RefreshDontExists = "Böyle bir refresh token mevcut değil";
        public const string InvalidRefreshToken = "Geçersiz refresh token";
        public const string UserHaveAlreadyAAuthenticator = "Kullanıcının zaten bir doğrulayıcısı var";
        public const string EmailAuthenticatorDontExists = "Böyle bir e-posta doğrulayıcısı mevcut değil";
        public const string EmailActivationKeyDontExists = "Böyle bir e-posta doğrulama anahtarı mevcut değil";
    }
}
