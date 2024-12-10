
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Stack.Core;
using Stack.DTOs;
using Stack.DTOs.Enums;
using Stack.DTOs.Models;
using Stack.DTOs.Requests;
using Stack.Entities.Models;
using Stack.Repository.Common;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;

namespace Stack.ServiceLayer
{
    public class ApplicationUsersService
    {
        private readonly UnitOfWork unitOfWork;
        private readonly IConfiguration config;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMapper mapper;
        public ApplicationUsersService(UnitOfWork unitOfWork, IConfiguration config, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            this.config = config;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;

        }

        public async Task<ApiResponse<List<ApplicationUserDTO>>> GetAllCustomers()
        {
            ApiResponse<List<ApplicationUserDTO>> result = new ApiResponse<List<ApplicationUserDTO>>();
            try
            {
                var UsersListQuery = await unitOfWork.ApplicationUserManager.GetAllCustomers();
                var Users = UsersListQuery.ToList();

                if (Users != null)
                {
                    result.Succeeded = true;
                    result.Data = mapper.Map<List<ApplicationUserDTO>>(Users) ;
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

        public async Task<ApiResponse<ApplicationUserDTO>> GetCurrentCustomer()
        {
            ApiResponse<ApplicationUserDTO> result = new ApiResponse<ApplicationUserDTO>();
            try
            {
                var userID = await HelperFunctions.GetUserID(httpContextAccessor);
                if (userID != null)
                {
                    var user = await unitOfWork.ApplicationUserManager.GetCustomerById(userID);

                    if (user != null)
                    {
                        result.Succeeded = true;
                        //customize mapper
                        var config = new MapperConfiguration(
                            cfg =>
                            {
                                cfg.CreateMap<ApplicationUser, ApplicationUserDTO>();
                                cfg.CreateMap<Vehicle, VehicleDTO>()
                                   .ForMember(a => a.Customer, opt => opt.Ignore());

                            });
                        var _mapper = config.CreateMapper();

                        result.Data = _mapper.Map<ApplicationUserDTO>(user);
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Failed to Find customer");
                        return result;
                    }
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Failed to Find current user");
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

            
        public async Task<ApiResponse<ApplicationUser>> CreateCustomer(CreateCustomerModel model)
        {
            ApiResponse<ApplicationUser> result = new ApiResponse<ApplicationUser>();
            try
            {

                var currentUser = await unitOfWork.ApplicationUserManager.GetCustomerByUserName(model.UserName);
                if (currentUser == null)
                {
                    if (!await unitOfWork.RoleManager.RoleExistsAsync("Customer"))
                    {
                        var role = new IdentityRole();
                        role.Name = "Customer";
                        var res = await unitOfWork.RoleManager.CreateAsync(role);
                        await unitOfWork.SaveChangesAsync();
                    }

                    ApplicationUser user = new ApplicationUser
                    {
                        UserName = model.UserName,
                        Address = model.Address,

                    };

                    var createUserResult = await unitOfWork.ApplicationUserManager.CreateAsync(user, model.Password);

                    var test = await unitOfWork.SaveChangesAsync();

                    if (createUserResult.Succeeded)
                    {

                        var roleresult = await unitOfWork.ApplicationUserManager.AddToRoleAsync(user, "Customer");

                        var addToRoleResult = await unitOfWork.SaveChangesAsync();

                        if (roleresult.Succeeded == true)
                        {
                            result.Data = user;
                            result.Succeeded = true;
                            return result;
                        }
                        else
                        {
                            result.Succeeded = false;
                            result.Errors.Add("Failed To Create Customer");
                            result.ErrorType = ErrorType.LogicalError;
                            return result;
                        }
                    }
                    else
                    {
                        result.Succeeded = false;
                        foreach (var error in createUserResult.Errors)
                        {
                            result.Errors.Add(error.Description);
                        }
                        result.ErrorType = ErrorType.LogicalError;
                        return result;
                    }

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("This User Name is already exist !");
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


        public async Task<ApiResponse<ApplicationUserDTO>> CreateAdminAccount(RegisterModel model)
        {
            ApiResponse<ApplicationUserDTO> result = new ApiResponse<ApplicationUserDTO>();
            try
            {
                var currentUser = await unitOfWork.ApplicationUserManager.GetCustomerByUserName(model.UserName);
                if (currentUser == null)
                {
                    if (!await unitOfWork.RoleManager.RoleExistsAsync("Administrator"))
                    {
                        var role = new IdentityRole();
                        role.Name = "Administrator";
                        var res = await unitOfWork.RoleManager.CreateAsync(role);
                        await unitOfWork.SaveChangesAsync();
                    }

                    ApplicationUser user = new ApplicationUser
                    {
                        UserName = model.UserName,
                    };

                    var createUserResult = await unitOfWork.ApplicationUserManager.CreateAsync(user, model.Password);

                    var test = await unitOfWork.SaveChangesAsync();

                    if (createUserResult.Succeeded)
                    {

                        var roleresult = await unitOfWork.ApplicationUserManager.AddToRoleAsync(user, "Administrator");

                        var addToRoleResult = await unitOfWork.SaveChangesAsync();

                        if (roleresult.Succeeded == true)
                        {
                            result.Data = mapper.Map<ApplicationUserDTO>(user);
                            result.Succeeded = true;
                            return result;
                        }
                        else
                        {
                            result.Succeeded = false;
                            result.Errors.Add("Failed To Create Adminstrator");
                            result.ErrorType = ErrorType.LogicalError;
                            return result;
                        }
                    }
                    else
                    {
                        result.Succeeded = false;
                        foreach (var error in createUserResult.Errors)
                        {
                            result.Errors.Add(error.Description);
                        }
                        result.ErrorType = ErrorType.LogicalError;
                        return result;
                    }

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("This user name is already exist !");
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

        public async Task<ApiResponse<JwtAccessToken>> LoginAsync(LoginModel model)
        {
            ApiResponse<JwtAccessToken> result = new ApiResponse<JwtAccessToken>();
            try
            {

                var UserIsExistInternal = await unitOfWork.ApplicationUserManager.GetCustomerByUserName(model.UserName);

                if (UserIsExistInternal == null)
                {
                    result.Succeeded = false;
                    result.Errors.Add("Account does not exist");
                    result.ErrorType = ErrorType.Request;
                    return result;
                }

                else
                {
                    bool CheckPasswordRes = await unitOfWork.ApplicationUserManager.CheckPasswordAsync(UserIsExistInternal, model.Password);
                    //retun jwt token and update user data
                    if (CheckPasswordRes)
                    {
                        return await GenerateUserToken(UserIsExistInternal);
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Account Unavailable, Please contact the system adminstrator");
                        result.ErrorType = ErrorType.LogicalError;
                        return result;
                    }
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


        async public Task<ApiResponse<JwtAccessToken>> GenerateUserToken(ApplicationUser user)
        {
            ApiResponse<JwtAccessToken> result = new ApiResponse<JwtAccessToken>();
            try
            {
                ClaimsIdentity claims = new ClaimsIdentity(new[]
                       {
                                new Claim(ClaimTypes.Name, user.UserName),
                                new Claim(ClaimTypes.NameIdentifier, user.Id)
                            });

                IList<string> userRoles = await unitOfWork.ApplicationUserManager.GetRolesAsync(user);

                if (userRoles != null && userRoles.Count() > 0)
                {
                    foreach (string role in userRoles)
                    {
                        claims.AddClaim(new Claim(ClaimTypes.Role, role));
                    }
                }

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("Token:Key").Value));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddDays(0.25), // Set Token Validity Period . 
                    SigningCredentials = creds
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);

                result.Data = new JwtAccessToken();
                result.Data.Token = tokenHandler.WriteToken(token);
                result.Data.Expiration = token.ValidTo;
                result.Succeeded = true;
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


