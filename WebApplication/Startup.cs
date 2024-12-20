
using WebVocabulary.Data;
using WebVocabulary.Data.Interfaces;
using WebVocabulary.Manager;
using WebVocabulary.Manager.Interfaces;

namespace WebVocabulary;

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
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebVocabulary API v1"));
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }

}