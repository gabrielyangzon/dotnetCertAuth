using Microsoft.AspNetCore.Authentication.Certificate;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddAuthentication(CertificateAuthenticationDefaults.AuthenticationScheme)
    .AddCertificate("CertificateAuth" , options =>
    {
        options.Events = new CertificateAuthenticationEvents
        {
            OnAuthenticationFailed = context =>
            {

                return Task.CompletedTask;
            }


        };
    });


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CertificateAuth", policy => policy.AddAuthenticationSchemes(CertificateAuthenticationDefaults.AuthenticationScheme));

});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
