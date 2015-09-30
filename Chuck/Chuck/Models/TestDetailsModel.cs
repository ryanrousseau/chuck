using System.Collections.Generic;
using ICSharpCode.AvalonEdit.Document;

namespace Chuck.Models
{
    public class TestDetailsModel
    {
        public string ScriptName { get; set; }
        public TextDocument Script { get; set; }
        public string Status { get; set; }
        public ICollection<string> Tags { get; set; }
        public string TestName { get; set; }

        public TestDetailsModel(string testName, string scriptName, string scriptText, ICollection<string> tags)
        {
            ScriptName = scriptName;
            Script = new TextDocument {Text = scriptText};
            Tags = tags;
            TestName = testName;
            Status = "Idle";
        }
    }
}
