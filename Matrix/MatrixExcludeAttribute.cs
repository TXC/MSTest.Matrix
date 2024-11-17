using System;

namespace Microsoft.VisualStudio.TestTools.UnitTesting
{
    /// <summary>
    /// Excluded Combinations
    /// </summary>
    /// <param name="values"></param>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
#nullable enable
    public class MatrixExcludeAttribute(params object?[] values)
        : Attribute, IMatrixAttribute
    {
        public object?[] Values { get; } = values;
    }
#nullable disable
}
