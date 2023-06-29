using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using ContactManagementSystem.Api.Extensions;
using ContactManagementSystem.Domain;
using ContactManagementSystem.Shared;
using ContactManagementSystem.Shared.Features.Contacts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using ContactManagementSystem.Domain.Models;
using MudBlazor;

namespace ContactManagementSystem.Api.Features.Contacts
{
    public class AddContact : BaseAsyncEndpoint
        .WithRequest<AddContactRequest>
        .WithResponse<CommandResponse>
    {
        private readonly AppDbContext _context;

        public AddContact(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost(AddContactRequest.RouteTemplate)]
        [SwaggerOperation(
               Summary = "Add a contact",
               Description = "Add a contact",
               OperationId = "Contact.Add",
               Tags = new[] { "ContactEndpoint" })]
        public override async Task<ActionResult<CommandResponse>> HandleAsync(AddContactRequest request, CancellationToken cancellationToken = default)
        {
            try{
                if (ModelState.IsValid)
                {
                    var Contact = new Contact()
                    {
                        FirstName = request.Contact.FirstName,
                        LastName = request.Contact.LastName,
                        PhoneNumber = request.Contact.PhoneNumber,
                        Email = request.Contact.Email,
                        BirthDate = request.Contact.BirthDate,
                        CreatedDate = DateTime.Now
                    };

                    await _context.Contacts.AddAsync(Contact);
                    await _context.SaveChangesAsync(cancellationToken);



                    return Ok(new CommandResponse().Success());
                }
                else
                {
                    return BadRequest(new CommandResponse().Errors(ModelState));
                }
            } 
            catch (Exception ex)
            { 
                throw ex;
            }
           
        }
    }
}
