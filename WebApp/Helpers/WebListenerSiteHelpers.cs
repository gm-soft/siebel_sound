using System;
using System.Configuration;
using System.Net;
using System.Text;

namespace WebApp.Helpers
{
    public static class WebListenerSiteHelpers
    {
        private static string _fileServerUrl;

        /// <summary>
        /// Ссылка на файловый сервер
        /// </summary>
        /// <returns></returns>
        public static string GetFileServerUrl()
        {
            if (_fileServerUrl == null)
            {
                _fileServerUrl = ConfigurationManager.AppSettings["fileServerUrl"];
            }

            return _fileServerUrl;
        }

        public static string DecodeBase64(string base64EncodedFilePath)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(base64EncodedFilePath));
        }

        public static string GetFileFullWebUrl(string base64EncodedFilePath)
        {
            return $"{GetFileServerUrl()}{DecodeBase64(base64EncodedFilePath)}";
        }

        /// <summary>
        /// https://rec-adb/ - протокол https, без сертификата, доверяем по умолчанию.
        /// </summary>
        /// <param name="request"></param>
        public static void SetSertificateCallback(HttpWebRequest request)
        {
            request.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
        }
    }
}