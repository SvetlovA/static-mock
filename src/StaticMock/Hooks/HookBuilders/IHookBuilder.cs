using System;
using System.Reflection;

namespace StaticMock.Hooks.HookBuilders;

internal interface IHookBuilder
{
    MethodInfo CreateCallbackHook(Action callback);
    MethodInfo CreateCallbackHook<TArg>(Action<TArg> callback);
    MethodInfo CreateCallbackHook<TArg1, TArg2>(Action<TArg1, TArg2> callback);
    MethodInfo CreateCallbackHook<TArg1, TArg2, TArg3>(Action<TArg1, TArg2, TArg3> callback);
    MethodInfo CreateCallbackHook<TArg1, TArg2, TArg3, TArg4>(Action<TArg1, TArg2, TArg3, TArg4> callback);
    MethodInfo CreateCallbackHook<TArg1, TArg2, TArg3, TArg4, TArg5>(Action<TArg1, TArg2, TArg3, TArg4, TArg5> callback);
    MethodInfo CreateCallbackHook<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6> callback);
    MethodInfo CreateCallbackHook<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7> callback);
    MethodInfo CreateCallbackHook<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8> callback);
    MethodInfo CreateCallbackHook<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9> callback);

    MethodInfo CreateReturnHook<TReturn>(TReturn value);
    MethodInfo CreateReturnHook<TReturn>(Func<TReturn> getValue);
    MethodInfo CreateReturnHook<TArg, TReturn>(Func<TArg, TReturn> getValue);
    MethodInfo CreateReturnHook<TArg1, TArg2, TReturn>(Func<TArg1, TArg2, TReturn> getValue);
    MethodInfo CreateReturnHook<TArg1, TArg2, TArg3, TReturn>(Func<TArg1, TArg2, TArg3, TReturn> getValue);
    MethodInfo CreateReturnHook<TArg1, TArg2, TArg3, TArg4, TReturn>(Func<TArg1, TArg2, TArg3, TArg4, TReturn> getValue);
    MethodInfo CreateReturnHook<TArg1, TArg2, TArg3, TArg4, TArg5, TReturn>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TReturn> getValue);
    MethodInfo CreateReturnHook<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TReturn>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TReturn> getValue);
    MethodInfo CreateReturnHook<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TReturn>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TReturn> getValue);
    MethodInfo CreateReturnHook<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TReturn>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TReturn> getValue);
    MethodInfo CreateReturnHook<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TReturn>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TReturn> getValue);
    
    MethodInfo CreateReturnAsyncHook<TReturn>(TReturn value);

    MethodInfo CreateThrowsHook<TException>(TException exception) where TException : Exception, new();
}