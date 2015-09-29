using System.Collections.Generic;

namespace Chuck.Models
{
    public class TestDetailsModel
    {
        public string ScriptName { get; set; }
        public string ScriptText { get; set; }
        public ICollection<string> Tags { get; set; }
        public string TestName { get; set; }

        public TestDetailsModel(string testName, string scriptName, string scriptText, ICollection<string> tags)
        {
            ScriptName = scriptName;
            ScriptText = scriptText;
            Tags = tags;
            TestName = testName;
        }
    }
}
