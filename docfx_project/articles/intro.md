# SMock
SMock is opensource lib for mocking static and instance methods and properties.
# Installation
Download and install the package from [NuGet](https://www.nuget.org/packages/SMock/) or [GitHub](https://github.com/SvetlovA/static-mock/pkgs/nuget/SMock)
# Getting Started
## Hook Manager Types
When you are using SMock you have a possibilty to switch hook manager type for your needs.
| Hook Manager Type | Description |
| ----------------- | ----------- |
| MonoMod | Using [MonoMod](https://github.com/MonoMod/MonoMod) library to provide hook (**Default**) |
| Harmony | Using [Harmony](https://github.com/pardeike/Harmony) library to provide hook |

There is a possibilty to set hook manager type manually:
```cs
Mock.SetHookManagerType(HookManagerType.MonoMod);
```
## Code Examples
### Returns
```cs
Mock.Setup(context => StaticClass.MethodToMock(context.It.IsAny<int>()), () =>
{
    var actualResult = StaticClass.MethodToMock(1);
    Assert.AreNotEqual(originalResult, actualResult);
    Assert.AreEqual(expectedResult, actualResult);
}).Returns(expectedResult);

Mock.Setup(context => StaticClass.MethodToMock(context.It.IsAny<int>()), () =>
{
    var actualResult = StaticClass.MethodToMock(1);
    Assert.AreNotEqual(originalResult, actualResult);
    Assert.AreEqual(expectedResult, actualResult);
}).Returns(() => expectedResult);

Mock.Setup(context => StaticClass.MethodToMock(context.It.Is<int>(x => x == 1)), () =>
{
    var actualResult = StaticClass.MethodToMock(1);
    Assert.AreNotEqual(originalResult, actualResult);
    Assert.AreEqual(expectedResult, actualResult);
}).Returns<int>(argument => argument);

Mock.Setup(context => StaticClass.MethodToMockAsync(context.It.IsAny<int>()), async () =>
{
    var actualResult = await StaticClass.MethodToMockAsync(1);
    Assert.AreNotEqual(originalResult, actualResult);
    Assert.AreEqual(expectedResult, actualResult);
}).Returns<int>(async argument => await Task.FromResult(argument));
```
[Other returns setup examples](https://github.com/SvetlovA/static-mock/tree/master/src/StaticMock.Tests/Tests/ReturnsTests)
### Throws
```cs
Mock.Setup(() => StaticClass.MethodToMock(), () =>
{
    Assert.Throws<Exception>(() => StaticClass.MethodToMock());
}).Throws<Exception>();
```
[Other throws setup examples](https://github.com/SvetlovA/static-mock/tree/master/src/StaticMock.Tests/Tests/ThrowsTests)
### Callback
```cs
Mock.Setup(() => StaticClass.MethodToMock(), () =>
{
    var actualResult = StaticClass.MethodToMock();
    Assert.AreNotEqual(originalResult, actualResult);
    Assert.AreEqual(expectedResult, actualResult);
}).Callback(() =>
{
    DoSomething();
});

Mock.Setup(context => StaticClass.MethodToMock(context.It.IsAny<int>()), () =>
{
    var actualResult = StaticClass.MethodToMock(1);
    Assert.AreNotEqual(originalResult, actualResult);
    Assert.AreEqual(expectedResult, actualResult);
}).Callback<int>(argument =>
{
    DoSomething(argument);
});
```
[Other callback setup examples](https://github.com/SvetlovA/static-mock/tree/master/src/StaticMock.Tests/Tests/CallbackTests)
### SetupDefault
```cs
Mock.SetupDefault(() => StaticClass.VoidMethodToMock(), () =>
{
    StaticClass.VoidMethodToMock(); // This method do nothing
});
```
[Other examples](https://github.com/SvetlovA/static-mock/tree/master/src/StaticMock.Tests/Tests)
# Library license
The library is available under the [MIT license](https://github.com/SvetlovA/static-mock/blob/master/LICENSE).