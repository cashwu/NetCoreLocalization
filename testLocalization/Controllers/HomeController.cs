using System;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace testLocalization.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStringLocalizer<HomeController> _localizer;
        private IHttpContextAccessor _httpContextAccessor;

        public HomeController(ILogger<HomeController> logger,
                              IStringLocalizer<HomeController> localizer,
                              IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _localizer = localizer;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// http://localhost:23362/?culture=en-US
        /// http://localhost:23362/?culture=fr-FR
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            LogCulture();

            return Content(_localizer["Name"]);
        }

        /// <summary>
        /// http://localhost:23362/home/see?culture=en-US
        /// http://localhost:23362/home/see?culture=fr-FR
        /// </summary>
        /// <returns></returns>
        public IActionResult See()
        {
            LogCulture();

            return View();
        }

        private void LogCulture()
        {
            var cultureFeature = _httpContextAccessor.HttpContext.Features.Get<IRequestCultureFeature>();
            _logger.LogInformation(cultureFeature.RequestCulture.Culture.Name);
        }
    }
}