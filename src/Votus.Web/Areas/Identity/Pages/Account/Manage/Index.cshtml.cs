using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Votus.Domain.Services;
using Votus.Infrastructure.Identity;

namespace Votus.Web.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<WebApplicationUser> _userManager;
        private readonly SignInManager<WebApplicationUser> _signInManager;
        private readonly IPersonService _personService;

        public IndexModel(
            UserManager<WebApplicationUser> userManager,
            SignInManager<WebApplicationUser> signInManager,
            IPersonService personService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _personService = personService;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [DataType(DataType.Date)]
            [Display(Name = "Data de Nascimento")]
            public DateTime BirthDate { get; set; }

            [Display(Name = "Nome")]
            public string FullName { get; set; }

            [EmailAddress]
            [Display(Name = "E-mail")]
            public string Email { get; set; }
        }

        private async Task LoadAsync(WebApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var userDetail = await _userManager.GetUserAsync(User);
            
            Username = userName;

            Input = new InputModel
            {
                BirthDate = userDetail.BirthDate,
                Email = userDetail.Email,
                FullName = userDetail.FullName
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);

                return Page();
            }

            user.FullName = Input.FullName;
            user.Email = Input.Email;
            user.BirthDate = Input.BirthDate;

            var update =  await _userManager.UpdateAsync(user);

            if (update.Errors.Any())
            {
                await LoadAsync(user);
                return Page();
            }

            _personService.UpdatePerson(user);

            await _signInManager.RefreshSignInAsync(user);

            StatusMessage = "Seu perfil foi atualizado";

            return RedirectToPage();
        }
    }
}
