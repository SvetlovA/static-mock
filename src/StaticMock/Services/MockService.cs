using System;
using System.Reflection;
using StaticMock.Services.Return.Reference;
using StaticMock.Services.Return.Value;
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

        public void Returns<TValue>(TValue value)
        {
            if (_originalMethodInfo.ReturnType == typeof(void))
            {
                throw new Exception("Original method must be function with return");
            }

            if (typeof(TValue).IsValueType)
            {
                var valueReturnService = new ValueReturnMockService<TValue>(_originalMethodInfo);
                valueReturnService.Returns(value);
                return;
            }

            var referenceReturnService = new ReferenceReturnMockService(_originalMethodInfo);
            referenceReturnService.Returns(value);
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
