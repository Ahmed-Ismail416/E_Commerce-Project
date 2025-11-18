using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions
{
    public sealed class  UnUthorizedException(string? message = "You are not authorized to access this resource.") : Exception(message)
    {

    }
}
