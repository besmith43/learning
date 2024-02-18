using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace hello.Shared.Models;
public class ErrorMessage
{
    [JsonPropertyName("message")]
    public string Message { get; set; }
}