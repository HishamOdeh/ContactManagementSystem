using System.Threading;
using System.Threading.Tasks;
using ContactManagementSystem.Shared.Features.Contacts;
using ContactManagementSystem.Web.Extensions;
using ContactManagementSystem.Web.Services;
using MediatR;

namespace ContactManagementSystem.Web.Features.Contacts.SearchContact
{
    public class SearchContact : IRequestHandler<SearchContactRequest, SearchContactRequest.Response>
    {
        private readonly IHttpService _httpService;

        public SearchContact(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<SearchContactRequest.Response> Handle(SearchContactRequest request, CancellationToken cancellationToken)
        {
            var url = $"{SearchContactRequest.RouteTemplate}?{request.ToUrl()}";

            return await _httpService.SendGetAsync<SearchContactRequest.Response>(url);
        }
    }
}

