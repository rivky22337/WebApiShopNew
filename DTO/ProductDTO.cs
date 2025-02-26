using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
   public record SingleProductDTO(int ProductId, string ProductName, int Price,int CategoryId, string Description);
   public record ListProductDTO(int ProductId, string ProductName, int Price,string Category);
}
