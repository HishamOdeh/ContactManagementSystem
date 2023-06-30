using System;
using MediatR;

namespace ContactManagementSystem.Shared.Features.Contacts
{
    public class SearchContactRequest : IRequest<SearchContactRequest.Response>
    {
        public const string RouteTemplate = "/api/contacts/searches";

        public record Response(List<ListContactDto> Contacts);
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public bool IsEmpty()
        {
            return string.IsNullOrEmpty(LastName) && string.IsNullOrEmpty(FirstName);
        }
    }

  //  public record ListContactDto(Guid Id, string Name int TotalCount);
}
