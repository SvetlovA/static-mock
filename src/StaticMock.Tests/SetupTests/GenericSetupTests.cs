﻿using NUnit.Framework;
using StaticMock.Tests.Common.TestEntities;

namespace StaticMock.Tests.SetupTests;

[TestFixture]
public class GenericSetupTests
{
    [Test]
    public void TestGenericSetup()
    {
        Mock.Setup(() => TestStaticClass.TestMethodReturn1WithoutParameters(), () => { });
    }
}