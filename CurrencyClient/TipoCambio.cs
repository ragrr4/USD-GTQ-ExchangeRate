using System;
using System.IO;
using System.Net;
using System.Xml;

namespace CurrencyClient
{
    class TipoCambio
    {
        private const string wsUrl = "http://www.banguat.gob.gt//variables/ws/TipoCambio.asmx";
        private const string wsAction = "http://www.banguat.gob.gt/variables/ws/TipoCambioDia";

        /// <summary>method <c>TipoCambioDia</c>
        /// Consumes the TipoCambioDia action from the TipoCambio WebService.</summary>
        public static CurrencyEntry TipoCambioDia()
        {
            CurrencyEntry currencyEntry = null;

            const string soapBody= @"<TipoCambioDia xmlns=""http://www.banguat.gob.gt/variables/ws/"" />";
            XmlDocument soapEnvelopeXml = CreateSoapEnvelope(soapBody);
            HttpWebRequest webRequest = CreateWebRequest(wsUrl, wsAction);
            InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);

            // async call to web request.
            IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);
            asyncResult.AsyncWaitHandle.WaitOne();

            // Response from the completed web request.
            string soapResult;
            using (WebResponse webResponse = webRequest.EndGetResponse(asyncResult))
            {
                using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                {
                    soapResult = rd.ReadToEnd();
                }
                // Retrieve information from the XML response
                XmlDocument response = new XmlDocument();
                response.LoadXml(soapResult);
                currencyEntry = new CurrencyEntry();
                string fecha = response.GetElementsByTagName("fecha")[0].InnerText;
                currencyEntry.date = DateTime.ParseExact(fecha, "dd/MM/yyyy", null);
                currencyEntry.rate = Convert.ToDouble(response.GetElementsByTagName("referencia")[0].InnerText);
            }
            return currencyEntry;
        }

        private static HttpWebRequest CreateWebRequest(string url, string action)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Headers.Add("SOAPAction", action);
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }

        private static XmlDocument CreateSoapEnvelope(string soapBody)
        {
            XmlDocument soapEnvelopeDocument = new XmlDocument();
            soapEnvelopeDocument.LoadXml(string.Format(
                @"<?xml version=""1.0"" encoding=""utf-8""?>
                <soap:Envelope xmlns:xsi=""http://www.w3.org/1999/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/1999/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                    <soap:Body>
                        {0}
                    </soap:Body>
                </soap:Envelope>", soapBody));
            return soapEnvelopeDocument;
        }

        private static void InsertSoapEnvelopeIntoWebRequest(XmlDocument soapEnvelopeXml, HttpWebRequest webRequest)
        {
            using (Stream stream = webRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }
        }
    }
}
