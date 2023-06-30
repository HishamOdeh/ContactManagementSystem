using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using ContactManagementSystem.Api.Helpers;
using ContactManagementSystem.Shared;
using ContactManagementSystem.Shared.Features.Contacts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RepoDb;
using Swashbuckle.AspNetCore.Annotations;

namespace ContactManagementSystem.Api.Features.Contacts
{
    public class SearchContact : BaseAsyncEndpoint
        .WithRequest<SearchContactRequest>
        .WithResponse<SearchContactRequest.Response>
    {
        private readonly IConfiguration _configuration;

        public SearchContact(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet(SearchContactRequest.RouteTemplate)]
        [SwaggerOperation(
          Summary = "Search Contacts",
          Description = "Search Contacts ",
          OperationId = "Contacts.Search",
          Tags = new[] { "ContactsEndpoint" })]
        public override async Task<ActionResult<SearchContactRequest.Response>> HandleAsync([FromQuery] SearchContactRequest request, CancellationToken cancellationToken = default)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")).EnsureOpen())
            {
                var result = await connection.ExecuteQueryAsync<ListContactDto>(CreateSql(),
                    new {
                        LastName = request.LastName,
                        FirstName = request.FirstName
                    });
                return Ok(new SearchContactRequest.Response(result.ToList()));
            }
            string CreateSql()
            {
                return $@"Select * FROM dbo.Contact Where{CreateWhereClause(request)}";
            }
            string AddAnd(string whereClause)
            {
                return string.IsNullOrEmpty(whereClause) ? string.Empty : $" AND ";
            }

            string CreateWhereClause(SearchContactRequest request)
            {
                var whereClause = string.Empty;
                if (!string.IsNullOrWhiteSpace(request.FirstName))
                {
                    whereClause = $"{whereClause} {AddAnd(whereClause)} (FirstName LIKE '%' + @FirstName + '%')";
                }
                if (!string.IsNullOrWhiteSpace(request.LastName))
                {
                    whereClause = $"{whereClause} {AddAnd(whereClause)} (LastName LIKE '%' + @LastName + '%')";
                }
                return whereClause;
            }
        }
    }
}
