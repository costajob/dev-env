// Copyright (c) .NET Foundation. All rights reserved. 
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace aspnetcore
{
    public class PlaintextMiddleware
    {

        private static readonly byte[] _helloWorldPayload = Encoding.UTF8.GetBytes("Hello World");

        private readonly RequestDelegate _next;

        public PlaintextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            
            httpContext.Response.StatusCode = 200;
            httpContext.Response.ContentType = "text/plain";
            // HACK: Setting the Content-Length header manually avoids the cost of serializing the int to a string.
            //       This is instead of: httpContext.Response.ContentLength = _helloWorldPayload.Length;
            httpContext.Response.Headers["Content-Length"] = "11";
            return httpContext.Response.Body.WriteAsync(_helloWorldPayload, 0, _helloWorldPayload.Length);

        }
    }

    public static class PlaintextMiddlewareExtensions
    {
        public static IApplicationBuilder UsePlainText(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<PlaintextMiddleware>();
        }
    }
}