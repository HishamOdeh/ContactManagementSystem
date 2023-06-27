using MediatR;

namespace ContactManagementSystem.Shared.Features.Contacts
{
    public record AddContactRequest(ContactFormModel Contact) : IRequest<CommandResponse>
    {
        public const string RouteTemplate = "/api/contact";
    }
}
