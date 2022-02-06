# PhoneBookApp
.Net Core ile Basit Telefon Defteri Uygulaması


PhoneBookApp\bin\Debug\netcoreapp3.1\PhoneBookApp.exe
PhoneBookApp\Microservices\ReportService\PhoneBook.ReportService\bin\Debug\netcoreapp3.1\PhoneBook.ReportService.exe
çalıştırılarak ayağa kaldırılabilir. İki servis ayrı portlarda çalışmaktadır.

Docker ise 'docker-compose up -d' ile ayağa kalkacaktır.

Proje içerisinde PostgreSql kullanılmıştır.
Mesaj Queue işlemi için Cap üzerinden RabbitMq kullanılmıştır.
İki mikroservisinde DB leri ortaktır. Vakit darlığı sebebiyle ortak olarak kullanılmıştır. Eğer bir firma çalışması vs olsaydı muhtemelen ayrı yapıp mongodb ya da herhangi bir nosql veri tabanı kullanabilirdim.



api/Users/GetAll    ----> Rehberdeki kişilerin listelenmesi

api/Users/GetAllWithDetail   ----> Rehberdeki kişilerin detaylı bilgisi 

api/Users/GetById     ----> UUIDsi verilen Bir kişiye ait iletişim bilgilerinin de yer aldığı detaylı bilgi

api/Users/Add      ---->   Rehberde kişi oluşturma (Belirtilen veri yapısına uygun)

api/Users/Update   ---->    Rehberdeki kişiye ait bilgileri güncelleme (Belirtilen veri yapısına uygun)

api/Users/Delete   ----> UUIDsi verilen kişiyi Rehberden kaldırma 


api/Contact/Add     ---> rehberdeki kişiye iletişim bilgisi ekleme

api/Contact/Update  ---> rehberdeki kişinin iletişim bilgisini güncelleme

api/Contact/Delete  ---> rehberdeki kişinin iletişim bilgilerini kaldırma


api/Reports/CreateReport    ---> Rapor yaratma talebi. Verilecek personId ile kişinin bulunduğu konum için rapor oluşturulacak.

api/Reports/GetAll          ---> Raporların listesi 

api/Reports/GetById         ---> Sistemin oluşturduğu bir raporun id si verilerek detay bilgileri istenir.
