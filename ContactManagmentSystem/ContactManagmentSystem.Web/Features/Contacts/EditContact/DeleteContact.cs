using System.Threading;
using System.Threading.Tasks;
using ContactManagementSystem.Shared;
using ContactManagementSystem.Shared.Features.Contacts;
using ContactManagementSystem.Web.Services;
using MediatR;

namespace ContactManagementSystem.Web.Features.Contacts.EditContact
{
    public class DeleteContact : IRequestHandler<DeleteContactRequest, CommandResponse>
    {
        private readonly IHttpService _httpService;

        public DeleteContact(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<CommandResponse> Handle(DeleteContactRequest request, CancellationToken cancellationToken)
        {
            var response =  await _httpService.SendDeleteWithResponseAsync(
                DeleteContactRequest.RouteTemplate.Replace("{ContactId}",
                request.ContactId.ToString()));
            return response;
        }
    }
}
