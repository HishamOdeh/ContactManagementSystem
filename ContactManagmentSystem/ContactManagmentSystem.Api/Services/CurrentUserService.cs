
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using ContactManagementSystem.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace ContactManagementSystem.Api.Services
{
    public class CurrentUserService : ICurrentUserService
    {


        public CurrentUserService(Guid userId)
        {
            UserId = userId;
        }
        public CurrentUserService(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            if (httpContextAccessor.HttpContext?.User.Identity.IsAuthenticated == true)
            {
                if (configuration["AuthenticationType"] == "AzureAdB2C")
                {

                    UserId = Guid.Parse(httpContextAccessor.HttpContext?.User?.FindFirstValue("extension_EpilinkUserId"));

                    UserName = httpContextAccessor.HttpContext?.User.Identity.Name;
                }
                else
                {
                    UserName = httpContextAccessor.HttpContext?.User.Claims.First(x => x.Type.ToLower() == "preferred_username").Value;
                    UserId = Guid.Parse(httpContextAccessor.HttpContext?.User.Claims.First(x => x.Type.ToLower() == "epilinkuserid").Value);
                }

                this.Roles = new List<string>();

                foreach (var role in httpContextAccessor.HttpContext?.User.Claims.Where(x => x.Type == ClaimTypes.Role))
                {
                    this.Roles.Add(role.Value);
                }
            }
        }

        public Guid UserId { get; }
        public string UserName { get; private set; }
        public IList<string> Roles { get; private set; }

        public void AddRole(string role)
        {
            this.Roles.Add(role);
        }

        public bool IsInRole(string role)
        {
            if (Roles == null)
            {
                return false;
            }
            return Roles.Any(x => x.ToLower().Replace(" ", "").Replace("epilink", "") ==
                                  role.ToLower().Replace(" ", "").Replace("epilink", ""));
        }

    }
}
