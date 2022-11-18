using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace Notepad.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Credential Credential { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(){
            if (!ModelState.IsValid) return Page();

            //Verifying Credential
            if (Credential.UserName == "Gordon" && Credential.Password == "Gordon.dindi"){
                // Creating Security Context
                var claims = new List<Claim> { 
                    new Claim(ClaimTypes.Name, "Gordon"),
                    new Claim(ClaimTypes.Email, "Gordon@gmail.com")
                };

                var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);

                return RedirectToPage("/Note");
            }
            return Page();
        }
    }

    public class Credential{
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
