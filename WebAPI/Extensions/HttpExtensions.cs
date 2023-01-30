﻿using System.Text.Json;
using WebAPI.Helpers;

namespace WebAPI.Extensions
{
    public static class HttpExtensions
    {
        public static void AddPaginationHeader(this HttpResponse response, PaginationHeader header)
        {
            var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            if (response != null)
            {
                response.Headers.Add("Pagination", JsonSerializer.Serialize(header, jsonOptions));
                response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
            }
        }
    }
}
