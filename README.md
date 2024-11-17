# MSTest Matrix Extension

The `MatrixParameterAttribute` allows you to run the same test method
with a combination of multiple different inputs.
It must appear multiple times on a test method, otherwise it will throw
an exception, since it is pretty meaningless with only parameter.
It should be combined with `TestMethodAttribute` and `MatrixAttribute`.


It generates multiple test cases beforehand,
instead of having multiple test results 
under a single test.

It also contains exclusions support.

### Explanation

Each attribute `MatrixParameter()` results in a parameter for the method.

So with three attributes, the testmethod should accept 3 parameters.

### How to use

The following test generates a total of 16 combinations:
`1 + 2`, `1 + 4`, `1 + 6`, `1 + 8`, `3 + 2` and so on...

```c#
public class MyUnitTest
{
    ...
    [TestMethod]
    [Matrix]
    [MatrixParameter(1, 3, 5, 7)]
    [MatrixParameter(2, 4, 6, 8)]
    public void TestMatrixWithTwoParameters(int a, int b)
    {
        int result = a + b;
        Assert.IsTrue(result > 0);
        Assert.IsFalse(result % 2 == 0);
    }
    ...
}
```

The following test generates a total of 15 combinations:
`0 + 1 + 3`, `0 + 1 + 4`, `0 + 1 + 5`, ... `0 + 2 + 7`, ... `9 + 2 + 3` and so on...

```c#
public class MyUnitTest
{
    ...
    [TestMethod]
    [Matrix]
    [MatrixParameter(0, 9)]
    [MatrixParameter(1, 2)]
    [MatrixParameter(3, 4, 5, 7)]
    [MatrixExclude(9, 1, 4)]
    public void TestMatrix(int a, int b, int c)
    {
        int result = a + b + c;
        Assert.IsTrue(result > 0);
    }
    ...
}
```


### How to exclude a combination

The following test will exclude the following combination:
`0 + 0`, `3 + 4` and `7 + 8`.

```c#
public class MyUnitTest
{
    ...
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
    ...
}
```
