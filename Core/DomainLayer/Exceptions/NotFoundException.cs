using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions
{
    public class NotFoundException(string Tentity, string id) : Exception(string.Format("The {0} with id {1} was not found", Tentity, id))
    {
    }
}
