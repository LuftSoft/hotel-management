using hotel_management_api.Database;
using hotel_management_api.Database.Model;
using hotel_management_api.Extension.DependencyInjections;
using hotel_management_api.Extension.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("hotel_management"));
});
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
// Adding Jwt Bearer
.AddJwtBearer(options =>
 {
     options.SaveToken = true;
     options.RequireHttpsMetadata = false;
     options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
     {
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidAudience = builder.Configuration["JWTConfig:ValidAudience"],
         ValidIssuer = builder.Configuration["JWTConfig:ValidIssuer"],
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTConfig:AccessSecret"]))
     };
 });

builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("owner", p =>
    {
        p.RequireRole("owner");
        p.RequireClaim("AccessToken","true");
    });
    opt.AddPolicy("admin", p =>
    {
        p.RequireRole("admin");
        p.RequireClaim("AccessToken", "true");
    });
    opt.AddPolicy("user", p =>
    {
        p.RequireRole("user");
        p.RequireClaim("AccessToken", "true");
    });
});

builder.Services.AddApiVersioning(opt =>
{
    opt.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.ReportApiVersions = true;
    opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                                                    new HeaderApiVersionReader("x-api-version"),
                                                    new MediaTypeApiVersionReader("x-api-version"));
});




//add dependency
builder.Services
    .ServiceDependencyInjection()
    .LocationInteractorDependency()
    .RepositoryDependencyInjection()
    .UtilServiceDependencyInjection()
    .RoomInteractorDependencyInjection()
    .UserInteractorDependencyInjection()
    .HotelInteractorDependencyInjection()
    .BookingInteractorDependencyInjection()
    .CommentInteractorDependencyInjection()
    .RoomGalleryInteractorDependencyInjection()
    .HotelCategoryInteractorDependencyInjection();
builder.Services.AddCors(builder =>
{
    builder.AddPolicy(
        "CrossOrigin",
        opt => opt.WithOrigins("*").AllowAnyMethod().AllowAnyHeader()
    );
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.AddSecurityDefinition("Bearer",
        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
            In = Microsoft.OpenApi.Models.ParameterLocation.Header,
            Description = "Please enter into field the word 'Bearer' following by space and JWT",
            Scheme = "Bearer"
        });
    opt.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
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

var app = builder.Build();


// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//    app.UseHsts();
//}
app.UseStaticFiles();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHsts();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseCors("CrossOrigin");
app.MapControllers();
app.Run();
