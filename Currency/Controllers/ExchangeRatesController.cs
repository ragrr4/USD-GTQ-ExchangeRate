using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace Currency.Controllers
{
    public class ExchangeRatesController : ApiController
    {
        private ExchangeEntities db = new ExchangeEntities();

        // GET: api/ExchangeRates
        public List<ExchangeRate> GetExchangeRates()
        {
            return db.ExchangeRate.ToList();
        }

        // GET: api/ExchangeRates/5
        [ResponseType(typeof(ExchangeRate))]
        public IHttpActionResult GetExchangeRates(int id)
        {
            ExchangeRate exchangeRate = db.ExchangeRate.FirstOrDefault(row => row.ID == id);
            if (exchangeRate == null)
            {
                return NotFound();
            }

            return Ok(exchangeRate);
        }

        // POST: api/ExchangeRates
        [HttpPost]
        public IHttpActionResult PostExchangeRates(DateTime date, double rate)
        {
            ExchangeRate exchangeRate = new ExchangeRate()
            {
                Date = date.Date,
                Rate = rate,
                WhenObtained = DateTime.Now
            };
            db.ExchangeRate.Add(exchangeRate);
            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = exchangeRate.ID }, exchangeRate.WhenObtained);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}