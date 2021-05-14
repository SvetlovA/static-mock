namespace StaticMock
{
    public static class It
    {
        internal static bool IsAnySet { get; private set; }

        public static TType IsAny<TType>()
        {
            IsAnySet = true;
            return default;
        }
    }
}
