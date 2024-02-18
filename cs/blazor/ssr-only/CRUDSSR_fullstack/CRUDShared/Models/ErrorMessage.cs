using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;

namespace CRUDShared.Models;

public class ErrorMessage
{
    public string Message { get; set; } = string.Empty;
    public string? StackTrace { get; set; }
    public HttpStatusCode StatusCode { get; set; }
}