using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using ContactManagementSystem.Shared.Features.Contacts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RepoDb;
using Swashbuckle.AspNetCore.Annotations;

namespace ContactManagementSystem.Api.Features.Contacts
{
    public class ListContact : BaseAsyncEndpoint.WithoutRequest
        .WithResponse<ListContactRequest.Response>
    {
        private readonly IConfiguration _configuration;

        public ListContact(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet(ListContactRequest.RouteTemplate)]
        [SwaggerOperation(
          Summary = "List Contact",
          Description = "List Contact",
          OperationId = "Contact.List",
          Tags = new[] { "ContactEndpoint" })]
        public override async Task<ActionResult<ListContactRequest.Response>> HandleAsync(CancellationToken cancellationToken = default)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")).EnsureOpen();

            var result = await connection.ExecuteQueryAsync<ListContactDto>(CreateSql());

            return Ok(new ListContactRequest.Response(result.ToList()));


            string CreateSql()
            {
                return @"SELECT
                            c.Id,
                            c.LastName,
                            c.FirstName,
                            c.PhoneNumber,
                            c.Email,
                            c.BirthDate
                        From dbo.Contact c";
            }
        }
    }
}
