using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressiveReflection.Tests
{
    [TestClass]
    public class MethodReflectionTests
    {
        [TestMethod]
        public void Test001()
        {
            var method = new MethodReflection();
            var result = method.From(() => default(string).TrimEnd());

            Assert.AreEqual(typeof(string).GetMethod("TrimEnd"), result);
        }
        [TestMethod]
        public void Test001Name()
        {
            var method = new MethodReflection();
            var result = method.NameOf(() => default(string).TrimEnd());

            Assert.AreEqual("TrimEnd", result);
        }

        [TestMethod]
        public void Test002()
        {
            var method = new MethodReflection();
            var result = method.From(() => default(string).IndexOf(default(char)));

            Assert.AreEqual(typeof(string).GetMethod("IndexOf", new Type[] { typeof(char) }), result);
        }
        [TestMethod]
        public void Test002Name()
        {
            var method = new MethodReflection();
            var result = method.NameOf(() => default(string).IndexOf(default(char)));

            Assert.AreEqual("IndexOf", result);
        }

        [TestMethod]
        public void Test003()
        {
            var method = new MethodReflection();
            var result = method.From(() => default(string).IndexOf(default(char), default(int)));

            Assert.AreEqual(typeof(string).GetMethod("IndexOf", new Type[] { typeof(char) , typeof(int) }), result);
        }

        [TestMethod]
        public void Test003Name()
        {
            var method = new MethodReflection();
            var result = method.NameOf(() => default(string).IndexOf(default(char), default(int)));

            Assert.AreEqual("IndexOf", result);
        }
        string GetStringNoOptimization() {
            return default(string);
        }
        [TestMethod]
        public void Test004()
        {
            var method = new MethodReflection();
            var result = method.From(() => GetStringNoOptimization() == GetStringNoOptimization());

            Assert.AreEqual(typeof(string).GetMethod("op_Equality", new Type[] { typeof(string), typeof(string) }), result);
        }
        [TestMethod]
        public void Test004Name()
        {
            var method = new MethodReflection();
            var result = method.NameOf(() => GetStringNoOptimization() == GetStringNoOptimization());

            Assert.AreEqual("op_Equality", result);
        }

        int GetIntNoOptimization()
        {
            return default(int);
        }
        [TestMethod]
        public void Test005()
        {
            var method = new MethodReflection();
            var result = method.From(() => (decimal)GetIntNoOptimization());

            Assert.AreEqual(typeof(decimal).GetMethod("op_Implicit", new Type[] { typeof(int) }), result);
        }
        [TestMethod]
        public void Test005Name()
        {
            var method = new MethodReflection();
            var result = method.NameOf(() => (decimal)GetIntNoOptimization());

            Assert.AreEqual("op_Implicit", result);
        }


        public class TransmuteMethodTarget<T>
        {
            public void TransmuteTestMethod01(T val) { }

            public void TransmuteTestMethod02(string s) { }
            public void TransmuteTestMethod02(T s) { }

            public void TransmuteTestMethod03<T1>(T1 val) { }
            public void TransmuteTestMethod03<T1, T2>(T1 val1, T2 val2) { }
        }

        [TestMethod]
        public void Test006()
        {
            var method = new MethodReflection();
            var result = method.From(() => default(TransmuteMethodTarget<string>).TransmuteTestMethod01(default(string)));

            var newResult = method.Transmute(result, new Type[] { typeof(long) }, null);
            
            var miToAssert = method.From(() => default(TransmuteMethodTarget<long>).TransmuteTestMethod01(default(long)));

            Assert.AreEqual(miToAssert, newResult);
        }
        [TestMethod]
        public void Test007()
        {
            var method = new MethodReflection();
            var result = method.From(() => default(TransmuteMethodTarget<double>).TransmuteTestMethod02(default(string)));

            var newResult = method.Transmute(result, new Type[] { typeof(long) }, null);

            var miToAssert = method.From(() => default(TransmuteMethodTarget<long>).TransmuteTestMethod02(default(string)));

            Assert.AreEqual(miToAssert, newResult);
        }
        [TestMethod]
        public void Test008()
        {
            var method = new MethodReflection();
            var result = method.From(() => default(TransmuteMethodTarget<double>).TransmuteTestMethod02(default(double)));

            var newResult = method.Transmute(result, new Type[] { typeof(long) }, null);

            var miToAssert = method.From(() => default(TransmuteMethodTarget<long>).TransmuteTestMethod02(default(long)));

            Assert.AreEqual(miToAssert, newResult);
        }
        [TestMethod]
        public void Test009()
        {
            var method = new MethodReflection();
            var result = method.From(() => default(TransmuteMethodTarget<double>).TransmuteTestMethod03<decimal>(default(decimal)));

            var newResult = method.Transmute(result, new Type[] { typeof(long) }, new Type[] { typeof(string) });

            var miToAssert = method.From(() => default(TransmuteMethodTarget<long>).TransmuteTestMethod03<string>(default(string)));

            Assert.AreEqual(miToAssert, newResult);
        }
        [TestMethod]
        public void Test010()
        {
            var method = new MethodReflection();
            var result = method.From(() => default(TransmuteMethodTarget<double>).TransmuteTestMethod03<decimal, Guid>(default(decimal), default(Guid)));

            var newResult = method.Transmute(result, new Type[] { typeof(long) }, new Type[] { typeof(string), typeof(bool) });

            var miToAssert = method.From(() => default(TransmuteMethodTarget<long>).TransmuteTestMethod03<string, bool>(default(string), default(bool)));

            Assert.AreEqual(miToAssert, newResult);
        }
        [TestMethod]
        public void Test011()
        {
            var method = new MethodReflection();
            var name = method.NameOf(() => default(TransmuteMethodTarget<string>).TransmuteTestMethod01(default(string)));

            Assert.AreEqual("TransmuteTestMethod01", name);
        }


    }
}
