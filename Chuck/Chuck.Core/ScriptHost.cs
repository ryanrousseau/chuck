using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAutomation;
using ScriptCs;
using ScriptCs.Contracts;
using ScriptCs.Engine.Roslyn;
using ScriptCs.FluentAutomation;
using ScriptCs.Hosting;

namespace Chuck.Core
{
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

            return builder.Build();
        }

        public void Execute(string script)
        {
            var services = CreateScriptServices(true);

            var scriptExecutor = services.Executor;
            scriptExecutor.AddReference<BaseFluentTest>();
            scriptExecutor.AddReference<FluentAutomationPack>();
            scriptExecutor.AddReference<SeleniumWebDriver>();

            scriptExecutor.ImportNamespaces(new string[] { "FluentAutomation", "ScriptCs.FluentAutomation" });
            scriptExecutor.Initialize(new string[] {}, new IScriptPack[] { new FluentAutomationPack() });

            var scriptResult = scriptExecutor.ExecuteScript(script, string.Empty);

            if (scriptResult != null)
            {
                if (scriptResult.CompileExceptionInfo != null)
                {
                    if (scriptResult.CompileExceptionInfo.SourceException != null)
                    {
                        services.Logger.Debug(scriptResult.CompileExceptionInfo.SourceException.Message);
                    }
                }
            }

            F14N.Results.Clear();
            scriptExecutor.Terminate();
        }
    }
}
