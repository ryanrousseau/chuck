using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptCs.FluentAutomation;

namespace Chuck.Core
{
    public class TestRunner
    {
        public bool Run(Test test)
        {
            var scriptHost = new ScriptHost();

            var script = string.Format(@"var Test = Require<F14N>()  
    .Init<FluentAutomation.SeleniumWebDriver>()
    .Bootstrap(""{0}"")
    .Config(settings => {{
        // Easy access to FluentAutomation.Settings values
        settings.DefaultWaitUntilTimeout = TimeSpan.FromSeconds(1);
    }});

Test.Run(""{1}"", I => {{
{2}}});", "Chrome", test.Name, test.Script);

            scriptHost.Execute(script);

            var passed = F14N.Results[0].Passed;

            F14N.Results.Clear();

            return passed;
        }
    }
}
