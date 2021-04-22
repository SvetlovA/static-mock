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
        private readonly Action _action;

        public MockService(MethodInfo originalMethod, Action action)
        {
            _originalMethodInfo = originalMethod ?? throw new ArgumentNullException(nameof(originalMethod));
            _action = action ?? throw new ArgumentNullException(nameof(action));
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
                using (valueReturnService.Returns(value))
                {
                    _action();
                }
                return;
            }

            var referenceReturnService = new ReferenceReturnMockService(_originalMethodInfo);
            using (referenceReturnService.Returns(value))
            {
                _action();
            }
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
