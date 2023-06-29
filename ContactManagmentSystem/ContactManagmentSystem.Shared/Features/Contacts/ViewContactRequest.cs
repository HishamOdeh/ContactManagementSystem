using System;
using MediatR;

namespace ContactManagementSystem.Shared.Features.Contacts
{
    public record ViewContactRequest(Guid Id) : IRequest<ViewContactRequest.Response>
    {
        public const string RouteTemplate = "/api/contacts/views/{Id}";

        public record Response(ViewContactDto Contact);
    }

    public record ViewContactDto(Guid Id, string FirstName, string LastName, string PhoneNumber,
                            string Email, DateTime BirthDate);
}
