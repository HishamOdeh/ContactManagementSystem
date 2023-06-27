using System.Threading;
using System.Threading.Tasks;
using ContactManagementSystem.Shared.Features.Contacts;
using ContactManagementSystem.Web.Services;
using MediatR;

namespace ContactManagementSystem.Web.Features.Contacts.ListContact
{
    public class ListContact : IRequestHandler<ListContactRequest, ListContactRequest.Response>
    {
        private readonly IHttpService _httpService;

        public ListContact(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<ListContactRequest.Response> Handle(ListContactRequest request, CancellationToken cancellationToken)
        {
            return await _httpService.SendGetAsync<ListContactRequest.Response>(ListContactRequest.RouteTemplate);
        }
    }
}

