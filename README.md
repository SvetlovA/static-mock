# SMock
SMock is opensource lib for mocking static and instance methods and properties.
# Installation
Download and install the package from [NuGet](https://www.nuget.org/packages/SMock/)
# Getting Started
## Code Examples
### Returns
```cs
Mock.Setup(context => StaticClass.MethodToMock(context.It.IsAny<int>()), () =>
{
    var actualResult = StaticClass.MethodToMock(1);
    Assert.AreNotEqual(originalResult, actualResult);
    Assert.AreEqual(expectedResult, actualResult);
}).Returns(expectedResult);
```
### Throws
```cs
Mock.Setup(() => StaticClass.MethodToMock(), () =>
{
    Assert.Throws<Exception>(() => StaticClass.MethodToMock());
}).Throws<Exception>();
```
### SetupDefault
```cs
Mock.SetupDefault(() => StaticClass.VoidMethodToMock(), () =>
{
    StaticClass.VoidMethodToMock(); // This method do nothing
});
```
### Callback
```cs
Mock.Setup(() => StaticClass.MethodToMock(), () =>
{
    var actualResult = StaticClass.MethodToMock();
    Assert.AreNotEqual(originalResult, actualResult);
    Assert.AreEqual(expectedResult, actualResult);
}).Callback(() =>
{
    return expectedResult;
});
```
[Other examples](https://github.com/SvetlovA/static-mock/tree/master/src/StaticMock.Tests)
# Known Issues
* **System.AccessViolationException:** This exception most often occurs if you use x64 platform in your test project. If you have this exception **System.AccessViolationException: 'Attempted to read or write protected memory. This is often an indication that other memory is corrupt.'** try to move mocking methods away from each other or try to use x32 platform for your test project compilation.
# Library license
The library is available under the [MIT license](https://github.com/SvetlovA/static-mock/blob/master/LICENSE).