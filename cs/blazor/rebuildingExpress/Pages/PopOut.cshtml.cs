using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace rebuildingExpress.Pages;

[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
[IgnoreAntiforgeryToken]
public class PopOutModel : PageModel
{
    // public string? RequestId { get; set; }

    // public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

    private readonly ILogger<PopOutModel> _logger;

    public PopOutModel(ILogger<PopOutModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        // RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
    }
}
