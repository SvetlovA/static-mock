using System;
using System.Threading.Tasks;
using StaticMock.Hooks;
using StaticMock.Hooks.HookBuilders;
using StaticMock.Mocks.Returns;

namespace StaticMock.Mocks.Implementation.Sequential;

internal class AsyncFuncMock<TReturnValue> : FuncMock<Task<TReturnValue>>, IAsyncFuncMock<TReturnValue>
{
    private readonly IHookBuilder _hookBuilder;
    private readonly IHookManager _hookManager;

    public AsyncFuncMock(IHookBuilder hookBuilder, IHookManager hookManager) : base(hookBuilder, hookManager)
    {
        _hookBuilder = hookBuilder;
        _hookManager = hookManager;
    }

    public IDisposable ReturnsAsync(TReturnValue value) =>
        new ReturnsMock<TReturnValue>(_hookBuilder, _hookManager).ReturnsAsync(value);
}