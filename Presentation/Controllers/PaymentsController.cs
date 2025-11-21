using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class PaymentsController(IServiceManager _serviceManager) : BaseController
    {
        // Create Or Update Payment Intent Id
        [Authorize]
        [HttpPost(template: "{BasketId}")] // POST BaseURL/Api/Payments/gobdbdjbd

        public async Task<ActionResult<BasketDTo>> CreateOrUpdatePaymentIntent(string BasketId)
        {
            var Basket = await _serviceManager.PaymentService.CreateOrUpdatePaymentIntent(BasketId);
            return
            Ok(value: Basket);
        }
    }
    
}
