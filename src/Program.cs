using FleetManager.Filters;
using FleetManager.Services;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers(options =>
{
    options.Filters.Add<HttpResponseExceptionFilter>();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddSingleton<IKubernetesClientService, KubernetesClientService>();
builder.Services.AddSingleton<IFleetService, FleetService>();
builder.Services.AddSingleton<IGameServerService, GameServerService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpLogging();
app.UseAuthorization();
app.MapControllers();
app.Run();
