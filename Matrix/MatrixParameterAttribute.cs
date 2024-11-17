using System;
namespace Microsoft.VisualStudio.TestTools.UnitTesting
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
#nullable enable
    public class MatrixParameterAttribute(params object?[] values)
        : Attribute, IMatrixAttribute
    {
        public object?[] Values { get; } = values;
    }
#nullable disable
}
