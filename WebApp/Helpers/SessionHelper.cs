using System;
using System.Web;

namespace WebApp.Helpers
{
    public static class SessionHelper
    {
        public static void SaveObjectToSession<TType>(string key, TType objectToSave)
        {
            HttpContext.Current.Session.Add(key, objectToSave);
        }

        public static TType GetObjectFromSession<TType>(string key)
        {
            var session = HttpContext.Current.Session;
            var uriFromSession = (TType)session[key];
            session.Remove(key);

            return uriFromSession;
        }
    }
}