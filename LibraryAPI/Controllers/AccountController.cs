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
        public async Task<LoginReturnDTO> Login(LoginDTO loginDTO) 
        {
            var user = await _userManager.FindByNameAsync(loginDTO.Username);
            if (user == null) 
            {
                throw new ArgumentException("User not exists");
            }
            var result = await _signInManager.PasswordSignInAsync(loginDTO.Username, loginDTO.Passwd, false, false);

            if (!result.Succeeded) 
            {
                throw new ArgumentException("Loggin Error");
            }
            var userReturn = new LoginReturnDTO();
            userReturn.FirstName = user.FirstName;
            userReturn.LastName = user.LastName;
            userReturn.UserName = user.UserName;

            return userReturn;
        }

        [HttpPost("Register")]
        public async Task<RegisterReturnDTO> Register(RegisterDTO registerDTO) 
        {
            var newUser = new User();
            newUser.UserName = registerDTO.Username;
            newUser.FirstName = registerDTO.FirstName;
            newUser.LastName = registerDTO.LastName;
            newUser.Email = registerDTO.Email;
            var result = await _userManager.CreateAsync(newUser, registerDTO.Passwd);

            if (!result.Succeeded) 
            {
                throw new ArgumentException("Register Error");
            }
            var registerReturn = new RegisterReturnDTO();
            registerReturn.FirstName = newUser.FirstName;
            registerReturn.LastName = newUser.LastName;
            registerReturn.UserName = newUser.UserName;

            return registerReturn;
        }

        [HttpGet]
        public async Task<ActionResult<User>> SelectOneUser(string userId) 
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null) 
            {
                throw new ArgumentException("User does not exists");
            }

            return user;
        }


    }

}
