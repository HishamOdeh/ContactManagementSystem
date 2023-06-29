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
    public class EditContact : BaseAsyncEndpoint
        .WithRequest<EditContactRequest>
        .WithResponse<CommandResponse>
    {
        private readonly AppDbContext _context;

        public EditContact(AppDbContext context)
        {
            _context = context;
        }

        [HttpPut(EditContactRequest.RouteTemplate)]
        [SwaggerOperation(
               Summary = "Edit a contact",
               Description = "Edit a contact",
               OperationId = "Contact.Edit",
               Tags = new[] { "ContactEndpoint" })]
        public override async Task<ActionResult<CommandResponse>> HandleAsync(EditContactRequest request, CancellationToken cancellationToken = default)
        {
            if (ModelState.IsValid)
            {
                var Contact = await _context.Contacts.FindAsync(request.Contact.Id);

                if (Contact != null)
                {
                    Contact.FirstName = request.Contact.FirstName;
                    Contact.LastName = request.Contact.LastName;
                    Contact.Email = request.Contact.Email;
                    Contact.PhoneNumber = request.Contact.PhoneNumber;
                    if (request.Contact.BirthDate.HasValue)
                    {
                        Contact.BirthDate = request.Contact.BirthDate.Value;
                    }
                    else
                    {
                        Contact.BirthDate = null;
                    }
                    Contact.ModifiedDate = DateTime.Now;

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
