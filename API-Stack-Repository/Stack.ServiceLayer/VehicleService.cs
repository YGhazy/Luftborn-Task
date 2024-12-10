
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Stack.API.Hubs;
using Stack.Core;

using Stack.DTOs;
using Stack.DTOs.Enums;
using Stack.DTOs.Models;
using Stack.Entities.Models;
using Stack.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stack.ServiceLayer
{
    public class VehicleService
    {
        private readonly UnitOfWork unitOfWork;
        private readonly IConfiguration config;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly NotificationsHub notificationsHub;

        public VehicleService(UnitOfWork unitOfWork, IConfiguration config, IMapper mapper, IHttpContextAccessor httpContextAccessor,NotificationsHub notificationsHub)
        {
            this.unitOfWork = unitOfWork;
            this.config = config;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
            this.notificationsHub = notificationsHub;
        }


        public async Task<ApiResponse<bool>> CreateVehicle(CreateVehicleModel model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
              
                var VehicleExistsQ = await unitOfWork.VehicleManager.GetAsync(t => t.Number == model.Number);
                var VehicleExists = VehicleExistsQ.FirstOrDefault();
                
                if (VehicleExists == null)
                {
                    Vehicle VehicleToCreat = new  Vehicle{
                        Number=model.Number,
                        VIN=model.VIN,
                        CustomerId=model.CustomerID
                    };
                    var creationResult = await unitOfWork.VehicleManager.CreateAsync(VehicleToCreat);
                    var saveResult = await unitOfWork.SaveChangesAsync();

                    if (saveResult)
                    {
                        result.Succeeded = true;
                        result.Data = true;
                        return result;

                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Data = false;
                        result.Errors.Add("Failed to create Vehicle ");
                        return result;

                    }
                }

                else
                {
                    result.Succeeded = false;
                    result.Data = false;
                    result.Errors.Add("Vehicle already exists");
                    return result;

                }

            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Errors.Add(ex.Message);
                result.ErrorType = ErrorType.SystemError;
                return result;
            }

        }

        public async Task<ApiResponse<List<VehicleDTO>>> GetAllVehicles()
        {
            ApiResponse<List<VehicleDTO>> result = new ApiResponse<List<VehicleDTO>>();
            try
            {
                var VehiclesQ = await unitOfWork.VehicleManager.GetAsync(includeProperties: "Customer");
                var Vehicles = VehiclesQ.ToList();

                if (Vehicles != null)
                {

                    result.Succeeded = true;
                    result.Data = mapper.Map<List<VehicleDTO>>(Vehicles) ;
                    return result;
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Failed to Find customers");
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Errors.Add(ex.Message);
                return result;
            }
        }

        public async Task<ApiResponse<ApplicationUserDTO>> CustomerVehiclesPing( )
        {
            ApiResponse<ApplicationUserDTO> result = new ApiResponse<ApplicationUserDTO>();
            try
            {
                var userID = await HelperFunctions.GetUserID(httpContextAccessor);
                if (userID != null)
                {
                    var user = await unitOfWork.ApplicationUserManager.GetCustomerById(userID);

                    if (user != null && await unitOfWork.ApplicationUserManager.IsInRoleAsync(user, "Customer"))
                    {
                        foreach (var Vehicle in user.Vehicles)
                        {
                            var todaysDate = await HelperFunctions.GetEgyptsCurrentLocalTime();
                            Random random = new Random();
                            int randomNumberLessThan6 = random.Next(6);
                            var Status = (Status)randomNumberLessThan6;
                            Vehicle.LastPing = todaysDate;
                            Vehicle.Status = Status.ToString();
                            await notificationsHub.Update();
                            var creationResult = await unitOfWork.VehicleManager.UpdateAsync(Vehicle);
                            var saveResult = await unitOfWork.SaveChangesAsync();

                            if (!saveResult)
                            {
                                result.Succeeded = false;
                                result.Errors.Add("Failed to update Vehicle :" + Vehicle.Number);
                                return result;

                            }
                        }
                        var config = new MapperConfiguration(
                         cfg =>
                           {
                             cfg.CreateMap<ApplicationUser, ApplicationUserDTO>();
                             cfg.CreateMap<Vehicle, VehicleDTO>()
                            .ForMember(a => a.Customer, opt => opt.Ignore());

                          });
                        var _mapper = config.CreateMapper();
                        result.Data = _mapper.Map<ApplicationUserDTO>(user);
                        result.Succeeded = true;
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Failed to find current customer ");
                        return result;

                    }
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Failed to find current customer ");
                    return result;

                }


            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Errors.Add(ex.Message);
                result.ErrorType = ErrorType.SystemError;
                return result;
            }

        }


    }
}


