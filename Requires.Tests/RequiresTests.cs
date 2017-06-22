namespace Requires
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public sealed class RequiresTests
    {
        public RequiresTests()
        {
        }

        [TestMethod]
        public void Requires_NotNull_ExceptionParamName_Is_ParameterName()
        {
            Action<object> testAction = (object foobar) =>
            {
                Requires.NotNull(() => foobar);
            };

            try
            {
                testAction(null);
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("foobar", ex.ParamName);
            }
        }

        [TestMethod]
        public void Requires_NotNull_ExceptionMessage_Contains_ParameterName()
        {
            Action<object> testAction = (object foobar) =>
            {
                Requires.NotNull(() => foobar);
            };

            try
            {
                testAction(null);
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.Message.Contains("foobar"));
            }
        }

        [TestMethod]
        public void Requires_NotNull_Test1()
        {
            Action<object> testAction = (object o) =>
            {
                Requires.NotNull(() => o);
            };

            testAction(new object());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Requires_NotNull_WithNull_ThrowsArgumentNullException()
        {
            Action<object> testAction = (object o) =>
            {
                Requires.NotNull(() => o);
            };

            testAction(null);
        }

        [TestMethod]
        public void Requires_NotNullOrEmpty_Test1()
        {
            Action<string> testAction = (string str) =>
            {
                Requires.NotNullOrEmpty(() => str);
            };

            testAction("hello, world.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Requires_NotNullOrEmpty_WithNull_ThrowsArgumentNullException()
        {
            Action<string> testAction = (string str) =>
            {
                Requires.NotNullOrEmpty(() => str);
            };

            testAction(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Requires_NotNullOrEmpty_WithEmpty_ThrowsArgumentException()
        {
            Action<string> testAction = (string str) =>
            {
                Requires.NotNullOrEmpty(() => str);
            };

            testAction(String.Empty);
        }

        [TestMethod]
        public void Requires_Require_Test1()
        {
            Action<int> testAction = (int i) =>
            {
                NameValueCombinationInitial<int> nvc = Requires.Require(() => i);
                Assert.AreEqual("i", nvc.Name);
                Assert.AreEqual(42, nvc.Value);
            };

            testAction(42);
        }

        [TestMethod]
        public void Requires_GreaterThan_Test1()
        {
            Action<int, int> testAction = (int x, int y) =>
            {
                Requires.Require(() => x).GreaterThan(() => y);
            };

            testAction(2, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Requires_GreaterThan_NotGreater_ThrowsArgumentOutOfRangeException()
        {
            Action<int, int> testAction = (int x, int y) =>
            {
                Requires.Require(() => x).GreaterThan(() => y);
            };

            testAction(1, 2);
        }

        [TestMethod]
        public void Requires_GreaterThan_ExceptionMessage()
        {
            Action<int, int> testAction = (int x, int y) =>
            {
                Requires.Require(() => x).GreaterThan(() => y);
            };

            try
            {
                testAction(1, 2);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.IsTrue(ex.Message.Contains("\"x\" (actual value: 1) must be greater than \"y\" (actual value: 2)."));
            }
        }

        [TestMethod]
        public void Requires_GreaterThan_ExceptionParamName_Is_ParameterName()
        {
            Action<int, int> testAction = (int x, int y) =>
            {
                Requires.Require(() => x).GreaterThan(() => y);
            };

            try
            {
                testAction(1, 2);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.AreEqual("x", ex.ParamName);
            }
        }

        [TestMethod]
        public void Requires_GreaterThan_ExceptionActualValue_Is_ActualValue()
        {
            Action<int, int> testAction = (int x, int y) =>
            {
                Requires.Require(() => x).GreaterThan(() => y);
            };

            try
            {
                testAction(1, 2);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.AreEqual(1, ex.ActualValue);
            }
        }

        [TestMethod]
        public void Requires_AndLessThan_Test1()
        {
            Action<int, int, int> testAction = (int x, int y, int z) =>
            {
                Requires.Require(() => x).GreaterThan(() => y).AndLessThan(() => z);
            };

            testAction(2, 1, 3);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Requires_AndLessThan_NotLessThan_ThrowsArgumentOutOfRangeException()
        {
            Action<int, int, int> testAction = (int x, int y, int z) =>
            {
                Requires.Require(() => x).GreaterThan(() => y).AndLessThan(() => z);
            };

            testAction(2, 1, 0);
        }

        [TestMethod]
        public void Requires_AndLessThan_ExceptionMessage()
        {
            Action<int, int, int> testAction = (int x, int y, int z) =>
            {
                Requires.Require(() => x).GreaterThan(() => y).AndLessThan(() => z);
            };

            try
            {
                testAction(2, 1, 0);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.IsTrue(ex.Message.Contains("\"x\" (actual value: 2) must be less than \"z\" (actual value: 0)."));
            }
        }

        [TestMethod]
        public void Requires_AndLessThan_ExceptionParamName_Is_ParameterName()
        {
            Action<int, int, int> testAction = (int x, int y, int z) =>
            {
                Requires.Require(() => x).GreaterThan(() => y).AndLessThan(() => z);
            };

            try
            {
                testAction(2, 1, 0);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.AreEqual("x", ex.ParamName);
            }
        }

        [TestMethod]
        public void Requires_AndLessThan_ExceptionActualValue_Is_ActualValue()
        {
            Action<int, int, int> testAction = (int x, int y, int z) =>
            {
                Requires.Require(() => x).GreaterThan(() => y).AndLessThan(() => z);
            };

            try
            {
                testAction(2, 1, 0);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.AreEqual(2, ex.ActualValue);
            }
        }

    }
}
