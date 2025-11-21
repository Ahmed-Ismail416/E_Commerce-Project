using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.OrderModule;
using Microsoft.Extensions.Configuration;
using Shared.DTOs.BasketModuleDtos;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product = DomainLayer.Models.Products.Product;
namespace ServiceAbstraction
{
    public class PaymentService(IConfiguration _configuration,
        IBasketService _basketService,
        IUnitOfWork _unitOfWork,
        IMapper _mapper) : IPaymentService
    {
        public async Task<BasketDto> CreateOrUpdatePaymentIntent(string basketId)
        {
            // Stripe Configuration
            StripeConfiguration.ApiKey = _configuration["StripeSettings:SecretKey"];

            //Get Basket By Id
            var Basket = await _basketService.GetBasketAsync(basketId) ?? throw new BasketNotFoundException(basketId);

            //Get Amount - Product + Delviery Method
            var productRepo = _unitOfWork.GetRepository<Product, int>();
            foreach (var item in Basket.Items)
            {
                var Product = await productRepo.GetByIdAsync(item.Id);
                item.Price = Product.Price;
            }
            ArgumentNullException.ThrowIfNull(Basket.deliveryMethodId);
            var deliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(Basket.deliveryMethodId.Value)
                ?? throw new DeliveryMethodNotFoundException(Basket.deliveryMethodId);
            Basket.shippingPrice = deliveryMethod.Price;

            var BasketAmount = (long)(Basket.Items.Sum(O => O.Quantity * O.Price) + Basket.shippingPrice) * 100;

            //create Payment Intent
            var PaymentService = new PaymentIntentService();
            if (Basket.paymentIntentId == null)
            {
                // Create
                var Options = new PaymentIntentCreateOptions() { Amount = BasketAmount, Currency = "usd", PaymentMethodTypes = new List<string> { "card" } };
                var PaymentIntent = await PaymentService.CreateAsync(Options);
                Basket.paymentIntentId = PaymentIntent.Id;
                await _unitOfWork.SaveChangesAsync();
            } else
            {
                //update
                var Options = new PaymentIntentUpdateOptions() { Amount = BasketAmount };
                await PaymentService.UpdateAsync(Basket.paymentIntentId, Options);

            }
            await _basketService.CreateOrUpdateBasketAsync(Basket);

            return Basket;
        }
    }
}
