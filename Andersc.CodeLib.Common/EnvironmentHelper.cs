using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Andersc.CodeLib.Common
{
    /// <summary>
    /// Helper class that retrieves information about OS environment.
    /// </summary>
    public class EnvironmentHelper
    {
        public static IDictionary GetEnvironmentVariables()
        {
            return Environment.GetEnvironmentVariables();
        }

        public static bool HasEnvironmentVar(string name)
        {
            foreach (string varName in GetEnvironmentVariables().Keys)
            {
                if (string.Equals(name, varName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
