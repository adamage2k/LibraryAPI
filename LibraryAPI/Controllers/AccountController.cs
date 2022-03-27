using LibraryAPI.DTOs;
using LibraryAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("Login")]
        public async Task<User> Login(LoginDTO loginDTO) 
        {
            var user = await _userManager.FindByNameAsync(loginDTO.Username);
            if (user == null) 
            {
                throw new ArgumentException("User not exsists");
            }
            var result = await _signInManager.PasswordSignInAsync(loginDTO.Username, loginDTO.Passwd, false, false);

            if (!result.Succeeded) 
            {
                throw new ArgumentException("Loggin Error");
            }

            return user;
        }

    }

}
