
using System;
using ContactManagementSystem.Domain.Interfaces;

namespace ContactManagementSystem.Api.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
