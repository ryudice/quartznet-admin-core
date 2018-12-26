using FakeItEasy;
using Quartz;

namespace QuartzApiCore.Tests
{
    public class TriggersProviderTests
    {
        public void should_get_triggers_from_quartz_scheduler()
        {
            var scheduler = A.Fake<IScheduler>();
            
            
        }
    }
}