using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicWASMExample.Shared.Models;

public class ErrorMessage
{
    public string Message { get; set; }
    public string StackTrace { get; set; }
    public int HttpStatusCode { get; set; }
}