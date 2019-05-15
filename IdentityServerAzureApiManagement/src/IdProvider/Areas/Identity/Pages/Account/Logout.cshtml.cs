using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace IdProvider.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        private readonly IIdentityServerInteractionService _interactionService;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<LogoutModel> _logger;

        public LogoutModel(
            IIdentityServerInteractionService interactionService,
            SignInManager<IdentityUser> signInManager, 
            ILogger<LogoutModel> logger)
        {
            _interactionService = interactionService;
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task<IActionResult> OnGet(string logoutId = null)
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");

            if (!String.IsNullOrEmpty(logoutId))
            {
                var logoutContext = await _interactionService.GetLogoutContextAsync(logoutId);

                SignOutIFrameUrl = logoutContext.SignOutIFrameUrl;
                PostLogoutRedirectUri = logoutContext.PostLogoutRedirectUri;
            }

            return Page();
        }

        public string SignOutIFrameUrl { get; set; }

        public string PostLogoutRedirectUri { get; set; }

        public async Task<IActionResult> OnPost(string returnUrl = null, string logoutId = null)
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");

            if (!String.IsNullOrEmpty(logoutId))
            {
                var logoutContext = await _interactionService.GetLogoutContextAsync(logoutId);

                SignOutIFrameUrl = logoutContext.SignOutIFrameUrl;
                PostLogoutRedirectUri = logoutContext.PostLogoutRedirectUri;
            }
            else
            {
                if (returnUrl != null)
                {
                    return LocalRedirect(returnUrl);
                }
            }

            return Page();
        }
    }
}