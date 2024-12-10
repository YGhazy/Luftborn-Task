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
    public class VehiclesController : BaseResultHandlerController<VehicleService>
    {
        public VehiclesController(VehicleService _service) : base(_service)
        {

        }

        [HttpGet("GetAllVehicles")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetAllVehicles()
        {
            return await GetResponseHandler(async () => await service.GetAllVehicles());
        }

        [HttpGet("CustomerVehiclesPing")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> CustomerVehiclesPing()
        {
            return await GetResponseHandler(async () => await service.CustomerVehiclesPing());
        }
    }
}
