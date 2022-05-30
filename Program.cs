using Orchard.Environment;


var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

IOrchardHost host=new DefaultOrchardHost( new RunningShellTable());

host.Initialize();

app.Map("/map1", async context=> {
    await context.Response.WriteAsync("Hello from /map1!");
});

app.Map("/map2", HandleMap2);

app.Use((context, next)=> {
        host.BeginRequest(context);
        var result = next.Invoke();
        host.EndRequest();
        return result;
    }
);

app.Run();

static void HandleMap2(IApplicationBuilder app) {
    app.Run(
        async context => { await context.Response.WriteAsync("Hello from /map2!"); });
}

