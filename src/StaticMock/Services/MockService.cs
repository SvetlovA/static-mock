using System;
using System.Reflection;
using StaticMock.Services.Return;
using StaticMock.Services.Throw;

namespace StaticMock.Services
{
    internal class MockService : IMockService
    {
        private readonly MethodInfo _originalMethodInfo;

        public MockService(MethodInfo originalMethod)
        {
            _originalMethodInfo = originalMethod ?? throw new ArgumentNullException(nameof(originalMethod));
        }

        public void Returns(object value)
        {
            if (_originalMethodInfo.ReturnType == typeof(void))
            {
                throw new Exception("Original method must be function with return");
            }

            if (value.GetType().IsValueType)
            {
                throw new Exception(
                    $"This method can work only with reference types, use Generic implementation of {nameof(Returns)} method");
            }

            var returnService = new ReturnReferenceMockService(_originalMethodInfo);
            returnService.Returns(value);
        }

        public void Throws(Type exceptionType)
        {
            var throwService = new ThrowService(_originalMethodInfo);
            throwService.Throws(exceptionType);
        }

        public void Throws<TException>() where TException : Exception, new()
        {
            var throwService = new ThrowService(_originalMethodInfo);
            throwService.Throws<TException>();
        }
    }
}
