using Orchard.Environment.Configuration;

namespace Orchard.Environment;

public class DefaultOrchardHost : IOrchardHost {
    private readonly IRunningShellTable _runningShellTable;

    public DefaultOrchardHost(IRunningShellTable runningShellTable) {
        _runningShellTable = runningShellTable;
    }
    public void Initialize() {
        //throw new NotImplementedException();
    }

    public void ReloadExtensions() {
        throw new NotImplementedException();
    }

    public async Task BeginRequest(HttpContext context) {
        
            //await context.Response.WriteAsync("Hello from Orchard 1.5!");
            //await context.Response.WriteAsync("2nd line, Hello from Orchard 1.5!");

        /****
        BlockRequestsDuringSetup();


        ****/
        Action ensureInitialized = () => {
            // Ensure all shell contexts are loaded, or need to be reloaded if
            // extensions have changed

            /****
            MonitorExtensions();
            BuildCurrent();
            ****/
        };
        
        ShellSettings currentShellSettings = null!;
        

        //var httpContext = _httpContextAccessor.Current();
        var httpContext = context;

        if (httpContext != null)
        {
            currentShellSettings = _runningShellTable.Match(httpContext);
        }

        if (currentShellSettings == null)
        {
            ensureInitialized();
        }
        else
        {
            //**** _shellActivationLock.RunWithReadLock(currentShellSettings.Name, () =>
            {
                ensureInitialized();
            }
                //**** );
        }

        //// StartUpdatedShells can cause a writer shell activation lock so it should run outside the reader lock.
        //StartUpdatedShells();
    }

    public void EndRequest() {
        //throw new NotImplementedException();
    }
}
