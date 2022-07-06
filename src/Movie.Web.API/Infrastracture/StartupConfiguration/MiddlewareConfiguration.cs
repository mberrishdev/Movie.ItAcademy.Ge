using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Movie.Web.API.Infrastracture.Middlewares;

namespace Movie.Web.Api.Infrastracture.StartupConfiguration
{
    public static class MiddlewareConfiguration
    {
        public static WebApplication ConfigureMiddleware(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI();

            //app.UseServerOptionsLoaderMiddleware();
            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            return app;
        }
    }
}
