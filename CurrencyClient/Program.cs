using System;
using System.Configuration;
using System.Threading;

namespace CurrencyClient
{
    class Program
    {
        static void Main()
        {
            int refresh = int.Parse(ConfigurationManager.AppSettings.Get("RefreshRate")) * 1000;
            CurrencyConsumer api = new CurrencyConsumer(
                ConfigurationManager.AppSettings.Get("Url"));
            Console.WriteLine("Exchange rate USD/GTQ from http://www.banguat.gob.gt/");
            while (true)
            {
                // Obtain the TipoCambio
                CurrencyEntry newCurrency = TipoCambio.TipoCambioDia();
                if(newCurrency != null)
                {
                    // Post to the API
                    api.PostCurrencyAsync(newCurrency);
                }
                else
                {
                    Console.WriteLine(String.Format(
                        "TipoCambio Web Service is not available retrying in {0} seconds",
                        refresh / 1000));
                }
                Thread.Sleep(refresh);
            }
        }
    }
}
