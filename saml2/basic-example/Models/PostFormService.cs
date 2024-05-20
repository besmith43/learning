using Microsoft.AspNetCore.Http;

namespace api.Models;

public class PostFormService
{
    public IFormCollection? Form { get; set; }
}