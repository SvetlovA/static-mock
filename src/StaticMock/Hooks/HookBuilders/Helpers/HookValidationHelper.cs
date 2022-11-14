using System.Reflection;

namespace StaticMock.Hooks.HookBuilders.Helpers
{
    internal static class HookValidationHelper
    {
        public static void Validate<TArg, TReturn>(MethodInfo originalMethodInfo, Func<TArg, TReturn> getValue)
        {
            var originalMethodInfoParameters = originalMethodInfo.GetParameters();
            var hookMethodParameters = getValue.Method.GetParameters();

            ValidateParameters(originalMethodInfoParameters, hookMethodParameters);
        }

        public static void Validate<TArg1, TArg2, TReturn>(MethodInfo originalMethodInfo, Func<TArg1, TArg2, TReturn> getValue)
        {
            var originalMethodInfoParameters = originalMethodInfo.GetParameters();
            var hookMethodParameters = getValue.Method.GetParameters();

            ValidateParameters(originalMethodInfoParameters, hookMethodParameters);
        }

        public static void Validate<TArg1, TArg2, TArg3, TReturn>(MethodInfo originalMethodInfo, Func<TArg1, TArg2, TArg3, TReturn> getValue)
        {
            var originalMethodInfoParameters = originalMethodInfo.GetParameters();
            var hookMethodParameters = getValue.Method.GetParameters();

            ValidateParameters(originalMethodInfoParameters, hookMethodParameters);
        }

        public static void Validate<TArg1, TArg2, TArg3, TArg4, TReturn>(MethodInfo originalMethodInfo, Func<TArg1, TArg2, TArg3, TArg4, TReturn> getValue)
        {
            var originalMethodInfoParameters = originalMethodInfo.GetParameters();
            var hookMethodParameters = getValue.Method.GetParameters();

            ValidateParameters(originalMethodInfoParameters, hookMethodParameters);
        }

        public static void Validate<TArg1, TArg2, TArg3, TArg4, TArg5, TReturn>(MethodInfo originalMethodInfo, Func<TArg1, TArg2, TArg3, TArg4, TArg5, TReturn> getValue)
        {
            var originalMethodInfoParameters = originalMethodInfo.GetParameters();
            var hookMethodParameters = getValue.Method.GetParameters();

            ValidateParameters(originalMethodInfoParameters, hookMethodParameters);
        }

        public static void Validate<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TReturn>(MethodInfo originalMethodInfo, Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TReturn> getValue)
        {
            var originalMethodInfoParameters = originalMethodInfo.GetParameters();
            var hookMethodParameters = getValue.Method.GetParameters();

            ValidateParameters(originalMethodInfoParameters, hookMethodParameters);
        }

        public static void Validate<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TReturn>(MethodInfo originalMethodInfo, Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TReturn> getValue)
        {
            var originalMethodInfoParameters = originalMethodInfo.GetParameters();
            var hookMethodParameters = getValue.Method.GetParameters();

            ValidateParameters(originalMethodInfoParameters, hookMethodParameters);
        }

        public static void Validate<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TReturn>(MethodInfo originalMethodInfo, Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TReturn> getValue)
        {
            var originalMethodInfoParameters = originalMethodInfo.GetParameters();
            var hookMethodParameters = getValue.Method.GetParameters();

            ValidateParameters(originalMethodInfoParameters, hookMethodParameters);
        }

        public static void Validate<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TReturn>(MethodInfo originalMethodInfo, Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TReturn> getValue)
        {
            var originalMethodInfoParameters = originalMethodInfo.GetParameters();
            var hookMethodParameters = getValue.Method.GetParameters();

            ValidateParameters(originalMethodInfoParameters, hookMethodParameters);
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
    }
}
