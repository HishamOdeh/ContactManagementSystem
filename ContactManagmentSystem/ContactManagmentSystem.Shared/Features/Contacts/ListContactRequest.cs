using System;
using System.Collections.Generic;
using MediatR;

namespace ContactManagementSystem.Shared.Features.Contacts
{
    public record ListContactRequest() : IRequest<ListContactRequest.Response>
    {
        public const string RouteTemplate = "/api/contacts";
        public record Response(List<ListContactDto> Contacts);
    }

    public record ListContactDto(Guid Id, string FirstName,string LastName, string PhoneNumber,
                            string Email,DateTime BirthDate );
}
