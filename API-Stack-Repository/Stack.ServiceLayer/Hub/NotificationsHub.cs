using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Stack.Core;
using Stack.DTOs;
using Stack.DTOs.Models;
using Stack.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stack.API.Hubs
{
    [Authorize]
    public class NotificationsHub : Hub
    {
        private readonly UnitOfWork unitOfWork;
        private readonly IMapper mapper;
        protected IHubContext<NotificationsHub> _context;

        public NotificationsHub(UnitOfWork unitOfWork, IMapper mapper, IHubContext<NotificationsHub> context)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this._context = context;
        }

        public async override Task OnConnectedAsync()
        {
            var username = Context.User.Identity.Name;
            var user = await unitOfWork.ApplicationUserManager.FindByNameAsync(username);
            ConnectionId conId = new ConnectionId
            {
                Id = Context.ConnectionId,
                ApplicationUserId = user.Id
            };
            await unitOfWork.ConnectionIdsManager.CreateAsync(conId);
            await unitOfWork.SaveChangesAsync();
        }

        public async override Task OnDisconnectedAsync(Exception exception)
        {
            var conId = await unitOfWork.ConnectionIdsManager.GetByIdAsync(Context.ConnectionId);
            await unitOfWork.ConnectionIdsManager.RemoveAsync(conId);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<ApiResponse<bool>> Update()
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var conIdsQ = await unitOfWork.ConnectionIdsManager.GetAsync();
                var conIds = conIdsQ.ToList();
                if (conIds != null && conIds.Count() > 0)
                {
                    await _context.Clients.Clients(conIds.Select(c => c.Id).ToList()).SendAsync("ping");
                }
                result.Succeeded = true;
                result.Data = true;
                return result;

            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Errors.Add(ex.Message);
                return result;
            }

        }
    }
}
