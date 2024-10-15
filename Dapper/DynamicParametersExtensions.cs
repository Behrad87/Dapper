using System.Reflection;

namespace Dapper
{
    /// <summary>
    /// An Extention Class to DynamicParameters
    /// </summary>

    public static class DynamicParametersExtensions
    {
        /// <summary>
        /// Add Parameter if exists
        /// </summary>
        /// <param name="dp">Extending DynamicParameter</param>
        /// <param name="paramName">Parameter Name</param>
        /// <param name="param">Parameter with type of dynamic</param>
        public static void AddIfExists(this DynamicParameters dp, string paramName, dynamic? param)
        {
            if (param is not null)
            {
                var pStr = param is string;
                if (pStr)
                {
                    if (string.IsNullOrEmpty(param))
                    {
                        return;
                    }
                }

                dp.Add(paramName, param);
            }
        }

        /// <summary>
        /// Add members of class as parameter; if the member is not null
        /// </summary>
        /// <typeparam name="T">Generic Type</typeparam>
        /// <param name="dp">Extending DynamicParameters</param>
        /// <param name="param"></param>
        public static void AddParametersIfExists<T>(this DynamicParameters dp, T param)
        {
            if (param is not null)
            {
                foreach (PropertyInfo prop in param.GetType().GetProperties())
                {
                    if (
                        !prop.Name.ToLower().Contains("monitoringtype")
                        && !prop.Name.ToLower().Contains("projecttype")
                    )
                    {
                        AddIfExists(dp, paramName: prop.Name, param: prop.GetValue(param));
                    }
                }
            }
        }
    }
}
