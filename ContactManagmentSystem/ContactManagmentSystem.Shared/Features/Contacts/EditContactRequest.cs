using MediatR;

namespace ContactManagementSystem.Shared.Features.Contacts
{
    public record EditContactRequest(ContactFormModel Contact) : IRequest<CommandResponse>
    {
        public const string RouteTemplate = "/api/contact";
    }
}
