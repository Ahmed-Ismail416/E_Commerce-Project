using Shared.DTOs.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.OrderDtos
{
    public class OrderDto
    {
        public string BaskedId { get; set; } = default!;
        public int DelvierMethod { get; set; }
        public AddressDto Address { get; set; } = default!;
    }
}
