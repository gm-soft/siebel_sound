15.03.2018
Cервере kaspihelp-wfc-1, kaspihelp-wfc-2, kaspihelp-wfc балансировщик
80 порт был занят, настроил на 777

Ссылки в отчетах должны соответствовать следующему шаблону

http://kaspihelp-wfc:777/?k=U291bmQxLzIwMTgwMjAzL0Q2NjVFMEFFLTdFRkItRTcxMS1CMTIyLTAwNTA1NjgxMUIyQi8yMDE4MDIwMzEzMzkzMF81ODBFMEY4My0wOEI1LTExRTgtQjEyMi0wMDUwNTY4MTFCMkIuc2xhdmlj

http протокол используется только без шифрования, https не поддерживается
kaspihelp-wfc:777 точка входа, iis сервер
k параметр для получения относительного(закодированного) пути к файлу
U291bmQxLzIwMTgwMjAzL0Q2NjVFMEFFLTdFRkItRTcxMS1CMTIyLTAwNTA1NjgxMUIyQi8yMDE4MDIwMzEzMzkzMF81ODBFMEY4My0wOEI1LTExRTgtQjEyMi0wMDUwNTY4MTFCMkIuc2xhdmlj  ссылка закодированная в base64

Путь к серверу первоисточнику, хранилищу аудио файлов, настаивается в конфигурации приложения, web.config - <add key="fileServerUrl" value="https://rec-adb/"/>