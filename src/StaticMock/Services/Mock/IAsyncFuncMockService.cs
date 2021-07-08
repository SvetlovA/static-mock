﻿using System.Threading.Tasks;

namespace StaticMock.Services.Mock
{
    public interface IAsyncFuncMockService<TReturnValue> : IFuncMockService<Task<TReturnValue>>
    {
        void ReturnsAsync(TReturnValue value);
    }
}
