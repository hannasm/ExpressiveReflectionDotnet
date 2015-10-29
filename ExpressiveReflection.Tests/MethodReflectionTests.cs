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



    }
}
