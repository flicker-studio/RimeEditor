using Moon.Kernel.Extension;
using NUnit.Framework;

namespace Test.Kernel.Extension
{
    [Author("Mors"), TestOf(nameof(Flag))]
    internal class FlagTest
    {
        internal static Flag[] FlagDataSource =
        {
            new() { SetFlag = true },
            new() { SetFlag = false }
        };

        [TestCase(true)]
        [TestCase(false)]
        public void SetFlagTest(bool source)
        {
            var flag = new Flag { SetFlag = source };
            Assert.AreEqual(source, flag.GetFlag);
        }

        [TestCaseSource(nameof(FlagDataSource))]
        public void GetFlagTest(Flag flag)
        {
            _ = flag.GetFlag;
            Assert.IsFalse(flag.GetFlag);
        }
    }
}