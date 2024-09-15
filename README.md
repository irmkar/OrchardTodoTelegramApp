# OrchardTodoTelegramApp

OrchardTodoTelegramApp, bir Telegram botu aracýlýðýyla To-Do listelerini yönetmek için geliþtirilmiþ bir uygulamadýr. Bu proje, **Orchard Core CMS**, **.NET Core**, **Vue.js**, **Azure Table Storage** ve **Telegram Bot API**'yi kullanarak geliþtirilmiþtir.

## Ýçindekiler

- [Özellikler](#özellikler)
- [Kurulum](#kurulum)
- [Kullaným](#kullaným)
- [Teknolojiler](#teknolojiler)
- [Lisans](#lisans)

## Özellikler

- Telegram üzerinden /todo komutu ile To-Do listesini görüntüleme
- Yeni To-Do oluþturma, düzenleme ve silme
- Orchard Core modülleriyle entegre yapý
- Azure Table Storage kullanarak verileri saklama
- Vue.js ile dinamik ve kullanýcý dostu arayüz
- IIS Express, HTTP ve HTTPS desteði

## Kurulum

### Gerekli Baðýmlýlýklar

Projeyi çalýþtýrmadan önce aþaðýdaki baðýmlýlýklarýn kurulu olduðundan emin olun:

- **.NET 6 SDK** veya daha yenisi
- **Visual Studio 2022** veya benzer bir IDE
- **Azure Storage Hesabý** (Azure Table Storage kullanmak için)
- **Telegram Bot API Token** (Bot oluþturmak için [BotFather](https://core.telegram.org/bots#botfather) aracýlýðýyla)

### Adýmlar

1. Projeyi indirin veya klonlayýn:
    ```bash
    git clone https://github.com/irmkar/OrchardTodoTelegramApp.git
    cd OrchardTodoTelegramApp
    ```

2. **appsettings.json** dosyasýna bot token'ýnýzý ve Azure Table Storage baðlantý bilgilerini ekleyin:
    ```json
    {
      "AzureStorage": {
        "TableStorage": {
          "ConnectionString": "<Your Azure Storage Connection String>",
          "TableName": "TodoTable"
        }
      },
      "TelegramBotToken": "<Your Telegram Bot Token>"
    }
    ```

3. Projeyi çalýþtýrmak için Visual Studio veya aþaðýdaki komutu kullanýn:
    ```bash
    dotnet run
    ```

4. Proje çalýþtýðýnda, Telegram botunuz aracýlýðýyla `/start` ve `/todo` komutlarýný kullanarak To-Do listenizi yönetebilirsiniz.

## Kullaným

Admin paneline giriþ yapabilmek için aþaðýdaki bilgileri kullanabilirsiniz:

- Kullanýcý Adý: <span style="color: red;">admin</span>
- Þifre: <span style="color: red;">Admin@123</span>


### Telegram Botu Kullanýmý

1. Botunuzu [BotFather](https://core.telegram.org/bots#botfather) üzerinden oluþturun ve API token'ýnýzý alýn.
2. Projeyi baþlattýktan sonra Telegram üzerinden `/start` komutunu kullanarak botunuzu baþlatýn.
3. `/todo` komutu ile To-Do listenizi görüntüleyebilir, listenizdeki öðeleri yönetebilirsiniz.

### Web Arayüzü

Projeyi ilk çalýþtýrdýðýnýzda, varsayýlan olarak Orchard Core Framework'ün karþýlama sayfasý açýlýr. Bu sayfa, site baþarýlý bir þekilde oluþturulduðunda görüntülenen bir "Hoþ geldiniz" mesajýný içerir.

#### To-Do Modülüne Eriþim
Projenin temel iþlevlerinden biri, To-Do List modülüdür. Bu modül, admin paneli üzerinden yönetilebilir ve kullanýcýya basit bir yapýlacaklar listesi sunar. Admin panelindeki sol navigasyon menüsünde "Orchard To-Do App Management" baðlantýsýna týklayarak modüle eriþebilirsiniz.

- URL: /Module/Todo

**Ana Sayfa** üzerinden To-Do listenizi görüntüleyebilir, yeni görevler ekleyebilir, mevcut görevleri düzenleyebilir veya silebilirsiniz.

## Teknolojiler

Bu projede kullanýlan ana teknolojiler:

- **Orchard Core**: Modüler bir CMS platformu.
- **.NET Core**: Proje altyapýsý ve API'lar için.
- **Vue.js**: To-Do listesi için dinamik kullanýcý arayüzü.
- **Azure Table Storage**: Verileri saklamak için.
- **Telegram Bot API**: Telegram botu entegrasyonu.

## Lisans

Bu proje [MIT Lisansý](LICENSE) ile lisanslanmýþtýr.
