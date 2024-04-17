using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BasicWASMExample.Shared.Models;
using MudBlazor;

namespace BasicWASMExample.Controllers;

[ApiController]
[Route("api/QuickLinks")]
public class QuickLinksController : ControllerBase
{
    public QuickLinksController()
    {

    }

    [HttpGet]
    public IEnumerable<QuickLink> Get()
    {
        return new List<QuickLink>()
        {
            new QuickLink { name = "Google", url = "https://www.google.com", icon = Icons.Custom.Brands.Google, order = 1, Selector = "1" },
            new QuickLink { name = "Git", url = "https://git-scm.com", icon = "fa-brands fa-git-alt", order = 2, Selector = "1" },
            new QuickLink { name = "Microsoft", url = "https://www.microsoft.com", icon = Icons.Custom.Brands.Microsoft, order = 2, Selector = "2" },
            new QuickLink { name = "Github", url = "https://www.github.com", icon = Icons.Custom.Brands.GitHub, order = 3, Selector = "2" },
            new QuickLink { name = "Apple", url = "https://www.apple.com", icon = Icons.Custom.Brands.Apple, order = 4, Selector = "2" },
            new QuickLink { name = "Facebook", url = "https://www.facebook.com", icon = Icons.Custom.Brands.Facebook, order = 5, Selector = "2" },
            new QuickLink { name = "Twitter", url = "https://www.twitter.com", icon = Icons.Custom.Brands.Twitter, order = 6, Selector = "2" },
            new QuickLink { name = "Instagram", url = "https://www.instagram.com", icon = Icons.Custom.Brands.Instagram, order = 7, Selector = "2" },
            new QuickLink { name = "Reddit", url = "https://www.reddit.com", icon = Icons.Custom.Brands.Reddit, order = 8, Selector = "2" }
        };
    }
}