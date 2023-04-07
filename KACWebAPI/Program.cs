
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using WebAPI_CQRS.Domain.Infrastructure.Authorization;

using Microsoft.OpenApi.Models;
using System.Text;
using KVMWebAPI.Infrastructure.System;
using Newtonsoft.Json;
using KACGatewayContextLibrary.Domain.Entity;
using KQueue.RabbitMQ;
using KVMWebAPI.Controllers;
using KACCloudContextLibrary.Domain;
using KACWebAPI.Business.Abstract;
using KACWebAPI.Business.Concrete;
using KACWebAPI.Domain.Query.Queries.Utente.Concrete;
using KACWebAPI.Domain.Query.Queries.Ruolo.Concrete;

var builder = WebApplication.CreateBuilder(args);

var statusSystem = StatusSystem.GetInstance();
var directory = AppDomain.CurrentDomain.BaseDirectory;
var file = Path.Combine(directory, "appsettings.json");
int webAPiPort = 5000;
bool useKestrel = true;
var connectionString = "";
using (StreamReader r = new StreamReader(file))
{
    string json = r.ReadToEnd();
    SettingFile setting = JsonConvert.DeserializeObject<SettingFile>(json);
    statusSystem.SetSettings(setting.Settings);
    webAPiPort = setting.Settings.WebAPIPort;
    useKestrel = setting.Settings.UseKestrel;
    connectionString = setting.Settings.ConnectionString;
}

//aggiunta per gestione porta
if (useKestrel)
{
    builder.WebHost.ConfigureKestrel(serverOptions =>
    {
        serverOptions.ListenAnyIP(webAPiPort, listenOptions =>
        {
            //listenOptions.UseHttps(args[0], args.Length > 1 ? args[1] : "");
        });
        serverOptions.ListenAnyIP(80); // NOTE: optionally listen on port 80, too
    });
}


// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IUtenteBusiness, UtenteBusiness>();
builder.Services.AddScoped<IUtenteSerializer, UtenteSerializer>();

builder.Services.AddScoped<IRuoloBusiness, RuoloBusiness>();
builder.Services.AddScoped<IRuoloSerializer, RuoloSerializer>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "KVM WebAPI", Version = "v1" });
    c.CustomSchemaIds(type => type.ToString());

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description =
            "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        
                        ValidIssuer = "Fiver.Security.Bearer",
                        ValidAudience = "Fiver.Security.Bearer",
                        IssuerSigningKey = JwtSecurityKey.Create("grgpla74a26g284d")
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            Console.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
                            return Task.CompletedTask;
                        },
                        OnTokenValidated = context =>
                        {
                            Console.WriteLine("OnTokenValidated: " + context.SecurityToken);
                            return Task.CompletedTask;
                        }
                };
                });

builder.Services.AddDbContext<KACCloudContext>(options => options.UseNpgsql(statusSystem.Settings.ConnectionString,
                o => o.MigrationsAssembly(typeof(WebControllerBase).Assembly.GetName().Name)), ServiceLifetime.Transient);

var app = builder.Build();

app.UseDeveloperExceptionPage();

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.UseAuthorization();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "KAC WebAPI v1"));

//todo per test invio modifiche anagrafica
var anagrafica = new Anagrafica() { ID = Guid.NewGuid(), Cognome = "Gregorat", Nome = "Paolo", CreationDate = DateTime.Now, LastEditDate = DateTime.Now };
string payLoad = JsonConvert.SerializeObject(anagrafica);
//var communicator = MQTTCommunicator.GetInstrance("localhost", 707);
//var res = communicator.Send("kronotech/topic/json", payLoad);

//while (true)
//{
//    var communicator = RabbitMQCommunicator.GetInstrance(statusSystem.Settings.HostNameQueueCommunicator, statusSystem.Settings.PortQueueCommunicator);
//    communicator.SendEventToMessageQueueAsync("KAGGateway", "KAGUpdateAnagrafica", payLoad);
//    System.Threading.Thread.Sleep(2000);
//}

//communicator.SendEventToMessageQueueAsync("KAGGateway", "prova", payLoad);

app.Run();

