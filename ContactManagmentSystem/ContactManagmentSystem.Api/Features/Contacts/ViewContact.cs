using System;
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

namespace ContactManagementSystem.Api.Features.Contacts.ViewContact
{
    public class ViewContact : BaseAsyncEndpoint.WithRequest<Guid>
        .WithResponse<ViewContactRequest.Response>
    {
        private readonly IConfiguration _configuration;

        public ViewContact(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet(ViewContactRequest.RouteTemplate)]
        [SwaggerOperation(
          Summary = "View a Contact",
          Description = "View a Contact",
          OperationId = "Contact.View",
          Tags = new[] { "ContactEndpoint" })]
        public override async Task<ActionResult<ViewContactRequest.Response>> HandleAsync(Guid id, CancellationToken cancellationToken = default)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")).EnsureOpen();

            var result = await connection.ExecuteQueryAsync<ViewContactDto>(CreateSql(), new { Id = id });

            return Ok(new ViewContactRequest.Response(result.FirstOrDefault()));

            string CreateSql()
            {
                return @"SELECT
                        c.Id,
                        c.FirstName,
                        c.LastName,
                        c.Email,
                        c.PhoneNumber,
                        c.BirthDate
                   FROM dbo.Contact c
                   WHERE c.Id = @Id";
            }
        }
    }
}
