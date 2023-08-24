using System;

namespace StaticMock.Mocks.Implementation.Hierarchical;

internal class Disposable : IDisposable
{
    public void Dispose()
    {
        // Dispose for support mocks interfaces
    }
}