using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaloriesAppBackend.Models
{
    public class TokenResponseDto
    {
        public string token { get; set; }
        public int expiration { get; set; }
    }
}
