using Discount.API.Entities;
using Discount.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Discount.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountRepository _discountRepository;

        public DiscountController(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository ?? throw new ArgumentNullException(nameof(discountRepository));
        }

        [HttpGet("{productName}", Name = nameof(GetDiscount))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Coupon))]
        public async Task<ActionResult<Coupon>> GetDiscount(string productName)
        {
            var coupon = await _discountRepository.GetDiscount(productName);

            return Ok(coupon);
        }

        [HttpPost(Name = nameof(CreateDiscount))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Coupon))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Coupon))]
        public async Task<ActionResult<Coupon>> CreateDiscount([FromBody] Coupon coupon)
        {
            var creationResult = await _discountRepository.CreateDiscount(coupon);

            if (creationResult) return CreatedAtRoute(nameof(GetDiscount), new { productName = coupon.ProductName }, coupon);

            return BadRequest(coupon);
        }

        [HttpPut(Name = nameof(UpdateDiscount))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Coupon))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Coupon))]
        public async Task<ActionResult<Coupon>> UpdateDiscount([FromBody] Coupon coupon)
        {
            var updateResult = await _discountRepository.UpdateDiscount(coupon);

            if (updateResult) return Ok(updateResult);

            return BadRequest(coupon);
        }

        [HttpDelete("{productName}", Name = nameof(DeleteDiscount))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Coupon))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Coupon))]
        public async Task<ActionResult<Coupon>> DeleteDiscount(string productName)
        {
            var deleteResult = await _discountRepository.DeleteDiscount(productName);

            if (deleteResult) return Ok(productName);

            return BadRequest(productName);
        }
    }
}
