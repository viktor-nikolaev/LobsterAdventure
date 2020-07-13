using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Voyager;
using LobsterAdventure.AppServices;
using LobsterAdventure.Domain;
using LobsterAdventure.Infrastructure;
using LobsterAdventure.WebApp.GQL;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;

namespace LobsterAdventure.WebApp
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddControllersWithViews();
      // In production, the Angular files will be served from this directory
      services.AddSpaStaticFiles(configuration => configuration.RootPath = "ClientApp/dist");

      // Add GraphQL Services
      services.AddGraphQL(
        SchemaBuilder.New()
          .AddQueryType<Query>()
          .AddMutationType<Mutation>()
          .ModifyOptions(o => o.RemoveUnreachableTypes = true)
      );

      services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(Configuration["RedisUrl"]));
      services.AddScoped(_ => _.GetRequiredService<IConnectionMultiplexer>().GetDatabase());

      services.AddScoped<IAdventureRepository, AdventureRepository>();
      services.AddScoped<IUserAdventureSessionRepository, UserAdventureSessionRepository>();

      services.AddMediatR(typeof(GetAdventuresQuery));
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Error");
      }

      app.UseStaticFiles();
      if (!env.IsDevelopment())
      {
        app.UseSpaStaticFiles();
      }

      app.UseRouting();

      app.UseGraphQL("/graphql")
        .UsePlayground("/graphql")
        .UseVoyager("/graphql");

      app.UseSpa(spa =>
      {
        // To learn more about options for serving an Angular SPA from ASP.NET Core,
        // see https://go.microsoft.com/fwlink/?linkid=864501

        spa.Options.SourcePath = "ClientApp";

        if (env.IsDevelopment())
        {
          spa.UseAngularCliServer("start");
        }
      });
    }
  }
}