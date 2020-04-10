using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using SftMembership.web.Domain.Models;
using SftMembership.web.Models;
using SftMembership.web.Persistance.Contexts;

namespace SftMembership.web.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthController> _logger;
        private readonly SftDbContext _sftDbContext;

        public AuthController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IMapper mapper,
            ILogger<AuthController> logger,
            SftDbContext sftDbContext
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _logger = logger;
            _sftDbContext = sftDbContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {

                var userToRegister = _mapper.Map<User>(registerViewModel);
                userToRegister.SubscriptionPlan = _sftDbContext.SubscriptionPlans.FirstOrDefault(x =>x.Name == registerViewModel.SubscriptionPlanName);

                var result = await _userManager.CreateAsync(userToRegister, registerViewModel.Password);
                if (result.Succeeded)
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(userToRegister);
                    var confirmationLink = Url.Action("ConfirmEmail", "Auth",
                                                new { userName = userToRegister.UserName, token = token }, Request.Scheme);
                    _logger.Log(LogLevel.Warning, confirmationLink);

                    return RedirectToAction("Confirm", "Auth", new { confirmationLink = confirmationLink });
                }

                //If there is any error while saving user 
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View();
        }

        [HttpGet]
        public IActionResult Confirm(string confirmationLink)
        {
            ViewBag.Confirmation = confirmationLink;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userName, string token)
        {
            if (string.IsNullOrEmpty(userName)  || string.IsNullOrEmpty(token))
                return RedirectToAction("Index", "Home");

            var existingUser = _sftDbContext.Users.Include(x => x.SubscriptionPlan).FirstOrDefault(x => x.UserName ==userName); 

            if(existingUser == null)
            {
                ViewBag.Message = $"The userName :{userName} is not found:";
                return View();
            }

            var result = await _userManager.ConfirmEmailAsync(existingUser, token);

            if (result.Succeeded)
            {
                ViewBag.Message = "Email been verified successfully";
                var userDetailViewModel = _mapper.Map<UserDetialViewModel>(existingUser);
                return View(userDetailViewModel);
            }

            ViewBag.Message = "Email can not be confirmed";
            return View();
        }



    }
}