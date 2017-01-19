using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ExpressiveReflection.Tests
{
    [TestClass]
    public class ConstructorReflectionTests
    {
        [TestMethod]
        public void Test001()
        {
            var constructor = new ConstructorReflection();
            var result = constructor.From(()=>new string(default(char[])));

            Assert.AreEqual(typeof(string).GetConstructor(new Type[] { typeof(char[]) }), result);
        }

        [TestMethod]
        public void Test002()
        {
            var constructor = new ConstructorReflection();
            var result = constructor.From(() => new string(default(char[]), default(int), default(int)));

            Assert.AreEqual(typeof(string).GetConstructor(new Type[] { typeof(char[]), typeof(int) , typeof(int) }), result); 
        }

        [TestMethod]
        public void Test003()
        {
            var constructor = new ConstructorReflection();
            var result = constructor.From(() => new List<string> { default(string), default(string) });

            Assert.AreEqual(typeof(List<string>).GetConstructor(new Type[] { }), result);
        }

        [TestMethod]
        public void Test004()
        {
            var constructor = new ConstructorReflection();
            var result = constructor.From(() => new {
                A = default(string),
                B = default(string)
            });

            var type = (new { A = default(string), B=default(string) }).GetType();
            Assert.AreEqual(type.GetConstructor(new Type[] { typeof(string), typeof(string) }), result);
        }

        public class Dummy {
            public string A;
            public string B;
        }
        [TestMethod]
        public void Test005()
        {
            var constructor = new ConstructorReflection();
            var result = constructor.From(() => new Dummy {
                A = default(string),
                B = default(string)
            }); 
            
            Assert.AreEqual(typeof(Dummy).GetConstructor(new Type[] { }), result);
        }
        [TestMethod]
        public void Test006()
        {
            var constructor = new ConstructorReflection();
            var result = constructor.From(() => new string[0]);

            Assert.AreEqual(typeof(string[]).GetConstructor(new Type[] { typeof(int) }), result);
        }
        [TestMethod]
        public void Test007()
        {
            var constructor = new ConstructorReflection();
            var result = constructor.From(() => new string[1]);

            Assert.AreEqual(typeof(string[]).GetConstructor(new Type[] { typeof(int) }), result);
        }
        [TestMethod]
        public void Test008()
        {
            var constructor = new ConstructorReflection();
            var result = constructor.From(() => new string[2]);

            Assert.AreEqual(typeof(string[]).GetConstructor(new Type[] { typeof(int) }), result);
        }

        [TestMethod]
        public void Test009()
        {
            var constructor = new ConstructorReflection();
            var result = constructor.From(() => new string[] { });

            Assert.AreEqual(typeof(string[]).GetConstructor(new Type[] { typeof(int) }), result);
        }
        [TestMethod]
        public void Test010()
        {
            var constructor = new ConstructorReflection();
            var result = constructor.From(() => new string[] { default(string) });

            Assert.AreEqual(typeof(string[]).GetConstructor(new Type[] { typeof(int) }), result);
        }
        [TestMethod]
        public void Test011()
        {
            var constructor = new ConstructorReflection();
            var result = constructor.From(() => new string[] { default(string), default(string) });

            Assert.AreEqual(typeof(string[]).GetConstructor(new Type[] { typeof(int) }), result);
        }

        class GenericConstructorTarget<T>
        {
            public GenericConstructorTarget(string someString, int someInt, T someT)
            {
            }
            public GenericConstructorTarget(int someInt, string someString, T someT)
            {
            }
        }
        [TestMethod]
        public void Test012()
        {
            var constructor = new ConstructorReflection();
            var result = constructor.From(() => new GenericConstructorTarget<string>(default(string), default(int), default(string)));
            result = constructor.Transmute(result, typeof(long));

            var parm = result.GetParameters();
            Assert.AreEqual(typeof(string), parm[0].ParameterType);
            Assert.AreEqual(typeof(int), parm[1].ParameterType);
            Assert.AreEqual(typeof(long), parm[2].ParameterType);
        }
        [TestMethod]
        public void Test013()
        {
            var constructor = new ConstructorReflection();
            var result = constructor.From(() => new GenericConstructorTarget<string>(default(int), default(string), default(string)));
            result = constructor.Transmute(result, typeof(long));

            var parm = result.GetParameters();
            Assert.AreEqual(typeof(int), parm[0].ParameterType);
            Assert.AreEqual(typeof(string), parm[1].ParameterType);
            Assert.AreEqual(typeof(long), parm[2].ParameterType);
        }
    }
}
