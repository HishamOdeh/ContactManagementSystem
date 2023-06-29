using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using ContactManagementSystem.Api.Extensions;
using ContactManagementSystem.Domain;
using ContactManagementSystem.Shared;
using ContactManagementSystem.Shared.Features.Contacts;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ContactManagementSystem.Api.Features.Contacts
{
    public class DeleteContact : BaseAsyncEndpoint
        .WithRequest<DeleteContactRequest>
        .WithResponse<CommandResponse>
    {
        private readonly AppDbContext _context;

        public DeleteContact(AppDbContext context)
        {
            _context = context;
        }

        [HttpDelete(DeleteContactRequest.RouteTemplate)]
        [SwaggerOperation(
               Summary = "Delete a Contact",
               Description = "Delete a contact",
               OperationId = "Delete a Contact",
               Tags = new[] { "ContactEndpoint" })]
        public override async Task<ActionResult<CommandResponse>> HandleAsync([FromRoute] DeleteContactRequest request, CancellationToken cancellationToken = default)
        {
            if (ModelState.IsValid)
            {
                var Contact = await _context.Contacts.FindAsync(request.ContactId);

                if(Contact != null)
                {
                    _context.Contacts.Remove(Contact);

                    await _context.SaveChangesAsync(cancellationToken);
                }

                return Ok(new CommandResponse().Success());
            }
            else
            {
                return BadRequest(new CommandResponse().Errors(ModelState));
            }
        }
    }
}
