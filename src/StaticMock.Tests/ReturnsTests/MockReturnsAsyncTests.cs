using NUnit.Framework;
using StaticMock.Tests.TestEntities;

namespace StaticMock.Tests.ReturnsTests;

[TestFixture]
public class MockReturnsAsyncTests
{
    //[Test]
    //public async Task TestSetupReturnsAsyncMethodsReturnTask()
    //{
    //    var originalResult = await TestStaticAsyncClass.TestMethodReturnTaskWithoutParameters();
    //    Assert.AreEqual(1, originalResult);
    //    var expectedResult = 2;

    //    Mock.Setup(typeof(TestStaticAsyncClass), nameof(TestStaticAsyncClass.TestMethodReturnTaskWithoutParameters), async () =>
    //    {
    //        var actualResult = await TestStaticAsyncClass.TestMethodReturnTaskWithoutParameters();
    //        Assert.AreEqual(expectedResult, actualResult);
    //    }).ReturnsAsync(expectedResult);
    //}

    //[Test]
    //public async Task TestSetupReturnsAsyncMethodsReturnTaskAsync()
    //{
    //    var originalResult = await TestStaticAsyncClass.TestMethodReturnTaskWithoutParametersAsync();
    //    Assert.AreEqual(1, originalResult);
    //    var expectedResult = 2;

    //    Mock.Setup(typeof(TestStaticAsyncClass), nameof(TestStaticAsyncClass.TestMethodReturnTaskWithoutParametersAsync), async () =>
    //    {
    //        var actualResult = await TestStaticAsyncClass.TestMethodReturnTaskWithoutParametersAsync();
    //        Assert.AreEqual(expectedResult, actualResult);
    //    }).ReturnsAsync(expectedResult);
    //}

    //[Test]
    //public async Task TestSetupInstanceReturnsAsyncMethodsReturnTask()
    //{
    //    var instance = new TestInstance();
    //    var originalResult = await instance.TestMethodReturnTaskWithoutParameters();
    //    Assert.AreEqual(1, originalResult);
    //    var expectedResult = 2;

    //    Mock.Setup(typeof(TestInstance), nameof(TestInstance.TestMethodReturnTaskWithoutParameters), async () =>
    //    {
    //        var actualResult = await instance.TestMethodReturnTaskWithoutParameters();
    //        Assert.AreEqual(expectedResult, actualResult);
    //    }).ReturnsAsync(expectedResult);
    //}

    //[Test]
    //public async Task TestSetupInstanceReturnsAsyncMethodsReturnTaskAsync()
    //{
    //    var instance = new TestInstance();
    //    var originalResult = await instance.TestMethodReturnTaskWithoutParameters();
    //    Assert.AreEqual(1, originalResult);
    //    var expectedResult = 2;

    //    Mock.Setup(typeof(TestInstance), nameof(TestInstance.TestMethodReturnTaskWithoutParametersAsync), async () =>
    //    {
    //        var actualResult = await instance.TestMethodReturnTaskWithoutParametersAsync();
    //        Assert.AreEqual(expectedResult, actualResult);
    //    }).ReturnsAsync(expectedResult);
    //}
}