using Autofac;
using Orchard.Environment;


var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

IOrchardHost host= OrchardStarter.CreateHost(MvcSingletons);  //new DefaultOrchardHost( new RunningShellTable());

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

static void MvcSingletons(ContainerBuilder builder)
{
    /****
        builder.Register(ctx => RouteTable.Routes).SingleInstance();
        builder.Register(ctx => ModelBinders.Binders).SingleInstance();
        builder.Register(ctx => ViewEngines.Engines).SingleInstance();
    ****/
}
