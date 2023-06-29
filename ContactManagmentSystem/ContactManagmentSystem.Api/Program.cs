using ContactManagementSystem.Api.Features.Contacts;
using ContactManagementSystem.Api.Services;
using ContactManagementSystem.Domain;
using ContactManagementSystem.Domain.Interfaces;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//            .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection(builder.Configuration["AuthenticationType"]));

//builder.Services.Configure<JwtBearerOptions>(
// JwtBearerDefaults.AuthenticationScheme, options =>
// {
//     options.TokenValidationParameters.NameClaimType = "name";
// });

builder.Services.AddDbContext<AppDbContext>(options =>
   options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddTransient<IDateTime, DateTimeService>();

RepoDb.Converter.EnumDefaultDatabaseType = System.Data.DbType.Int32;

RepoDb.SqlServerBootstrap.Initialize();

builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages().AddFluentValidation(fv =>
{
    fv.ImplicitlyValidateChildProperties = true;
    fv.RegisterValidatorsFromAssemblyContaining<ContactsServerValidator>();
    fv.RegisterValidatorsFromAssemblyContaining<Program>();

});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Contact API", Version = "v1" });
    c.EnableAnnotations();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

//app.UseAuthentication();
//app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
