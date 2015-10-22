using System.Collections.Generic;
using System.Linq;
using ICSharpCode.AvalonEdit.Document;
using Newtonsoft.Json;

namespace Chuck.Models
{
    /// <summary>
    ///     A model for testdetails
    /// </summary>
    public class TestDetailsModel
    {    
        /// <summary>
        ///     The name of the script we're editing
        /// </summary>
        public string ScriptName { get; set; }

        /// <summary>
        ///     The text contained within Script  [Avalon TextDocument]
        ///     -Needed for JSON write support-
        /// </summary>
        public string ScriptText { get; set; }

        /// <summary>
        ///     The current status of our test if any.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        ///     The tags associated with this test.
        /// </summary>
        public ICollection<string> Tags { get; set; }

        /// <summary>
        ///     The details of this test from a high glance.
        /// </summary>
        public string TestDetail { get { return ToString(); } }

        /// <summary>
        ///     The name of this test.
        /// </summary>
        public string TestName { get; set; }

        /// <summary>
        ///     The actual TextDocument containing our script
        /// </summary>
        [JsonIgnore]
        public TextDocument Script { get; set; }

        /// <summary>
        ///     Create a new instance of TestDetailsModel
        /// </summary>
        /// <param name="testName">Name of this test</param>
        /// <param name="scriptName">Name of the script</param>
        /// <param name="scriptText">Text that will be displayed within the script editor</param>
        /// <param name="tags">Tags associated with this test.</param>
        public TestDetailsModel(string testName, string scriptName, string scriptText, ICollection<string> tags)
        {
            ScriptName = scriptName;
            Script = new TextDocument {Text = scriptText};
            Tags = tags;
            TestName = testName;
            Status = "Idle";
        }

        /// <summary>
        ///     Necessary for saving the file with JSON.
        /// </summary>
        public void GetScriptTextFromAvalonDocument()
        {
            ScriptText = Script.Text;
        }

        /// <summary>
        ///     A human readable version of TestDetails
        /// </summary>
        /// <returns>A human readable version of TestDetails</returns>
        public override string ToString()
        {
            var tags = Tags.Aggregate(string.Empty, (current, tag) => current + string.Format("[{0}]  ", tag));
            return string.Format("{0} - Tags: {1}", TestName, tags.Trim());
        }
    }
}
