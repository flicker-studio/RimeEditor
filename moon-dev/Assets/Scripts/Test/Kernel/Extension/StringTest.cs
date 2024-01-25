using Moon.Kernel.Extension;
using NUnit.Framework;

namespace Test.Kernel.Extension
{
    [Author("Mors"), TestOf(nameof(String))]
    internal class MethodTest
    {
        [TestCase("Object 123", ExpectedResult = "Object ")]
        [TestCase("Object-4560", ExpectedResult = "Object-")]
        [TestCase("Obje12ct4560", ExpectedResult = "Obje12ct")]
        [TestCase("Objet12ct45.60", ExpectedResult = "Objet12ct45.")]
        public string RemoveTrailingNumbersPasses(string source)
        {
            return source.RemoveTrailingNumbers();
        }
    }
}