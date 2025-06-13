💰 Iyzico Ödeme Sistemi Entegrasyonu (Test Uygulaması)
Bu proje, Iyzico Sanal Pos API’si ile ödeme entegrasyonunun nasıl gerçekleştirileceğini temel düzeyde öğrenmek ve test etmek amacıyla geliştirilmiştir. Özellikle e-ticaret projelerinde yaygın olarak tercih edilen Iyzico servisinin, dış servis olarak nasıl yapılandırıldığını anlamaya odaklanılmıştır.

🎯 Proje Amacı
Bu uygulama; kullanıcıdan alınan ödeme bilgilerini Iyzico test ortamına ileterek, başarılı/başarısız sonuçlara göre geri dönüşleri ele alır. Amaç, gerçek hayatta kullanılan bir sanal pos servisinin:

API yapısını anlamak

Gerekli istek parametrelerini hazırlamak

Güvenlik süreçlerine (hash, signature) dikkat etmek

Temel düzeyde ödeme akışını oluşturmak
üzerine odaklanmaktır.

⚙️ Kullanılan Teknolojiler
Teknoloji	Açıklama
.NET Core 8 Web API	
HttpClient 	Dış servise ödeme isteği göndermek için
Iyzico Sandbox	Test ortamı ile ödeme akışı simülasyonu


🧪 Test Edilen Senaryolar
Başarılı ödeme (test kartı ile)
Geçersiz kart ile başarısız işlem
Eksik parametre gönderiminde hata yönetimi
API üzerinden dönen response mesajlarının yorumlanması


