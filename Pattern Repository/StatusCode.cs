using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pattern_Repository
{
    public enum StatusCode
    {
        DataNotFound = 1,
        OK = 200,
        EmptyResource = 202,
        DataAlreadyExists = 300,
        ClientError = 400,
        InternalServerError = 500,
    }
}
