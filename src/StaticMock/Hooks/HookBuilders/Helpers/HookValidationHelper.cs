using System.Reflection;

namespace StaticMock.Hooks.HookBuilders.Helpers
{
    internal static class HookValidationHelper
    {
        public static void Validate(MethodInfo originalMethodInfo, MethodInfo hookedMethodInfo)
        {
            var originalMethodInfoParameters = originalMethodInfo.GetParameters();
            var hookMethodParameters = hookedMethodInfo.GetParameters();

            ValidateParameters(originalMethodInfoParameters, hookMethodParameters);
            ValidateReturnType(originalMethodInfo.ReturnType, hookedMethodInfo.ReturnType);
        }

        private static void ValidateParameters(IReadOnlyList<ParameterInfo> originalMethodParameters, IReadOnlyList<ParameterInfo> hookMethodParameters)
        {
            if (originalMethodParameters.Count != hookMethodParameters.Count)
            {
                throw new Exception(
                    $"Parameters count mismatch. Original method parameters count: {originalMethodParameters.Count}. Hook method parameters count {hookMethodParameters.Count}");
            }

            for (var i = 0; i < originalMethodParameters.Count; i++)
            {
                var originalParameterType = originalMethodParameters[i].ParameterType;
                var hookParameterType = hookMethodParameters[i].ParameterType;

                if (originalParameterType != hookParameterType)
                {
                    throw new Exception(
                        $"Parameters type mismatch. {i + 1} original parameter type is {originalParameterType} and {i + 1} hook parameter type is {hookParameterType}");
                }
            }
        }

        public static void ValidateReturnType(Type originalMethodReturnType, Type hookReturnType)
        {
            if (originalMethodReturnType.IsGenericType &&
                originalMethodReturnType.GetGenericTypeDefinition() == typeof(Task<>) &&
                (!hookReturnType.IsGenericType ||
                hookReturnType.GetGenericTypeDefinition() != typeof(Task<>)))
            {
                var genericArgumentType = originalMethodReturnType.GetGenericArguments().Single();
                if (genericArgumentType != hookReturnType)
                {
                    throw new Exception(
                        $"Return types mismatch. Original generic return type is {genericArgumentType}. Hook return type is {hookReturnType}");
                }
            }
            else if (originalMethodReturnType != hookReturnType)
            {
                throw new Exception(
                    $"Return types mismatch. Original return type is {originalMethodReturnType}. Hook return type is {hookReturnType}");
            }
        }
    }
}
