using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ContactManagementSystem.Shared.Features.Contacts
{
    public record DeleteContactRequest(Guid ContactId) : IRequest<CommandResponse>
    {
        public const string RouteTemplate = "/api/contacts/{ContactId}";
    }
}
