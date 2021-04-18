using System;
using System.Reflection;
using StaticMock.Helpers;

namespace StaticMock.Services.Throw
{
    internal class ThrowService : IThrowService
    {
        private static object _injectedException;

        private readonly MethodInfo _originalMethodInfo;

        public ThrowService(MethodInfo originalMethodInfo)
        {
            _originalMethodInfo = originalMethodInfo ?? throw new ArgumentNullException(nameof(originalMethodInfo));
        }


        public void Throws(Type exceptionType)
        {
            if (exceptionType == null)
            {
                throw new ArgumentNullException(nameof(exceptionType));
            }

            if (exceptionType.IsSubclassOf(typeof(Exception)))
            {
                throw new Exception($"{exceptionType.FullName} is not an Exception");
            }

            _injectedException = Activator.CreateInstance(exceptionType);

            Action injectionMethod = () => throw (Exception) _injectedException;
            CodeInjectionHelper.Inject(_originalMethodInfo, injectionMethod.Method);
        }

        public void Throws<TException>() where TException : Exception, new()
        {
            Action injectionMethod = () => throw new TException();
            CodeInjectionHelper.Inject(_originalMethodInfo, injectionMethod.Method);
        }
    }
}
