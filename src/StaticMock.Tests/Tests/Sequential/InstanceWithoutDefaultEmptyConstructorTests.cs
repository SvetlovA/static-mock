﻿using NUnit.Framework;
using StaticMock.Entities;
using StaticMock.Tests.Common.TestEntities;

namespace StaticMock.Tests.Tests.Sequential;

[TestFixture]
public class InstanceWithoutDefaultEmptyConstructorTests
{
    [Test]
    public void TestInstanceSetupActionWithoutInstanceProperty()
    {
        Assert.Throws<Exception>(() =>
            Mock.SetupAction(typeof(TestInstanceWithoutDefaultEmptyConstructor), nameof(TestInstanceWithoutDefaultEmptyConstructor.TestVoidMethodWithoutParametersThrowsException)));
    }

    [Test]
    public void TestInstanceSetupPropertyWithoutInstanceProperty()
    {
        Assert.Throws<Exception>(() =>
            Mock.SetupProperty(typeof(TestInstanceWithoutDefaultEmptyConstructor), nameof(TestInstanceWithoutDefaultEmptyConstructor.ObjectProperty)));
    }

    [Test]
    public void TestInstanceSetupWithoutInstanceProperty()
    {
        Assert.Throws<Exception>(() =>
            Mock.Setup(typeof(TestInstanceWithoutDefaultEmptyConstructor), nameof(TestInstanceWithoutDefaultEmptyConstructor.TestMethodReturn1WithoutParameters)));
    }

    [Test]
    public void TestInstanceMethodReturns()
    {
        var testInstance = new TestInstanceWithoutDefaultEmptyConstructor(new TestInstance(), default, default);
        Assert.AreEqual(1, testInstance.TestMethodReturn1WithoutParameters());
        const int expectedResult = 2;

        using (Mock.Setup(typeof(TestInstanceWithoutDefaultEmptyConstructor), nameof(TestInstanceWithoutDefaultEmptyConstructor.TestMethodReturn1WithoutParameters), new SetupProperties { Instance = testInstance }).Returns(expectedResult))
        {
            var actualResult = testInstance.TestMethodReturn1WithoutParameters();
            Assert.AreEqual(expectedResult, actualResult);
        }

        Assert.AreEqual(1, testInstance.TestMethodReturn1WithoutParameters());
    }

    [Test]
    public void TestInstanceMethodReturnsCallback()
    {
        var testInstance = new TestInstanceWithoutDefaultEmptyConstructor(new TestInstance(), default, default);
        Assert.AreEqual(1, testInstance.TestMethodReturn1WithoutParameters());
        const int expectedResult = 2;

        using (Mock.Setup(typeof(TestInstanceWithoutDefaultEmptyConstructor), nameof(TestInstanceWithoutDefaultEmptyConstructor.TestMethodReturn1WithoutParameters), new SetupProperties { Instance = testInstance }).Returns(() => expectedResult))
        {
            var actualResult = testInstance.TestMethodReturn1WithoutParameters();
            Assert.AreEqual(expectedResult, actualResult);
        }

        Assert.AreEqual(1, testInstance.TestMethodReturn1WithoutParameters());
    }

    [Test]
    public void TestInstanceMethodReturnsGeneric()
    {
        var testInstance = new TestInstanceWithoutDefaultEmptyConstructor(new TestInstance(), default, default);
        Assert.AreEqual(1, testInstance.TestMethodReturn1WithoutParameters());
        const int expectedResult = 2;

        using (Mock.Setup(() => testInstance.TestMethodReturn1WithoutParameters()).Returns(expectedResult))
        {
            var actualResult = testInstance.TestMethodReturn1WithoutParameters();
            Assert.AreEqual(expectedResult, actualResult);
        }

        Assert.AreEqual(1, testInstance.TestMethodReturn1WithoutParameters());
    }

    [Test]
    public void TestInstanceMethodReturnsGenericCallback()
    {
        var testInstance = new TestInstanceWithoutDefaultEmptyConstructor(new TestInstance(), default, default);
        Assert.AreEqual(1, testInstance.TestMethodReturn1WithoutParameters());
        const int expectedResult = 2;

        using (Mock.Setup(() => testInstance.TestMethodReturn1WithoutParameters()).Returns(() => expectedResult))
        {
            var actualResult = testInstance.TestMethodReturn1WithoutParameters();
            Assert.AreEqual(expectedResult, actualResult);
        }

        Assert.AreEqual(1, testInstance.TestMethodReturn1WithoutParameters());
    }
}