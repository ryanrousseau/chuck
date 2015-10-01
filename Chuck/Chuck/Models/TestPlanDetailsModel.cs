using System.Collections.Generic;

namespace Chuck.Models
{
    /// <summary>
    ///     A model for TestPlanDetails
    /// </summary>
    public class TestPlanDetailsModel
    {
        /// <summary>
        ///     The tests that are included with this testplan
        /// </summary>
        public ICollection<TestDetailsModel> IncludedTests { get; set; }

        /// <summary>
        ///     The name of this testplan.
        /// </summary>
        public string TestPlanName { get; set; }

        /// <summary>
        ///     The results of executing this testplan.
        /// </summary>
        public string TestPlanResults { get; set; }

        /// <summary>
        ///     Create a new instance of TestPlanDetailsModel
        /// </summary>
        /// <param name="testplanName">Name of this test</param>
        /// <param name="includedTests">Tests that are included with this testplan.</param>
        public TestPlanDetailsModel(string testplanName, ICollection<TestDetailsModel> includedTests)
        {
            //: TODO - change includedTests to something that will consume less disk space
            IncludedTests = includedTests;
            TestPlanName = testplanName;
        }
    }
}
