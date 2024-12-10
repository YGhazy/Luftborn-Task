
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Stack.Core;

using Stack.DTOs;
using Stack.DTOs.Enums;
using Stack.DTOs.Models;
using Stack.DTOs.Requests;
using Stack.Entities.Models;
using Stack.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stack.ServiceLayer
{
    public class InitializerService
    {
        private readonly UnitOfWork unitOfWork;
        private readonly IConfiguration config;
        private readonly IMapper mapper;
        private readonly List<CustomerJson> Customers;
        private readonly VehicleService VehicleService;
        private readonly ApplicationUsersService ApplicationUsersService;

        public InitializerService(UnitOfWork unitOfWork, IConfiguration config, IMapper mapper, IOptions<List<CustomerJson>> Customers, VehicleService VehicleService, ApplicationUsersService ApplicationUsersService)
        {
            this.unitOfWork = unitOfWork;
            this.config = config;
            this.mapper = mapper;
            this.Customers = Customers.Value;
            this.VehicleService = VehicleService;
            this.ApplicationUsersService = ApplicationUsersService;

        }

        public async Task<ApiResponse<bool>> Initializer()
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                RegisterModel modelToCreateAdmin = new RegisterModel()
                {
                    Password = "P@ssw0rd",
                    UserName = "Admin"
                };

                var creationAdminResult = await ApplicationUsersService.CreateAdminAccount(modelToCreateAdmin);
                if (!creationAdminResult.Succeeded)
                {
                    result.Succeeded = false;
                    result.Errors.Add(creationAdminResult.Errors[0]);
                    return result;
                }

                if (Customers != null)
                {
                    foreach (var Customer in Customers)
                    {
                        CreateCustomerModel CreateCustomerModel = new CreateCustomerModel()
                        {
                            Address = Customer.Address,
                            UserName = Customer.UserName,
                            Password = "P@ssw0rd"
                        };
                        var creationCustomerResult = await ApplicationUsersService.CreateCustomer(CreateCustomerModel);
                        if (!creationCustomerResult.Succeeded)
                        {
                            result.Succeeded = false;
                            result.Errors.Add("Failed to create " + Customer.UserName);
                            return result;
                        }
                        foreach (var vehicle in Customer.Vehicles)
                        {
                            CreateVehicleModel CreateVehicleModel = new CreateVehicleModel()
                            {
                                Number = vehicle.Number,
                                VIN = vehicle.VIN,
                                CustomerID = creationCustomerResult.Data.Id
                            };

                            var creationVehicleResult = await VehicleService.CreateVehicle(CreateVehicleModel);
                            if (!creationVehicleResult.Succeeded)
                            {
                                result.Succeeded = false;
                                result.Errors.Add("Failed to create vehicle :" + vehicle.Number);
                                return result;
                            }

                        }
                    }
                    result.Succeeded = true;
                    return result;

                }

                result.Succeeded = false;
                result.Errors.Add("Failed to find customer json object");
                return result;

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


