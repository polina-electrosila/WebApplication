
using WebApplication.Data;
using WebApplication.Data.Interfaces;
using WebApplication.Manager;
using WebApplication.Manager.Interfaces;

namespace WebApplication;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IDictionaryContext, DictionaryContext>(); // scoped?
        services.AddSingleton<ICommandManager, CommandManager>();
        
        services.AddControllers();
        services.AddSwaggerGen();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApplication API v1"));
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }

}