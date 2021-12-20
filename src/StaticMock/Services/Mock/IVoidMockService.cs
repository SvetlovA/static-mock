using System;

namespace StaticMock.Services.Mock;

public interface IVoidMockService : IMockService
{
    void Callback(Action callback);
}