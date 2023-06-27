using System.Threading;
using System.Threading.Tasks;
using ContactManagementSystem.Shared;
using ContactManagementSystem.Shared.Features.Contacts;
using ContactManagementSystem.Web.Services;
using MediatR;

namespace ContactManagementSystem.Web.Features.Contacts.AddContact
{
    public class AddContact : IRequestHandler<AddContactRequest, CommandResponse>
    {
        private readonly IHttpService _httpService;

        public AddContact(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<CommandResponse> Handle(AddContactRequest request, CancellationToken cancellationToken)
        {
            return await _httpService.SendPostAsync<AddContactRequest>(AddContactRequest.RouteTemplate, request);
        }
    }
}
