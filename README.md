# .NET Core Proje - README

## Ön Koşullar
- .NET 8.0 SDK
- Visual Studio 2022 veya üstü (VS Code için C# eklentisi)
- (Opsiyonel) Docker ve Docker Compose

---

## 1. Visual Studio ile Çalıştırma

1. Projeyi Visual Studio’da açın.
2. `YazOkulu.GEN.API` projesini sağ tıklayın ve **Set as Startup Project** seçin.
3. `F5` veya **Start Debugging** ile çalıştırın.

---

## 2. VS Code ile Çalıştırma

1. VS Code ile proje dizinini açın.
2. Terminal açın ve `YazOkulu.GEN.API` projesinin path’ine gidin:
3. ...\YazOkuluBackend\YazOkulu.GEN.API>
4. dotnet run

## 3. Docker/Docker Compose ile Çalıştırma (Docker Desktop Çalışır Durumda Olmalı)
1. Terminalde proje ana dizinini açın.
2. ...\YazOkuluBackend>
3. docker-compose up --build
4. İlgili image oluşturulup container ile başlatılacaktır.

## İşlemlerden öncesinde Sql Server veritabanı oluşturulmuş olması gerekir. Bunun için "YazOkuluDB" repository de ki ilgili README dosyasını okuyunuz.
