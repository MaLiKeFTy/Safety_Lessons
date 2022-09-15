using System.Collections.Generic;
using System.Reflection;

namespace MonoServices.Core
{
    public static class CommandsFinder
    {
        public static string[] MonoServiceCommandNames(MonoService monoService)
        {

            List<string> tempCommandNames = new List<string>();

            List<string> tempDeclaredCommandNames = new List<string>();


            foreach (var method in monoService.GetType().GetMethods(
                BindingFlags.NonPublic |
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.DeclaredOnly))
            {
                if (method.Name.Contains("Command") && !method.Name.Contains("Commands") && !method.Name.Contains("InvokeCommand"))
                {
                    string listernerMethodName = method.Name.Replace("Command", "");
                    tempDeclaredCommandNames.Add(listernerMethodName);
                }
            }

            foreach (var method in monoService.GetType().GetMethods(
                BindingFlags.NonPublic |
                BindingFlags.Instance |
                BindingFlags.Public))
            {
                if (tempDeclaredCommandNames.Contains(method.Name.Replace("Command", "")))
                    continue;

                if (method.Name.Contains("Command") && !method.Name.Contains("Commands") && !method.Name.Contains("InvokeCommand"))
                {
                    string listernerMethodName = method.Name.Replace("Command", "");
                    tempCommandNames.Add(listernerMethodName);
                }
            }

            tempCommandNames.AddRange(MonoSeriveParamNames(monoService));
            tempCommandNames.AddRange(tempDeclaredCommandNames);

            return tempCommandNames.ToArray();
        }

        static List<string> MonoSeriveParamNames(MonoService monoService)
        {
            if (!(monoService is ParametersHandler))
                return new List<string>();

            var parametersHandler = (ParametersHandler)monoService;

            return parametersHandler.ParameterNames();
        }
    }
}

