using System.Threading;
using System.Threading.Tasks;
using ContactManagementSystem.Shared.Features.Contacts;
using ContactManagementSystem.Web.Services;
using MediatR;

namespace ContactManagementSystem.Web.Features.Contacts.EditContact
{
    public class GetContact : IRequestHandler<GetContactRequest, GetContactRequest.Response>
    {
        private readonly IHttpService _httpService;

        public GetContact(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<GetContactRequest.Response> Handle(GetContactRequest request, CancellationToken cancellationToken)
        {
            return await _httpService.SendGetAsync<GetContactRequest.Response>(GetContactRequest.RouteTemplate.Replace("{id}",request.Id.ToString()));
        }
    }
}

