using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions
{
    public sealed class UserNotFoundException(object? value) : NotFoundException($"User with identifier `{value}` was not found")
    {
    }
}
