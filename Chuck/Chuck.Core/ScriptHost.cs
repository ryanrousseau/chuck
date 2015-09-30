using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptCs;
using ScriptCs.Contracts;
using ScriptCs.Engine.Roslyn;
using ScriptCs.Hosting;

namespace Chuck.Core
{
    /// <summary>
    /// By default, the scriptcs.hosting inspects the bin folder for references.
    /// </summary>
    public class ScriptFileSystem : FileSystem
    {
        public override string BinFolder
        {
            get { return AppDomain.CurrentDomain.BaseDirectory; }
        }
    }

    public class ScriptHost
    {
        private ScriptServices CreateScriptServices(bool useLogging)
        {
            var console = new ScriptConsole();
            var configurator = new LoggerConfigurator(useLogging ? LogLevel.Debug : LogLevel.Info);
            configurator.Configure(console);
            var logger = configurator.GetLogger();
            var builder = new ScriptServicesBuilder(console, logger);

            builder.ScriptEngine<RoslynScriptEngine>();
            builder.FileSystem<ScriptFileSystem>();
            return builder.Build();
        }

        public void Execute(string script)
        {
            Environment.CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;

            var services = CreateScriptServices(true);

            var scriptExecutor = services.Executor;
            var scriptPackResolver = services.ScriptPackResolver;
            services.InstallationProvider.Initialize();

            // prepare NuGet dependencies, download them if required
            //var assemblyPaths = PreparePackages(services.PackageAssemblyResolver,
            //    services.PackageInstaller, PrepareAdditionalPackages(script.NuGetDependencies), script.LocalDependencies, services.Logger);

            scriptExecutor.Initialize(new string[] {}, scriptPackResolver.GetPacks());
            //scriptExecutor.ImportNamespaces(script.Namespaces);
            //scriptExecutor.AddReferences(script.LocalDependencies);

            var scriptResult = scriptExecutor.ExecuteScript(script, string.Empty);

            if (true /*script.UseLogging*/ && scriptResult != null)
                if (scriptResult.CompileExceptionInfo != null)
                    if (scriptResult.CompileExceptionInfo.SourceException != null)
                        services.Logger.Debug(scriptResult.CompileExceptionInfo.SourceException.Message);

            scriptExecutor.Terminate();
        }
    }
}
