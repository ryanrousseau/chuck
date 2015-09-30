using System.Collections.Generic;
using System.ComponentModel;

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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
