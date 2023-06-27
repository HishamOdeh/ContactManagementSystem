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

namespace ContactManagementSystem.Api.Features.Contacts
{
    public class GetContact : BaseAsyncEndpoint.WithRequest<Guid>
        .WithResponse<GetContactRequest.Response>
    {
        private readonly IConfiguration _configuration;

        public GetContact(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet(GetContactRequest.RouteTemplate)]
        [SwaggerOperation(
          Summary = "Get Contact",
          Description = "Get Contact",
          OperationId = "Contact.Get",
          Tags = new[] { "ContactEndpoint" })]
        public override async Task<ActionResult<GetContactRequest.Response>> HandleAsync(Guid id, CancellationToken cancellationToken = default)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")).EnsureOpen();

            var result = await connection.ExecuteQueryAsync<ContactFormModel>(CreateSql(), new { Id = id });

            return Ok(new GetContactRequest.Response(result.FirstOrDefault()));


            string CreateSql()
            {
                return @"SELECT
                            c.Id,
                            c.LastName,
                            c.FirstName,
                            c.PhoneNumber,
                            c.BirthDate,
                            c.Email
                        FROM dbo.Contact c 
                        WHERE c.Id = @Id;";
            }
        }
    }
}
