
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Logging;
using Microsoft.OpenApi.Models;
using Project.Entities;
using Project.Interface;
using Project.Middleware;
using Project.Repository;
using Project.Service.User;
using System.Net;

var MyAllowSpecificOrigins = "ClientPermission";
var builder = WebApplication.CreateBuilder(args);

ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
IdentityModelEventSource.ShowPII = true;

builder.Services.AddControllers();

// Configure Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your token in the text input below.",
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            Array.Empty<string>()
        }
    });
});

// CORS configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        builder =>
        {
            builder.WithOrigins(
                "http://localhost:3000", "http://127.0.0.1:3000", "http://localhost:7112",
                "https://hcmue.fm.edu.vn", "http://hcmue.fm.edu.vn",
                "https://internal-api.fm.edu.vn", "http://internal-api.fm.edu.vn", "http://localhost:3002",
                "https://qcr-hcmue.fm.edu.vn", "https://hcmue.fm.edu.vn/internal/qcr")
            .SetIsOriginAllowedToAllowWildcardSubdomains()
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
        });
});

// Authentication and authorization configuration
var authority = builder.Configuration.GetValue<string>("Application:IdentityServerAuthOptions:Authority");
var apiName = builder.Configuration.GetValue<string>("Application:IdentityServerAuthOptions:ApiName");

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("internal", policy => policy.RequireClaim("scope", "web_api.internal"));
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Authority = authority;
    options.Audience = apiName;
    options.RequireHttpsMetadata = false;
});

// Load additional configuration
builder.Configuration.AddJsonFile("./configurationkeys.json", optional: true, reloadOnChange: false);
;
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 52428800; // 50MB
});
builder.Services.AddDbContext<HcmUeQTTB_DevContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("MyConnectionString")));
builder.Services.AddScoped<IFloorRepository, FloorRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IBlockRepository, BlockRepository>();
builder.Services.AddScoped<IShiftRepository, ShiftRepository>();
builder.Services.AddScoped<ICriteriaRepository, CriteriaRepository>();
builder.Services.AddScoped<ICleaningFormRepository, CleaningFormRepository>();
builder.Services.AddScoped<ICleaningReportRepository, CleaningReportRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<ITagsPerCriteriaRepository, TagsPerCriteriaRepository>();
builder.Services.AddScoped<ICriteriasPerFormRepository, CriteriasPerFormRepository>();
builder.Services.AddScoped<ICriteriaReportRepository, CriteriaReportRepository>();
builder.Services.AddScoped<IRoomCategoryRepository, RoomCategoryRepository>();
builder.Services.AddScoped<IGroupRoomRepository, GroupRoomRepository>();
builder.Services.AddScoped<IScheduleRepository,ScheduleRepository>();
builder.Services.AddScoped<IUserPerTagRepository, UserPerTagRepository>();
builder.Services.AddScoped<IResponsibleGroupRepository, ResponsibleGroupRepository>();
builder.Services.AddScoped<IQRScannerRepository,QRScannerRepository>();
builder.Services.AddScoped<IChartRepository, ChartRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IApplicationRepository, ApplicationRepository>();




// Add Static Files Middleware
builder.Services.AddDirectoryBrowser();

var app = builder.Build();

// Enable serving static files from wwwroot/uploads
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.WebRootPath, "uploads")),
    RequestPath = "/uploads"
});


// Enable directory browsing (optional, for debugging purposes)
app.UseDirectoryBrowser(new DirectoryBrowserOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.WebRootPath, "uploads")),
    RequestPath = "/uploads"
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);
app.UseAuthentication(); // Ensure authentication is used before authorization
app.UseAuthorization();

app.MapControllers();

// Optional endpoint to show server status
app.MapGet("/", async context =>
{
    context.Response.ContentType = "text/html";
    await context.Response.WriteAsync("Internal Server is ready!");
});

app.Run();

