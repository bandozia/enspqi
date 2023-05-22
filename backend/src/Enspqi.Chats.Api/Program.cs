using Enspqi.Chats.Api;
using Enspqi.Chats.Api.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSignalR();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{    
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(conf => conf.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowCredentials());

app.MapGroup("/general/rooms").MapGeneralRooms();

app.MapHub<GeneralHub>("/chat/general");

app.Run();


//https://learn.microsoft.com/en-us/aspnet/core/performance/caching/distributed?view=aspnetcore-7.0
//https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-7.0

//https://learn.microsoft.com/en-us/aspnet/core/signalr/hubs?view=aspnetcore-7.0
//https://learn.microsoft.com/en-us/aspnet/core/signalr/hubcontext?view=aspnetcore-7.0
//https://learn.microsoft.com/en-us/aspnet/signalr/overview/guide-to-the-api/mapping-users-to-connections
//https://learn.microsoft.com/en-us/aspnet/signalr/overview/guide-to-the-api/working-with-groups?source=recommendations