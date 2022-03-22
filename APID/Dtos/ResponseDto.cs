using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APID.Dtos
{
   public class ResponseDto
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public object Result { get; set; }        
        public List<string> ErrorMessages { get; set; }
    }
}