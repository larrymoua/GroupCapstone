using GroupCapstone.Models;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroupCapstone.Controllers
{
    public class CartController : Controller
    {
  
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cart/Create
        [HttpPost]
        public ActionResult Create(string stripeToken)
        {
            StripeConfiguration.SetApiKey("sk_test_xUz5aOBDwQSi8S61VVen5E37");
     
            var options = new ChargeCreateOptions
            {
                Amount = 999,
                Currency = "usd",
                Description = "Example charge for larrymoua24@yahoo.com",
                SourceId = stripeToken
            };
            var service = new ChargeService();
            Charge charge = service.Create(options);

            var model = new ChargeViewModel();
            model.ChargeId = charge.Id;

            return View("OrderStatus", model);
        }

    }
}
