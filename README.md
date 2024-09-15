# OrchardTodoTelegramApp

OrchardTodoTelegramApp, bir Telegram botu arac�l���yla To-Do listelerini y�netmek i�in geli�tirilmi� bir uygulamad�r. Bu proje, **Orchard Core CMS**, **.NET Core**, **Vue.js**, **Azure Table Storage** ve **Telegram Bot API**'yi kullanarak geli�tirilmi�tir.

## ��indekiler

- [�zellikler](#�zellikler)
- [Kurulum](#kurulum)
- [Kullan�m](#kullan�m)
- [Teknolojiler](#teknolojiler)
- [Lisans](#lisans)

## �zellikler

- Telegram �zerinden /todo komutu ile To-Do listesini g�r�nt�leme
- Yeni To-Do olu�turma, d�zenleme ve silme
- Orchard Core mod�lleriyle entegre yap�
- Azure Table Storage kullanarak verileri saklama
- Vue.js ile dinamik ve kullan�c� dostu aray�z
- IIS Express, HTTP ve HTTPS deste�i

## Kurulum

### Gerekli Ba��ml�l�klar

Projeyi �al��t�rmadan �nce a�a��daki ba��ml�l�klar�n kurulu oldu�undan emin olun:

- **.NET 6 SDK** veya daha yenisi
- **Visual Studio 2022** veya benzer bir IDE
- **Azure Storage Hesab�** (Azure Table Storage kullanmak i�in)
- **Telegram Bot API Token** (Bot olu�turmak i�in [BotFather](https://core.telegram.org/bots#botfather) arac�l���yla)

### Ad�mlar

1. Projeyi indirin veya klonlay�n:
    ```bash
    git clone https://github.com/irmkar/OrchardTodoTelegramApp.git
    cd OrchardTodoTelegramApp
    ```

2. **appsettings.json** dosyas�na bot token'�n�z� ve Azure Table Storage ba�lant� bilgilerini ekleyin:
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

3. Projeyi �al��t�rmak i�in Visual Studio veya a�a��daki komutu kullan�n:
    ```bash
    dotnet run
    ```

4. Proje �al��t���nda, Telegram botunuz arac�l���yla `/start` ve `/todo` komutlar�n� kullanarak To-Do listenizi y�netebilirsiniz.

## Kullan�m

Admin paneline giri� yapabilmek i�in a�a��daki bilgileri kullanabilirsiniz:

- Kullan�c� Ad�: <span style="color: red;">admin</span>
- �ifre: <span style="color: red;">Admin@123</span>


### Telegram Botu Kullan�m�

1. Botunuzu [BotFather](https://core.telegram.org/bots#botfather) �zerinden olu�turun ve API token'�n�z� al�n.
2. Projeyi ba�latt�ktan sonra Telegram �zerinden `/start` komutunu kullanarak botunuzu ba�lat�n.
3. `/todo` komutu ile To-Do listenizi g�r�nt�leyebilir, listenizdeki ��eleri y�netebilirsiniz.

### Web Aray�z�

Projeyi ilk �al��t�rd���n�zda, varsay�lan olarak Orchard Core Framework'�n kar��lama sayfas� a��l�r. Bu sayfa, site ba�ar�l� bir �ekilde olu�turuldu�unda g�r�nt�lenen bir "Ho� geldiniz" mesaj�n� i�erir.

#### To-Do Mod�l�ne Eri�im
Projenin temel i�levlerinden biri, To-Do List mod�l�d�r. Bu mod�l, admin paneli �zerinden y�netilebilir ve kullan�c�ya basit bir yap�lacaklar listesi sunar. Admin panelindeki sol navigasyon men�s�nde "Orchard To-Do App Management" ba�lant�s�na t�klayarak mod�le eri�ebilirsiniz.

- URL: /Module/Todo

**Ana Sayfa** �zerinden To-Do listenizi g�r�nt�leyebilir, yeni g�revler ekleyebilir, mevcut g�revleri d�zenleyebilir veya silebilirsiniz.

## Teknolojiler

Bu projede kullan�lan ana teknolojiler:

- **Orchard Core**: Mod�ler bir CMS platformu.
- **.NET Core**: Proje altyap�s� ve API'lar i�in.
- **Vue.js**: To-Do listesi i�in dinamik kullan�c� aray�z�.
- **Azure Table Storage**: Verileri saklamak i�in.
- **Telegram Bot API**: Telegram botu entegrasyonu.

## Lisans

Bu proje [MIT Lisans�](LICENSE) ile lisanslanm��t�r.
