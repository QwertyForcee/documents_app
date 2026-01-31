using DocumentsAPI.Core.Documents.Interfaces;
using DocumentsAPI.Core.Documents.Services;
using DocumentsAPI.Core.Jobs;
using DocumentsAPI.Core.Users.Interfaces;
using DocumentsAPI.Core.Users.Services;
using DocumentsAPI.Core.Users.Settings;
using DocumentsAPI.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Quartz;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// Add services to the container.

services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDev", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

services.AddScoped<IAuthService, AuthService>();
services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));

services.AddScoped<IDocumentService, DocumentService>();
services.AddScoped<ICommentService, CommentService>();
services.AddScoped<ICleanUpOldDocumentsService, CleanUpOldDocumentsService>();

services
    .AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        var config = builder.Configuration;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = config["Jwt:Issuer"],
            ValidAudience = config["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Secret"])),

            NameClaimType = JwtRegisteredClaimNames.Sub,
            RoleClaimType = ClaimTypes.Role
        };
    });

services.AddAutoMapper(typeof(Program));

builder.Services.AddQuartz(q =>
{
    var jobKey = new JobKey(nameof(CleanUpOldDocumentsJob));
    q.AddJob<CleanUpOldDocumentsJob>(options => options.WithIdentity(jobKey));

    q.AddTrigger(options => options
        .ForJob(jobKey)
        .WithIdentity($"{nameof(CleanUpOldDocumentsJob)}-trigger")
        .StartNow()
        .WithCronSchedule(builder.Configuration["CleanUpOldDocumentsJobCronExpression"], x => x.WithMisfireHandlingInstructionIgnoreMisfires())
    );
});

builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

services.AddControllers();

var app = builder.Build();
// Configure the HTTP request pipeline.

app.UseRouting();

app.UseCors("AllowAngularDev");

app.UseAuthorization();

app.MapControllers();

app.Run();
