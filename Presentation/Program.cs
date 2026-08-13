using Application.Interfaces.Security;
using Avium.API.Configurations;
using Crosscutting.Common.Core.Security;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddDbContextConfig(builder.Configuration);
builder.Services.AddSingleton<IEncryptionService>(provider => {
    var configuration = provider.GetRequiredService<IConfiguration>();

    var keyBase64 = configuration["Encryption:Key"];

    if (string.IsNullOrWhiteSpace(keyBase64))
        throw new InvalidOperationException("Chave de criptografia não configurada.");

    var key = Convert.FromBase64String(keyBase64);

    return new AesEncryption(key);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
