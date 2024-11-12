using BlazorWeb.Components;
using BlazorWeb.Services;
using BlazorWeb.Services.HTTP;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddHttpClient();
RegisterHttpClient<IUserService, UserHttp>(builder.Services, "http://localhost:5298/api/User");
RegisterHttpClient<IPostService, PostHttp>(builder.Services, "http://localhost:5298/api/Post");
RegisterHttpClient<ICommentService, CommentHttp>(builder.Services, "http://localhost:5298/api/Comment");
RegisterHttpClient<IChannelService, ChannelHttp>(builder.Services, "http://localhost:5298/api/Channel");
var app = builder.Build();
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}
app.UseStaticFiles();
app.UseAntiforgery();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
app.Run();

void RegisterHttpClient<TService, TImplementation>(IServiceCollection services, string baseAddress)
    where TService : class
    where TImplementation : class, TService
{
    services.AddHttpClient<TService, TImplementation>(client =>
    {
        client.BaseAddress = new Uri(baseAddress);
    });
}