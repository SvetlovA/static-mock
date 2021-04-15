using System;
using StaticMock.Services;

namespace StaticMock
{
    public static class Mock
    {
        public static IMockService Setup(Type type, string methodName)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (methodName == null)
            {
                throw new ArgumentNullException(nameof(methodName));
            }

            var methodToReplace = type.GetMethod(methodName);

            if (methodToReplace == null)
            {
                throw new Exception($"Can't find method {methodName} of type {type.FullName}");
            }

            return new MockService(methodToReplace);
        }
    }
}
