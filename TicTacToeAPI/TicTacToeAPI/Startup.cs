using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using TicTacToeAPI.Infrastructure.Filter;
using TicTacToeAPI.Infrastructure.Repositories;
using TicTacToeAPI.Infrastructure.Repositories.Contracts;
using TicTacToeAPI.Infrastructure.Repositories.DBContext;
using TicTacToeAPI.Service;
using TicTacToeAPI.Service.Contract;

namespace TicTacToeAPI
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;

      var loggerConfiguration = new LoggerConfiguration()
    .MinimumLevel.Is(LogEventLevel.Information)
    .Enrich.FromLogContext()
    .Enrich.WithExceptionDetails()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
    .MinimumLevel.Override("System", LogEventLevel.Warning);

      Log.Logger = loggerConfiguration.CreateLogger();
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
      services.AddDbContext<GameContext>(o => o.UseSqlite(Configuration.GetConnectionString("Database")));

      services.AddTransient<IGameRepo, GameRepo>();
      services.AddTransient<IGameService, GameService>();

      services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));

      services.AddControllers();

      // Register the Swagger generator, defining 1 or more Swagger documents
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "TicTacToe", Version = "v1" });
      });

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TicTacToe v1"));
      }

      app.UseCors(Configuration);

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
