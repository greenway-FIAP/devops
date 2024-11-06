using ApiGreenway.Data;
using ApiGreenway.Repository;
using ApiGreenway.Repository.Interface;
using ApiGreenway.Services.Authentication;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Configuration;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<dbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleConnection"))
           .LogTo(Console.WriteLine, LogLevel.Information)
);

FirebaseApp.Create(new AppOptions
{
    Credential = GoogleCredential.FromFile("greenway-firebase.json")
});

builder.Services.AddHttpClient<IAuthService, AuthService>((sp, httpClient) =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    httpClient.BaseAddress = new Uri(configuration["Authentication:TokenUri"]!);
});

builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<IBadgeRepository, BadgeRepository>();
builder.Services.AddScoped<IBadgeLevelRepository, BadgeLevelRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<ICompanyRepresentativeRepository, CompanyRepresentativeRepository>();
builder.Services.AddScoped<IImprovementMeasurementRepository, ImprovementMeasurementRepository>();
builder.Services.AddScoped<IMeasurementProcessStepRepository, MeasurementProcessStepRepository>();
builder.Services.AddScoped<IMeasurementRepository, MeasurementRepository>();
builder.Services.AddScoped<IMeasurementTypeRepository, MeasurementTypeRepository>();
builder.Services.AddScoped<IProcessBadgeRepository, ProcessBadgeRepository>();
builder.Services.AddScoped<IProcessRepository, ProcessRepository>();
builder.Services.AddScoped<IProcessResourceRepository, ProcessResourceRepository>();
builder.Services.AddScoped<IProcessStepRepository, ProcessStepRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
builder.Services.AddScoped<IResourceRepository, ResourceRepository>();
builder.Services.AddScoped<IResourceTypeRepository, ResourceTypeRepository>();
builder.Services.AddScoped<ISectorRepository, SectorRepository>();
builder.Services.AddScoped<IStepRepository, StepRepository>();
builder.Services.AddScoped<ISustainableGoalRepository, SustainableGoalRepository>();
builder.Services.AddScoped<ISustainableImprovementActionsRepository, SustainableImprovementActionsRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserTypeRepository, UserTypeRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Api da Plataforma Greenway de Sustentabilidade",
        Version = "v1.0.0",
        Description = "Esta api tem como funcionalidade ser um BackEnd para que seja utilizado por outras aplicações web e etc.",
        Contact = new OpenApiContact
        {
            Name = "Greenway",
            Email = "technosfiap@gmail.com",
            Url = new Uri("https://www.greenway.com.br")
        }
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

app.UseDeveloperExceptionPage();
app.UseSwagger();
app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "GreenwayAPI"));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
