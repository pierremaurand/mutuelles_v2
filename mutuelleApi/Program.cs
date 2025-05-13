using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using mutuelleApi.data;
using mutuelleApi.helpers;
using mutuelleApi.hubConfig;
using mutuelleApi.interfaces;
using mutuelleApi.models;

var builder = WebApplication.CreateBuilder(args);

var secretKey = builder.Configuration.GetSection("AppSettings:Key").Value;
if (string.IsNullOrEmpty(secretKey))
{
    throw new InvalidOperationException("The secret key cannot be null or empty. Please check the configuration.");
}
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers().AddNewtonsoftJson();;
builder.Services.AddOpenApi();
builder.Services.AddSignalR();

builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default"))
.UseSeeding((context, _) =>
        {
             var testUser = context.Set<Utilisateur>().FirstOrDefault(u => u.Login == "admin");
            if (testUser == null)
            {
                byte[] passwordHash, passwordKey;

                using(var hmac = new HMACSHA512()){
                    passwordKey = hmac.Key;
                    passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("mutuelle"));
                }
                context.Set<Utilisateur>().Add(new Utilisateur
                {
                    Login = "admin",
                    MotDePasse = passwordHash,
                    ClesMotDePasse = passwordKey,
                    Role = mutuelleApi.enums.Role.Administrateur,
                    ModifiePar = 0,
                    ModifieLe = DateTime.Now,
                });
                context.SaveChanges();
            }
        })
.UseAsyncSeeding(async (context, _, cancellationToken) =>
        {
            var testUser = await context.Set<Utilisateur>().FirstOrDefaultAsync(u => u.Login == "admin", cancellationToken);
            if (testUser == null)
            {
                byte[] passwordHash, passwordKey;

                using(var hmac = new HMACSHA512()){
                    passwordKey = hmac.Key;
                    passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("mutuelle"));
                }
                context.Set<Utilisateur>().Add(new Utilisateur
                {
                    Login = "admin",
                    MotDePasse = passwordHash,
                    ClesMotDePasse = passwordKey,
                    Role = mutuelleApi.enums.Role.Administrateur,
                    ModifiePar = 0,
                    ModifieLe = DateTime.Now,
                });
                await context.SaveChangesAsync(cancellationToken);
            }
        }));
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = false,
            ValidateAudience = false,
            IssuerSigningKey = key
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUi(options =>
    {
        options.DocumentPath = "/openapi/v1.json";
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapControllers();
app.MapHub<SignalrServer>("/signalrServer");

app.Run();
