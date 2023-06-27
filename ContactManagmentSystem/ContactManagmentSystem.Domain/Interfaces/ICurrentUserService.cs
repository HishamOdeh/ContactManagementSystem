using System;
using System.Collections.Generic;

namespace ContactManagementSystem.Domain.Interfaces
{
    public interface ICurrentUserService
    {
        Guid UserId { get; }
        string UserName { get; }
        IList<string> Roles { get; }
        bool IsInRole(string role);
        void AddRole(string role);
    }
}
