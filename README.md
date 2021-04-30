# SMock
SMock is opensource lib for mocking static and instance methods.
# Installation
Download and install the package from [NuGet](https://www.nuget.org/packages/SMock/)
# Getting Started
## Code Examples
### Returns
```cs
Mock.Setup(() => StaticClass.MethodToMock(), () =>
{
    var actualResult = StaticClass.MethodToMock();
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
# Library license
The library is available under the [MIT license](https://github.com/SvetlovA/static-mock/blob/master/LICENSE).