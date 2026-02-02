using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    public static class CodeGenUtils
    {
        public static string ToPascal(string name)
        {
            return string.Concat(
                name.Split(' ')
                    .Where(x => !string.IsNullOrEmpty(x))
                    .Select(x => char.ToUpper(x[0]) + x.Substring(1))
            );
        }

        public static string ToCamel(string name)
        {
            var pascal = ToPascal(name);
            return char.ToLower(pascal[0]) + pascal.Substring(1);
        }
    }
}