using Moon.Kernel.Extension;
using NUnit.Framework;

namespace Test.Kernel.Extension
{
    [Author("Mors"), TestOf(nameof(StringExtension))]
    internal class MethodTest
    {
        [TestCase("Object 123", ExpectedResult = "Object "),
         TestCase("Object-4560", ExpectedResult = "Object-"),
         TestCase("Obje12ct4560", ExpectedResult = "Obje12ct"),
         TestCase("Objet12ct45.60", ExpectedResult = "Objet12ct45.")]
        public string RemoveTrailingNumbersPasses(string source)
        {
            return source.RemoveTrailingNumbers();
        }

        [TestCase("test1", ExpectedResult = "1b4f0e9851971998e732078544c96b36c3d01cedf7caa332359d6f1d83567014"),
         TestCase("test2", ExpectedResult = "60303ae22b998861bce3b28f33eec1be758a213c86c93c076dbe9f558c11c752")]
        public string ToSHA256Passes(string source)
        {
            return source.ToSHA256();
        }

        [TestCase("Kernel", 'e', ExpectedResult = "l"),
         TestCase("Kernel", 'r', ExpectedResult = "nel"),
         TestCase("Kernel", 'z', ExpectedResult = "Kernel")]
        public string GetSuffixTest(string str, char c)
        {
            return str.GetSuffix(c);
        }
    }
}