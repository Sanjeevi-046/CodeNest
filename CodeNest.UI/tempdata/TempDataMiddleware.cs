// ***********************************************************************************************
//
//  (c) Copyright 2024, Computer Task Group, Inc. (CTG)
//
//  This software is licensed under a commercial license agreement. For the full copyright and
//  license information, please contact CTG for more information.
//
//  Description: CodeNest .
//
// ***********************************************************************************************

namespace CodeNest.UI.tempdata
{
    public class TempDataMiddleware
    {
        private readonly RequestDelegate _next;

        public TempDataMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Call the next delegate/middleware in the pipeline
            await _next(context);

            // Clear TempData after the response is sent
            if (context.Response.StatusCode == 200)
            {
                context.Items["TempDataClear"] = true;
            }
        }
    }
}
