using System;
using MediatR;

namespace ContactManagementSystem.Shared.Features.Contacts
{
    public record GetContactRequest(Guid Id) : IRequest<GetContactRequest.Response>
    {
        public const string RouteTemplate = "/api/contact/{id}";
        public record Response(ContactFormModel Contact);
    }
}
