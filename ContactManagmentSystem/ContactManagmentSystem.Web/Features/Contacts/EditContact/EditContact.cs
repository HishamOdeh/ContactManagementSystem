using System.Threading;
using System.Threading.Tasks;
using ContactManagementSystem.Shared;
using ContactManagementSystem.Shared.Features.Contacts;
using ContactManagementSystem.Web.Services;
using MediatR;

namespace ContactManagementSystem.Web.Features.Contacts.EditContact
{
    public class EditContact : IRequestHandler<EditContactRequest, CommandResponse>
    {
        private readonly IHttpService _httpService;

        public EditContact(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<CommandResponse> Handle(EditContactRequest request, CancellationToken cancellationToken)
        {
            return await _httpService.SendPostAsync<EditContactRequest>(EditContactRequest.RouteTemplate, request);
        }
    }
}
