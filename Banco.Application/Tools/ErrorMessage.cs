using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco.Application.Tools
{
    public class ErrorMessage
    {
        public object mensaje {  get; set; }
        public string token { get; set; }
        public bool valido { get; set; }
    }
}
