using System;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace ContactManagementSystem.Shared.Features.Contacts
{
    public class SearchContactRequest : IRequest<SearchContactRequest.Response>
    {
        public const string RouteTemplate = "/api/contacts/searches";

        public record Response(List<ListContactDto> Contacts);
		[Required(ErrorMessage = "First Name is required.")]

		public string FirstName { get; set; }
		[Required(ErrorMessage = "Last Name is required.")]

		public string LastName { get; set; }

        public bool IsEmpty()
        {
            return string.IsNullOrEmpty(LastName) && string.IsNullOrEmpty(FirstName);
        }
    }

  //  public record ListContactDto(Guid Id, string Name int TotalCount);
}
