namespace StaticMock.Helpers
{
    internal static class InjectionMethods
    {
        public static object InjectionValue { get; set; }

        public static object ReturnInjectionMethod()
        {
            return InjectionValue;
        }
    }
}
