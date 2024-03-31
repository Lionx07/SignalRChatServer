using SignalRChatServer;


// Program file...
// teste
var builder = WebApplication.CreateBuilder(args);

// Configure services
builder.Services.AddSignalR();

var app = builder.Build();

// Configure endpoints
app.MapHub<Chat>("/chat");

app.Run();
