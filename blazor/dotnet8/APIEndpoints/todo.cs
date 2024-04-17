using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet8.BackgroundService;

namespace dotnet8.APIEndpoints;

// this idea came from the blog post linked below
// https://www.tessferrandez.com/blog/2023/10/31/organizing-minimal-apis.html

public static class todo
{
    public static void RegisterToDoEndpoints(this WebApplication app)
    {
        app.MapGet("/todo", () => new { Message = "Hello World!" });

        app.MapGet("/count", () => {
            var service = app.Services.GetHostedService<BackgroundNetCheckerService>();
            return new { Count = service.GetExecutionCount() };
        });
    }


}