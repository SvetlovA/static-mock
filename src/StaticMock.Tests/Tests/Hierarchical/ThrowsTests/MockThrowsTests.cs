﻿using System.Reflection;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using StaticMock.Entities;
using StaticMock.Tests.Common.TestEntities;
using StaticMock.Tests.Entities;

namespace StaticMock.Tests.Tests.Hierarchical.ThrowsTests;

[TestFixture]
public class MockThrowsTests
{
    [Test]
    public void TestThrowsTestMethodReturn1WithoutParameters()
    {
        Mock.Setup(() => TestStaticClass.TestMethodReturn1WithoutParameters(), () =>
            {
                Assert.Throws<Exception>(() => TestStaticClass.TestMethodReturn1WithoutParameters());
            }).Throws(typeof(Exception));
    }

    [Test]
    public void TestThrowsTestVoidMethodWithoutParameters()
    {
        Mock.Setup(() => TestStaticClass.TestVoidMethodWithoutParameters(), () =>
            {
                Assert.Throws<Exception>(() => TestStaticClass.TestVoidMethodWithoutParameters());
            }).Throws(typeof(Exception));
    }

    [Test]
    public void TestThrowsTestVoidMethodWithParameters()
    {
        Mock.Setup(() => TestStaticClass.TestVoidMethodWithParameters(1), () =>
            {
                Assert.Throws<Exception>(() => TestStaticClass.TestVoidMethodWithParameters(1));
            }).Throws(typeof(Exception));
    }

    [Test]
    public void TestThrowsArgumentNullException()
    {
        Mock.Setup(() => TestStaticClass.TestVoidMethodWithoutParameters(), () =>
            {
                Assert.Throws<ArgumentNullException>(() => TestStaticClass.TestVoidMethodWithoutParameters());
            }).Throws(typeof(ArgumentNullException));
    }

    [Test]
    public void TestInstanceMethodThrows()
    {
        var testInstance = new TestInstance();

        Mock.Setup(typeof(TestInstance), nameof(TestInstance.TestMethodReturn1WithoutParameters), new SetupProperties { Instance = testInstance }, () =>
        {
            Assert.Throws<Exception>(() => testInstance.TestMethodReturn1WithoutParameters());
        }).Throws<Exception>();
    }

    [Test]
    public void TestGenericThrowsTestMethodReturnTask()
    {
        Mock.Setup(typeof(TestStaticAsyncClass), nameof(TestStaticAsyncClass.TestMethodReturnTask), () =>
        {
            ClassicAssert.ThrowsAsync<Exception>(() => TestStaticAsyncClass.TestMethodReturnTask());
        }).Throws<Exception>();
    }

    [Test]
    public void TestGenericThrowsTestMethodReturnTaskAsync()
    {
        Mock.Setup(typeof(TestStaticAsyncClass), nameof(TestStaticAsyncClass.TestMethodReturnTaskAsync), () =>
        {
            ClassicAssert.ThrowsAsync<Exception>(() => TestStaticAsyncClass.TestMethodReturnTaskAsync());
        }).Throws<Exception>();
    }

    [Test]
    public void TestGenericThrowsTestVoidMethodReturnTaskAsync()
    {
        Mock.SetupAction(typeof(TestStaticAsyncClass), nameof(TestStaticAsyncClass.TestVoidMethodReturnTaskAsync), () =>
        {
            Assert.Throws<Exception>(() => TestStaticAsyncClass.TestVoidMethodReturnTaskAsync());
        }).Throws<Exception>();
    }

    [Test]
    public void TestGenericThrowsTestMethodReturnTaskWithoutParametersAsync()
    {
        Mock.Setup(typeof(TestStaticAsyncClass), nameof(TestStaticAsyncClass.TestMethodReturnTaskWithoutParametersAsync), () =>
        {
            ClassicAssert.ThrowsAsync<Exception>(() => TestStaticAsyncClass.TestMethodReturnTaskWithoutParametersAsync());
        }).Throws<Exception>();
    }

    [Test]
    public void TestGenericThrowsTestMethodReturnTaskWithoutParameters()
    {
        Mock.Setup(typeof(TestStaticAsyncClass), nameof(TestStaticAsyncClass.TestMethodReturnTaskWithoutParameters), () =>
        {
            ClassicAssert.ThrowsAsync<Exception>(() => TestStaticAsyncClass.TestMethodReturnTaskWithoutParameters());
        }).Throws<Exception>();
    }

    [Test]
    public void TestGenericThrowsInstanceTestMethodReturnTask()
    {
        var instance = new TestInstance();

        Mock.Setup(typeof(TestInstance), nameof(TestInstance.TestMethodReturnTask), new SetupProperties { Instance = instance }, () =>
        {
            ClassicAssert.ThrowsAsync<Exception>(() => instance.TestMethodReturnTask());
        }).Throws<Exception>();
    }

    [Test]
    public void TestGenericThrowsInstanceTestMethodReturnTaskAsync()
    {
        var instance = new TestInstance();

        Mock.Setup(typeof(TestInstance), nameof(TestInstance.TestMethodReturnTaskAsync), new SetupProperties { Instance = instance }, () =>
        {
            ClassicAssert.ThrowsAsync<Exception>(() => instance.TestMethodReturnTaskAsync());
        }).Throws<Exception>();
    }

    [Test]
    public void TestGenericThrowsInstanceTestVoidMethodReturnTaskAsync()
    {
        var instance = new TestInstance();

        Mock.SetupAction(typeof(TestInstance), nameof(TestInstance.TestVoidMethodReturnTaskAsync), new SetupProperties { Instance = instance}, () =>
        {
            Assert.Throws<Exception>(() => instance.TestVoidMethodReturnTaskAsync());
        }).Throws<Exception>();
    }

    [Test]
    public void TestGenericThrowsInstanceTestMethodReturnTaskWithoutParametersAsync()
    {
        var instance = new TestInstance();

        Mock.Setup(typeof(TestInstance), nameof(TestInstance.TestMethodReturnTaskWithoutParametersAsync), new SetupProperties { Instance = instance }, () =>
        {
            ClassicAssert.ThrowsAsync<Exception>(() => instance.TestMethodReturnTaskWithoutParametersAsync());
        }).Throws<Exception>();
    }

    [Test]
    public void TestGenericThrowsInstanceTestMethodReturnTaskWithoutParameters()
    {
        var instance = new TestInstance();

        Mock.Setup(typeof(TestInstance), nameof(TestInstance.TestMethodReturnTaskWithoutParameters), new SetupProperties { Instance = instance }, () =>
        {
            ClassicAssert.ThrowsAsync<Exception>(() => instance.TestMethodReturnTaskWithoutParameters());
        }).Throws<Exception>();
    }

    [Test]
    public void TestThrowsStaticIntProperty()
    {
        var originalValue = TestStaticClass.StaticIntProperty;
        ClassicAssert.AreEqual(default(int), originalValue);

        Mock.SetupProperty(typeof(TestStaticClass), nameof(TestStaticClass.StaticIntProperty), () =>
        {
            Assert.Throws<Exception>(() =>
            {
                var actualResult = TestStaticClass.StaticIntProperty;
            });
        }).Throws<Exception>();
    }

    [Test]
    public void TestThrowsStaticObjectProperty()
    {
        var originalValue = TestStaticClass.StaticObjectProperty;
        ClassicAssert.AreEqual(default, originalValue);

        Mock.SetupProperty(typeof(TestStaticClass), nameof(TestStaticClass.StaticObjectProperty), () =>
        {
            Assert.Throws<Exception>(() =>
            {
                var actualResult = TestStaticClass.StaticObjectProperty;
            });
        }).Throws<Exception>();
    }

    [Test]
    public void TestThrowsStaticIntPropertyInstance()
    {
        var instance = new TestInstance();
        var originalValue = instance.IntProperty;
        ClassicAssert.AreEqual(default(int), originalValue);

        Mock.SetupProperty(typeof(TestInstance), nameof(TestInstance.IntProperty), new SetupProperties { Instance = instance }, () =>
        {
            Assert.Throws<Exception>(() =>
            {
                var actualResult = instance.IntProperty;
            });
        }).Throws<Exception>();
    }

    [Test]
    public void TestThrowsStaticObjectPropertyInstance()
    {
        var instance = new TestInstance();
        var originalValue = instance.ObjectProperty;
        ClassicAssert.AreEqual(default, originalValue);

        Mock.SetupProperty(typeof(TestInstance), nameof(TestInstance.ObjectProperty), new SetupProperties { Instance = instance }, () =>
        {
            Assert.Throws<Exception>(() =>
            {
                var actualResult = instance.ObjectProperty;
            });
        }).Throws<Exception>();
    }

    [Test]
    public void TestInstanceThrowsPrivateMethodReturn1WithoutParameters()
    {
        var testInstance = new TestInstance();
        Type type = testInstance.GetType();
        MethodInfo methodInfo = type.GetMethod("TestPrivateMethodReturn1WithoutParameters", BindingFlags.NonPublic | BindingFlags.Instance);
        Mock.Setup(typeof(TestInstance), methodInfo.Name, new SetupProperties
        {
            Instance = testInstance,
            BindingFlags = BindingFlags.NonPublic | BindingFlags.Instance
        }, () =>
        {
            try
            {
                var actualResult = methodInfo.Invoke(testInstance, new object[] { });
            }
            catch (Exception e)
            {
                ClassicAssert.AreEqual(typeof(Exception), e.InnerException.GetType());
            }
        }).Throws(typeof(Exception));
    }

    [Test]
    public void TestThrowsPrivateIntProperty()
    {
        var testInstance = new TestInstance();
        Type type = testInstance.GetType();
        PropertyInfo propertyInfo = type.GetProperty("PrivateIntProperty", BindingFlags.NonPublic | BindingFlags.Instance);
        MethodInfo mothodInfo = propertyInfo.GetMethod;

        Mock.SetupProperty(typeof(TestInstance), propertyInfo.Name, new SetupProperties
        {
            Instance = testInstance,
            BindingFlags = BindingFlags.NonPublic | BindingFlags.Instance
        }, () =>
        {
            try
            {
                var actualResult = mothodInfo.Invoke(testInstance, new object[] { });
            }
            catch (Exception e)
            {
                ClassicAssert.AreEqual(typeof(Exception), e.InnerException.GetType());
            }


        }).Throws(typeof(Exception));
    }

    [Test]
    public void TestThrowsPrivateObjectProperty()
    {

        var testInstance = new TestInstance();
        Type type = testInstance.GetType();
        PropertyInfo propertyInfo = type.GetProperty("PrivateObjectProperty", BindingFlags.NonPublic | BindingFlags.Instance);
        MethodInfo mothodInfo = propertyInfo.GetMethod;

        Mock.SetupProperty(typeof(TestInstance), propertyInfo.Name, new SetupProperties
        {
            Instance = testInstance,
            BindingFlags = BindingFlags.NonPublic | BindingFlags.Instance
        }, () =>
        {
            try
            {
                var actualResult = mothodInfo.Invoke(testInstance, new object[] { });
            }
            catch (Exception e)
            {
                ClassicAssert.AreEqual(typeof(Exception), e.InnerException.GetType());
            }
        }).Throws(typeof(Exception));
    }

    [Test]
    public void TestThrowsTestPrivateMethodReturn1WithoutParameters()
    {

        Type type = typeof(TestStaticClass);
        MethodInfo methodInfo = type.GetMethod("TestPrivateStaticMethodReturn1WithoutParameters", BindingFlags.Static | BindingFlags.NonPublic);
        Mock.Setup(type, methodInfo.Name, BindingFlags.Static | BindingFlags.NonPublic, () =>
         {
             try
             {
                 int result = (int)methodInfo.Invoke(type, new object[] { });
             }
             catch (Exception e)
             {
                 ClassicAssert.AreEqual(typeof(Exception), e.InnerException.GetType());
             }

         }).Throws(typeof(Exception));
    }

    [Test]
    public void TestThrowsPrivateStaticIntProperty()
    {
        Type type = typeof(TestStaticClass);
        PropertyInfo propertyInfo = type.GetProperty("PrivateStaticIntProperty", BindingFlags.NonPublic | BindingFlags.Static);
        MethodInfo mothodInfo = propertyInfo.GetMethod;
        var originalValue = mothodInfo.Invoke(type, new object[] { });
        ClassicAssert.AreEqual(default(int), originalValue);

        Mock.SetupProperty(typeof(TestStaticClass), propertyInfo.Name, BindingFlags.NonPublic | BindingFlags.Static, () =>
        {
            try
            {
                var actualResult = (int)mothodInfo.Invoke(type, new object[] { });
            }
            catch (Exception e)
            {
                ClassicAssert.AreEqual(typeof(Exception), e.InnerException.GetType());
            }
        }).Throws<Exception>();
    }

    [Test]
    public void TestThrowsPrivateStaticObjectProperty()
    {
        Type type = typeof(TestStaticClass);
        PropertyInfo propertyInfo = type.GetProperty("PrivateStaticObjectProperty", BindingFlags.NonPublic | BindingFlags.Static);
        MethodInfo mothodInfo = propertyInfo.GetMethod;
        var originalValue = mothodInfo.Invoke(type, new object[] { });
        ClassicAssert.AreEqual(default, originalValue);

        Mock.SetupProperty(typeof(TestStaticClass), propertyInfo.Name, BindingFlags.NonPublic | BindingFlags.Static, () =>
        {
            try
            {
                var actualResult = (int)mothodInfo.Invoke(type, new object[] { });
            }
            catch (Exception e)
            {
                ClassicAssert.AreEqual(typeof(Exception), e.InnerException.GetType());
            }
        }).Throws<Exception>();
    }

    [Test]
    public void TestSetupThrowsWithGenericTestMethodReturnDefaultWithoutParameters()
    {
        var originalResult = TestStaticClass.GenericTestMethodReturnDefaultWithoutParameters<int>();
        ClassicAssert.AreEqual(0, originalResult);

        Mock.Setup(typeof(TestStaticClass), nameof(TestStaticClass.GenericTestMethodReturnDefaultWithoutParameters), new SetupProperties { GenericTypes = new[] { typeof(int) } },
            () =>
            {
                Assert.Throws<Exception>(() => TestStaticClass.GenericTestMethodReturnDefaultWithoutParameters<int>());
            }).Throws<Exception>();
    }

    [Test]
    public void TestSetupThrowsWithGenericTestMethodReturnDefaultWithoutParametersInstance()
    {
        var testInstance = new TestInstance();
        var originalResult = testInstance.GenericTestMethodReturnDefaultWithoutParameters<int>();
        ClassicAssert.AreEqual(0, originalResult);

        Mock.Setup(typeof(TestInstance), nameof(TestInstance.GenericTestMethodReturnDefaultWithoutParameters),
            new SetupProperties
            {
                Instance = testInstance,
                GenericTypes = new[] { typeof(int) }
            },
            () =>
            {
                Assert.Throws<Exception>(() => testInstance.GenericTestMethodReturnDefaultWithoutParameters<int>());
            }).Throws<Exception>();
    }

    [Test]
    public void TestSetupThrowsWithGenericTestMethodReturn1WithoutParametersInstance()
    {
        var testInstance = new TestGenericInstance<int>();
        var originalResult = testInstance.GenericTestMethodReturnDefaultWithoutParameters();
        ClassicAssert.AreEqual(0, originalResult);

        Mock.Setup(typeof(TestGenericInstance<int>), nameof(TestGenericInstance<int>.GenericTestMethodReturnDefaultWithoutParameters),
            new SetupProperties
            {
                Instance = testInstance,
                GenericTypes = new[] { typeof(int) }
            },
            () =>
            {
                Assert.Throws<Exception>(() => testInstance.GenericTestMethodReturnDefaultWithoutParameters());
            }).Throws<Exception>();
    }

    [Test]
    public void TestThrowsParameterLessCustomExceptionTestMethodReturn1WithoutParameters()
    {
        const string message = "testExceptionMessage";
        Mock.Setup(() => TestStaticClass.TestMethodReturn1WithoutParameters(), () =>
            {
                Assert.Throws<CustomExceptionWithoutDefaultConstructor>(() => TestStaticClass.TestMethodReturn1WithoutParameters(), message);
            }).Throws(typeof(CustomExceptionWithoutDefaultConstructor), message);
    }

    [Test]
    public void TestThrowsExceptionWithMessageTestMethodReturn1WithoutParameters()
    {
        const string message = "testExceptionMessage";
        Mock.Setup(() => TestStaticClass.TestMethodReturn1WithoutParameters(), () =>
        {
            Assert.Throws<Exception>(() => TestStaticClass.TestMethodReturn1WithoutParameters(), message);
        }).Throws(typeof(Exception), message);
    }
    
    [Test]
    public void TestGenericSetupReturnsDateTimeUtcNow()
    {
        Mock.Setup(context => DateTime.UtcNow, () =>
            {
                Assert.Throws<Exception>(() =>
                {
                    var x = DateTime.UtcNow;
                });
            })
            .Throws(typeof(Exception));
    }
    
    [Test]
    public void TestGenericSetupReturnsDateTimeNow()
    {
        Mock.Setup(context => DateTime.Now, () =>
            {
                Assert.Throws<Exception>(() =>
                {
                    var x = DateTime.Now;
                });
            })
            .Throws(typeof(Exception));
    }
    
    [Test]
    public void TestGenericSetupReturnsStaticUnsafeProperty()
    {
        Mock.Setup(context => TestStaticClass.UnsafeProperty, () =>
            {
                Assert.Throws<Exception>(() =>
                {
                    var x = TestStaticClass.UnsafeProperty;
                });
            })
            .Throws(typeof(Exception));
    }
    
    [Test]
    public void TestGenericSetupReturnsStaticUnsafeMethod()
    {
        Mock.Setup(context => TestStaticClass.TestUnsafeStaticMethod(), () =>
            {
                Assert.Throws<Exception>(() => TestStaticClass.TestUnsafeStaticMethod());
            })
            .Throws(typeof(Exception));
        
    }
    
    [Test]
    public void TestGenericSetupReturnsInstanceUnsafeProperty()
    {
        var instance = new TestInstance();
        Mock.Setup(context => instance.UnsafeProperty, () =>
            {
                Assert.Throws<Exception>(() =>
                {
                    var x = instance.UnsafeProperty;
                });
            })
            .Throws(typeof(Exception));
    }
    
    [Test]
    public void TestGenericSetupReturnsInstanceUnsafeMethod()
    {
        var instance = new TestInstance();
        Mock.Setup(context => instance.TestUnsafeInstanceMethod(), () =>
            {
                Assert.Throws<Exception>(() => instance.TestUnsafeInstanceMethod());
            })
            .Throws(typeof(Exception));
    }
}