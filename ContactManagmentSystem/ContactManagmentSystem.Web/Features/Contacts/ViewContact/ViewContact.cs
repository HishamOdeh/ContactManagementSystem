using System.Threading;
using System.Threading.Tasks;
using ContactManagementSystem.Shared.Features.Contacts;
using ContactManagementSystem.Web.Services;
using MediatR;

namespace ContactManagementSystem.Web.Features.Contacts.ViewContact
{
    public class ViewContact : IRequestHandler<ViewContactRequest, ViewContactRequest.Response>
    {
        private readonly IHttpService _httpService;

        public ViewContact(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<ViewContactRequest.Response> Handle(ViewContactRequest request, CancellationToken cancellationToken)
        {
            return await _httpService.SendGetAsync<ViewContactRequest.Response>(
                ViewContactRequest.RouteTemplate.Replace("{Id}",request.Id.ToString()));
        }
    }
}

