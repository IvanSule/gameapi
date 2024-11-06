using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using RPSSL.WebApi.E2ETests.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace RPSSL.WebApi.E2ETests.Extensions
{
    internal static class HttpResponseMessageExtensions
    {
        internal static async Task<ValidationProblemDetails?> GetValidationProblemDetails(this HttpResponseMessage message)
        {
            if (message.IsSuccessStatusCode) { throw new InvalidOperationException("Expected error response."); };

            ValidationProblemDetails? problemDetails = await message.Content.ReadFromJsonAsync<ValidationProblemDetails>();

            Ensure.NotNull(problemDetails, nameof(problemDetails));

            return problemDetails;             
        }

        internal static async Task<T?> GetOkResult<T>(this HttpResponseMessage message)
        {
            if (!message.IsSuccessStatusCode) { throw new InvalidOperationException("Expected success response."); };

            T? value = await message.Content.ReadFromJsonAsync<T>();

            return value;
        }
    }
}
