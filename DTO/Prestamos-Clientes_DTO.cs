using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Prestamos_Clientes_DTO
    {
        public Prestamos_DTO Prestamos_DTO { get; set; }
        public Cliente_DTO Cliente { get; set; }
    }
}
