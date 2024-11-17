using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Microsoft.VisualStudio.TestTools.UnitTesting
{
    /// <summary>
    /// Attribute to define in-line data for a test method.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class MatrixAttribute : Attribute, ITestDataSource
    {
        protected internal static TestIdGenerationStrategy TestIdGenerationStrategy { get; internal set; }

        /// <summary>
        /// Gets or sets display name in test results for customization.
        /// </summary>
        public string DisplayName { get; set; } = "";

        /// <inheritdoc />
        public IEnumerable<object[]> GetData(MethodInfo methodInfo)
        {
            // Retrieve parameters values
            var attributes = methodInfo.GetCustomAttributes<MatrixParameterAttribute>(true);
            var values = attributes.Select(p => p.Values).ToArray();

            if (attributes.Count() < 2)
            {
                //MeaningLessToHaveOnlyOneSetOfParameters();
                throw new Exception("You need atleast two MatrixParameterAttribute, otherwise use DataRowAttribute");
                //#warning "You need atleast two MatrixParameterAttribute, otherwise use DataRowAttribute"
            }

            var indices = new int[values.Length];

            // Retrieve any excluded combinations
            var excluded = methodInfo.GetCustomAttributes<MatrixExcludeAttribute>(true);

            // Combine all the values
            while (true)
            {
                // Create new arguments
                var arg = CreateArguments(indices, values);

                // Check if needs to be excluded
                if (IsExcluded(excluded, arg) == false)
                {
                    yield return arg!;
                }

                // Increment indices
                for (int i = indices.Length - 1; i >= 0; i--)
                {
                    indices[i]++;
                    if (indices[i] >= values[i].Length)
                    {
                        indices[i] = 0;

                        if (i == 0)
                        {
                            yield break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        static bool IsExcluded(IEnumerable<MatrixExcludeAttribute> excluded, object[] arg)
        {
            return excluded.Any(e => e.Values
                           .Zip(arg)
                           .All(v => v.First?.Equals(v.Second) == true));
        }

        static object[] CreateArguments(int[] indices, object[][] values)
        {
            var arg = new object[indices.Length];
            for (int i = 0; i < indices.Length; i++)
            {
                arg[i] = values[i][indices[i]];
            }
            return arg;
        }
        /*
                void MeaningLessToHaveOnlyOneSetOfParameters()
                {
                    throw new Exception("You need atleast two MatrixParameterAttribute, otherwise use DataRowAttribute");
        //#warning "You need atleast two MatrixParameterAttribute, otherwise use DataRowAttribute"
                }
        */
        /// <inheritdoc />
        public string GetDisplayName(MethodInfo methodInfo, object[] data)
        {
            if (string.IsNullOrWhiteSpace(DisplayName))
            {
                return $"{methodInfo.Name} ({string.Join(",", data)})";
            }
            return $"{DisplayName} ({string.Join(",", data)})";
        }
    }
}
