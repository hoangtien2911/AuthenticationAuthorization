using EquipmentsBusiness;
using EquipmentsRepo;
using EquipmentsRepo.IRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json;
using EquipmentsBusiness.Base;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<IAccountRepo, AccountRepo>();
builder.Services.AddScoped<IEquipmentRepo, EquipmentRepo>();
builder.Services.AddScoped<IRoomRepo, RoomRepo>();
builder.Services.AddScoped<AccountBusiness>();
builder.Services.AddScoped<EquipmentBusiness>();
builder.Services.AddScoped<RoomBusiness>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", builder =>
        builder.WithOrigins("https://localhost:7273")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithExposedHeaders("X-Custom-Header")  // Example of exposing headers
            .SetIsOriginAllowedToAllowWildcardSubdomains());
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
        // Customize the response for unauthorized requests
        options.Events = new JwtBearerEvents
        {
            OnChallenge = context =>
            {
                // Skip the default logic
                context.HandleResponse();

                // Set custom response
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";
                var result = JsonSerializer.Serialize(
                    new BusinessResult(false, "Authentication failed: Invalid JWT token."),
                    new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
                return context.Response.WriteAsync(result);
            },
            OnForbidden = context =>
            {
                // Set custom response
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                context.Response.ContentType = "application/json";
                var result = JsonSerializer.Serialize(
                    new BusinessResult(false, "Authorization failed: You do not have the required role."),
                    new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
                return context.Response.WriteAsync(result);
            }
        };
    });

builder.Services.AddSwaggerGen(opt =>
{
    opt.DescribeAllParametersInCamelCase();
    // Swagger doc
    opt.SwaggerDoc("v1", new OpenApiInfo()
    {
        Title = "Authentication-Authorization Api",
        Version = "v1"
    });

    //Security Definition JWT
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//CORS
app.UseCors("AllowSpecificOrigin");

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
