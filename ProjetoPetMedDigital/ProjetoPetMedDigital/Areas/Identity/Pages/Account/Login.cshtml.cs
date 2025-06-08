using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ProjetoPetMedDigital.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;

        // O construtor agora injeta UserManager também
        public LoginModel(SignInManager<IdentityUser> signInManager,
                          ILogger<LoginModel> logger,
                          UserManager<IdentityUser> userManager) // <-- NOVO: Injetar UserManager
        {
            _userManager = userManager; // <-- NOVO: Atribuir UserManager
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/"); // Padrão é a raiz do site

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            // O returnUrl padrão (~) está mapeado para o Login, então precisamos redirecionar explicitamente para a home correta.
            returnUrl ??= Url.Content("~/");

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");

                    // --- INÍCIO DA LÓGICA DE REDIRECIONAMENTO POR PERFIL ---
                    var user = await _userManager.FindByEmailAsync(Input.Email); // Obtém o usuário que acabou de fazer login

                    if (user != null)
                    {
                        if (await _userManager.IsInRoleAsync(user, "Administrador"))
                        {
                            return LocalRedirect(Url.Content("~/Home/AdminHome"));
                        }
                        else if (await _userManager.IsInRoleAsync(user, "Veterinario"))
                        {
                            return LocalRedirect(Url.Content("~/Home/VeterinarioHome"));
                        }
                        else if (await _userManager.IsInRoleAsync(user, "Secretaria"))
                        {
                            return LocalRedirect(Url.Content("~/Home/SecretariaHome"));
                        }
                        else
                        {
                            // Se o usuário não tiver nenhum perfil específico, redireciona para a Home padrão (Home/Index)
                            // ou para uma página de "acesso negado" ou de "perfil não atribuído".
                            return LocalRedirect(Url.Content("~/Home/Index")); // Padrão para usuários sem perfis específicos
                        }
                    }
                    // --- FIM DA LÓGICA DE REDIRECIONAMENTO POR PERFIL ---

                    // Este retorno só será alcançado se o 'user' for null após o login Succeeded (o que não deveria acontecer)
                    // ou se a lógica de perfis acima não o redirecionou por algum motivo inesperado.
                    return LocalRedirect(returnUrl); // Volta para a URL de retorno (que é padrão '~/' ou específica)
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}