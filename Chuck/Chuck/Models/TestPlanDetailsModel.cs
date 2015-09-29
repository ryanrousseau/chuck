using System.Collections.Generic;

namespace Chuck.Models
{
    public class TestPlanDetailsModel
    {
        public ICollection<TestDetailsModel> IncludedTests { get; set; }
        public string TestPlanName { get; set; }
        public string TestPlanResults { get; set; }

        public TestPlanDetailsModel(string testplanName, ICollection<TestDetailsModel> includedTests)
        {
            IncludedTests = includedTests;
            TestPlanName = testplanName;
        }
    }
}
