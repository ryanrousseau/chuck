using System.Collections.Generic;
using ICSharpCode.AvalonEdit.Document;
using System;
using Newtonsoft.Json;

namespace Chuck.Models
{
    public class TestDetailsModel
    {    
        public string ScriptName { get; set; }
        public string ScriptText { get; set; }
        public string Status { get; set; }
        public ICollection<string> Tags { get; set; }
        public string TestDetail { get { return ToString(); } }
        public string TestName { get; set; }

        [JsonIgnore]
        public TextDocument Script { get; set; }


        public TestDetailsModel(string testName, string scriptName, string scriptText, ICollection<string> tags)
        {
            ScriptName = scriptName;
            Script = new TextDocument {Text = scriptText};
            Tags = tags;
            TestName = testName;
            Status = "Idle";
        }

        public void GetScriptTextFromAvalonDocument()
        {
            ScriptText = Script.Text;
        }

        public override string ToString()
        {
            var tags = string.Empty;
            foreach (var tag in Tags)
            {
                tags += string.Format("[{0}]  ", tag);
            }

            return string.Format("{0} - Tags: {1}", TestName, tags.Trim());
        }
    }
}
