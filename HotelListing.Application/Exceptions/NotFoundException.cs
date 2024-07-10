using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListing.Application.Exceptions
{
    public class NotFoundException(
        string name, 
        object key
    ) : ApplicationException($"{name} ({key}) was not found.");
}
