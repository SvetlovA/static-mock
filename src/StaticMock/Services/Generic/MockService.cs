using System;
using System.Reflection;
using StaticMock.Services.Return.Generic;

namespace StaticMock.Services.Generic
{
    internal class MockService<TValue> : IMockService<TValue> where TValue : unmanaged
    {
        private readonly MethodInfo _originalMethodInfo;

        public MockService(MethodInfo originalMethodInfo)
        {
            _originalMethodInfo = originalMethodInfo ?? throw new ArgumentNullException(nameof(originalMethodInfo));
        }

        public void Returns(TValue value)
        {
            if (_originalMethodInfo.ReturnType == typeof(void))
            {
                throw new Exception("Original method must be function with return");
            }

            var returnService = new ReturnMockService<TValue>(_originalMethodInfo);
            returnService.Returns(value);
        }
    }
}
