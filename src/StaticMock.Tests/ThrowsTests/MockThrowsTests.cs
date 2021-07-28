using System;
using System.Reflection;
using NUnit.Framework;
using StaticMock.Entities;

namespace StaticMock.Tests.ThrowsTests
{
    [TestFixture]
    public class MockThrowsTests
    {
        [Test]
        public void TestThrowsTestMethodReturn1WithoutParameters()
        {
            Mock.Setup(() => TestStaticClass.TestMethodReturn1WithoutParameters(), () =>
                {
                    Assert.Throws<Exception>(() => TestStaticClass.TestMethodReturn1WithoutParameters());
                })
                .Throws(typeof(Exception));
        }

        [Test]
        public void TestThrowsTestVoidMethodWithoutParameters()
        {
            Mock.Setup(() => TestStaticClass.TestVoidMethodWithoutParameters(), () =>
                {
                    Assert.Throws<Exception>(() => TestStaticClass.TestVoidMethodWithoutParameters());
                })
                .Throws(typeof(Exception));
        }

        [Test]
        public void TestThrowsTestVoidMethodWithParameters()
        {
            Mock.Setup(() => TestStaticClass.TestVoidMethodWithParameters(1), () =>
                {
                    Assert.Throws<Exception>(() => TestStaticClass.TestVoidMethodWithParameters(1));
                })
                .Throws(typeof(Exception));
        }

        [Test]
        public void TestThrowsArgumentNullException()
        {
            Mock.Setup(() => TestStaticClass.TestVoidMethodWithoutParameters(), () =>
                {
                    Assert.Throws<ArgumentNullException>(() => TestStaticClass.TestVoidMethodWithoutParameters());
                })
                .Throws(typeof(ArgumentNullException));
        }

        [Test]
        public void TestInstanceMethodThrows()
        {
            var testInstance = new TestInstance();

            Mock.Setup(typeof(TestInstance), nameof(TestInstance.TestMethodReturn1WithoutParameters), () =>
            {
                Assert.Throws<Exception>(() => testInstance.TestMethodReturn1WithoutParameters());
            }).Throws<Exception>();
        }

        [Test]
        public void TestGenericThrowsTestMethodReturnTask()
        {
            Mock.Setup(typeof(TestStaticClass), nameof(TestStaticClass.TestMethodReturnTask), () =>
            {
                Assert.ThrowsAsync<Exception>(() => TestStaticClass.TestMethodReturnTask());
            });
        }

        [Test]
        public void TestGenericThrowsTestMethodReturnTaskAsync()
        {
            Mock.Setup(typeof(TestStaticClass), nameof(TestStaticClass.TestMethodReturnTaskAsync), () =>
            {
                Assert.ThrowsAsync<Exception>(() => TestStaticClass.TestMethodReturnTaskAsync());
            });
        }

        [Test]
        public void TestGenericThrowsTestVoidMethodReturnTaskAsync()
        {
            Mock.SetupVoid(typeof(TestStaticClass), nameof(TestStaticClass.TestVoidMethodReturnTaskAsync), () =>
            {
                Assert.Throws<Exception>(() => TestStaticClass.TestVoidMethodReturnTaskAsync());
            });
        }

        [Test]
        public void TestGenericThrowsTestMethodReturnTaskWithoutParametersAsync()
        {
            Mock.Setup(typeof(TestStaticClass), nameof(TestStaticClass.TestMethodReturnTaskWithoutParametersAsync), () =>
            {
                Assert.ThrowsAsync<Exception>(() => TestStaticClass.TestMethodReturnTaskWithoutParametersAsync());
            });
        }

        [Test]
        public void TestGenericThrowsTestMethodReturnTaskWithoutParameters()
        {
            Mock.Setup(typeof(TestStaticClass), nameof(TestStaticClass.TestMethodReturnTaskWithoutParameters), () =>
            {
                Assert.ThrowsAsync<Exception>(() => TestStaticClass.TestMethodReturnTaskWithoutParameters());
            });
        }

        [Test]
        public void TestGenericThrowsInstanceTestMethodReturnTask()
        {
            var instance = new TestInstance();

            Mock.Setup(typeof(TestInstance), nameof(TestInstance.TestMethodReturnTask), () =>
            {
                Assert.ThrowsAsync<Exception>(() => instance.TestMethodReturnTask());
            });
        }

        [Test]
        public void TestGenericThrowsInstanceTestMethodReturnTaskAsync()
        {
            var instance = new TestInstance();

            Mock.Setup(typeof(TestInstance), nameof(TestInstance.TestMethodReturnTaskAsync), () =>
            {
                Assert.ThrowsAsync<Exception>(() => instance.TestMethodReturnTaskAsync());
            });
        }

        [Test]
        public void TestGenericThrowsInstanceTestVoidMethodReturnTaskAsync()
        {
            var instance = new TestInstance();

            Mock.SetupVoid(typeof(TestInstance), nameof(TestInstance.TestVoidMethodReturnTaskAsync), () =>
            {
                Assert.Throws<Exception>(() => instance.TestVoidMethodReturnTaskAsync());
            });
        }

        [Test]
        public void TestGenericThrowsInstanceTestMethodReturnTaskWithoutParametersAsync()
        {
            var instance = new TestInstance();

            Mock.Setup(typeof(TestInstance), nameof(TestInstance.TestMethodReturnTaskWithoutParametersAsync), () =>
            {
                Assert.ThrowsAsync<Exception>(() => instance.TestMethodReturnTaskWithoutParametersAsync());
            });
        }

        [Test]
        public void TestGenericThrowsInstanceTestMethodReturnTaskWithoutParameters()
        {
            var instance = new TestInstance();

            Mock.Setup(typeof(TestInstance), nameof(TestInstance.TestMethodReturnTaskWithoutParameters), () =>
            {
                Assert.ThrowsAsync<Exception>(() => instance.TestMethodReturnTaskWithoutParameters());
            });
        }

        [Test]
        public void TestThrowsStaticIntProperty()
        {
            var originalValue = TestStaticClass.StaticIntProperty;
            Assert.AreEqual(default(int), originalValue);

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
            Assert.AreEqual(default, originalValue);

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
            Assert.AreEqual(default(int), originalValue);

            Mock.SetupProperty(typeof(TestInstance), nameof(TestInstance.IntProperty), () =>
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
            Assert.AreEqual(default, originalValue);

            Mock.SetupProperty(typeof(TestInstance), nameof(TestInstance.ObjectProperty), () =>
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
            Mock.Setup(typeof(TestInstance), methodInfo.Name, BindingFlags.NonPublic | BindingFlags.Instance, () =>
            {
                try
                {
                    var actualResult = methodInfo.Invoke(testInstance, new object[] { });
                }catch(Exception e)
                {
                    Assert.AreEqual(typeof(Exception), e.InnerException.GetType());
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

            Mock.SetupProperty(typeof(TestInstance), propertyInfo.Name, BindingFlags.NonPublic | BindingFlags.Instance, () =>
            {
                try
                {
                    var actualResult = mothodInfo.Invoke(testInstance, new object[] { });
                }catch(Exception e)
                {
                    Assert.AreEqual(typeof(Exception), e.InnerException.GetType());
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

            Mock.SetupProperty(typeof(TestInstance), propertyInfo.Name, BindingFlags.NonPublic | BindingFlags.Instance, () =>
            {
                try
                {
                    var actualResult = mothodInfo.Invoke(testInstance, new object[] { });
                }catch(Exception e)
                {
                    Assert.AreEqual(typeof(Exception), e.InnerException.GetType());
                } 
            }).Throws(typeof(Exception));
        }

        [Test]
        public void TestThrowsTestPrivateMethodReturn1WithoutParameters()
        {

            Type type = typeof(TestStaticClass);
            MethodInfo methodInfo = type.GetMethod("TestPrivateStaticMethodReturn1WithoutParameters", BindingFlags.Static | BindingFlags.NonPublic);
            Mock.Setup(type,methodInfo.Name, BindingFlags.Static | BindingFlags.NonPublic, () =>
            {
                try
                {
                    int result = (int)methodInfo.Invoke(type, new object[] { });
                }
                catch (Exception e)
                {
                    Assert.AreEqual(typeof(Exception), e.InnerException.GetType());
                }
                
            }).Throws(typeof(Exception));
        }

        [Test]
        public void TestThrowsPrivateStaticIntProperty()
        {
            Type type = typeof(TestStaticClass);
            PropertyInfo propertyInfo = type.GetProperty("PrivateStaticIntProperty", BindingFlags.NonPublic | BindingFlags.Static);
            MethodInfo mothodInfo = propertyInfo.GetMethod;
            var originalValue = mothodInfo.Invoke(type,new object[] { });
            Assert.AreEqual(default(int), originalValue);

            Mock.SetupProperty(typeof(TestStaticClass), propertyInfo.Name, BindingFlags.NonPublic | BindingFlags.Static, () =>
            {
                try
                {
                    var actualResult = (int)mothodInfo.Invoke(type, new object[] { });
                }
                catch(Exception e)
                {
                    Assert.AreEqual(typeof(Exception), e.InnerException.GetType());
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
            Assert.AreEqual(default, originalValue);

            Mock.SetupProperty(typeof(TestStaticClass), propertyInfo.Name, BindingFlags.NonPublic | BindingFlags.Static, () =>
            {
                try
                {
                    var actualResult = (int)mothodInfo.Invoke(type, new object[] { });
                }
                catch (Exception e)
                {
                    Assert.AreEqual(typeof(Exception), e.InnerException.GetType());
                }
            }).Throws<Exception>();
        }

        [Test]
        public void TestSetupThrowsWithGenericTestMethodReturn1WithoutParameters()
        {
            var originalResult = TestStaticClass.GenericTestMethodReturnDefaultWithoutParameters<int>();
            Assert.AreEqual(0, originalResult);

            Mock.Setup(typeof(TestStaticClass), nameof(TestStaticClass.GenericTestMethodReturnDefaultWithoutParameters), new SetupProperties { GenericTypes = new []{ typeof(int) } },
                    () =>
                    {
                        Assert.Throws<Exception>(() => TestStaticClass.GenericTestMethodReturnDefaultWithoutParameters<int>());
                    })
                .Throws<Exception>();
        }

        [Test]
        public void TestSetupThrowsWithGenericTestMethodReturn1WithoutParametersInstance()
        {
            var testInstance = new TestInstance();
            var originalResult = testInstance.GenericTestMethodReturnDefaultWithoutParameters<int>();
            Assert.AreEqual(0, originalResult);

            Mock.Setup(typeof(TestInstance), nameof(TestInstance.GenericTestMethodReturnDefaultWithoutParameters), new SetupProperties { GenericTypes = new []{ typeof(int) } },
                    () =>
                    {
                        Assert.Throws<Exception>(() => testInstance.GenericTestMethodReturnDefaultWithoutParameters<int>());
                    })
                .Throws<Exception>();
        }
    }
}
