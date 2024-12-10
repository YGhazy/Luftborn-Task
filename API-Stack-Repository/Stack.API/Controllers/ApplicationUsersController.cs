using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stack.API.Controllers.Common;
using Stack.DTOs.Models;
using Stack.DTOs.Requests;
using Stack.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 

namespace Stack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Require Authorization to access API endpoints . 
    public class ApplicationUsersController : BaseResultHandlerController<ApplicationUsersService>
    {
        public ApplicationUsersController(ApplicationUsersService _service) : base(_service)
        {

        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(LoginModel model)
        {
            return await GetResponseHandler(async () => await service.LoginAsync(model));
        }

        [HttpGet("GetAllCustomers")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetAllCustomers()
        {
            return await GetResponseHandler(async () => await service.GetAllCustomers());
        }


        [HttpGet("GetCurrentCustomer")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetCurrentCustomer()
        {
            return await GetResponseHandler(async () => await service.GetCurrentCustomer());
        }
    }
}
