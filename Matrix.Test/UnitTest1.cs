namespace Matrix.Test
{
    using System;

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        [Matrix]
        [MatrixParameter(0, 9)]
        [MatrixParameter(1, 2)]
        [MatrixParameter(3, 4, 5, 7)]
        [MatrixExclude(9, 1, 4)]
        public void TestMatrix(int a, int b, int c)
        {
            Assert.IsFalse(a == 9 && b == 1 && c == 4);
            Assert.IsTrue(a == 0 || a == 9);
            Assert.IsTrue(b == 1 || b == 2);
            Assert.IsTrue(c == 3 || c == 4 || c == 5 || c == 7);
        }

        [TestMethod]
        [Matrix]
        [MatrixParameter(1, 3, 5, 7)]
        [MatrixParameter(2, 4, 6, 8)]
        public void TestMatrixWithTwoParameters(int a, int b)
        {
            int result = a + b;
            Assert.IsFalse(result % 2 == 0);
        }

        [TestMethod]
        [Matrix]
        [MatrixParameter(0, 1, 3, 5, 7)]
        [MatrixParameter(0, 2, 4, 6, 8)]
        [MatrixExclude(0, 0), MatrixExclude(0, 2), MatrixExclude(0, 4)]
        [MatrixExclude(0, 4), MatrixExclude(0, 6), MatrixExclude(0, 8)]
        public void TestMatrixWithTwoParametersAndExcludes(int a, int b)
        {
            int result = a + b;
            Assert.IsTrue(result > 0);
            Assert.IsFalse(result % 2 == 0);
        }

        [TestMethod]
        [Matrix(DisplayName = "My Custom DisplayName")]
        [MatrixParameter("value1.1", "value1.2", "value1.3")]
        [MatrixParameter("value2.1", "value2.2", "value2.3")]
        public void TestDisplayName(string a, string b)
        {
            Assert.IsFalse(string.IsNullOrWhiteSpace(a) && string.IsNullOrWhiteSpace(b));
        }
    }
}
