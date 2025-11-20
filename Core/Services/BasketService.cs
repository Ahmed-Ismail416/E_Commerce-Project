using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.BasketModule;
using ServiceAbstraction;
using Shared.DTOs.BasketModuleDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BasketService(IBasketRepository _basketrepository, IMapper _mapper) : IBasketService
    {
        public async Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basket)
        {
            var CustomerBasket = _mapper.Map<BasketDto,CustomerBasket>(basket);
            var CreatedOrUpdatedBasket = await _basketrepository.UpdateOrCreateBasketAsync(CustomerBasket);
            if(CreatedOrUpdatedBasket is not null)
                return await GetBasketAsync(basket.Id);
            else
                throw new Exception("Cant Create Or Update Basket, Please Try Again");


        }

        public async Task<bool> DeleteBasketAsync(string key)
        => await _basketrepository.DeleteBasketAsync(key);

        public async Task<BasketDto> GetBasketAsync(string key)
        {
            var basket =  await _basketrepository.GetBasketAsync(key);
            if(basket is not null)
                return _mapper.Map<CustomerBasket,BasketDto>(basket);
            else
                throw new BasketNotFoundException(key);
        }
    }
}
