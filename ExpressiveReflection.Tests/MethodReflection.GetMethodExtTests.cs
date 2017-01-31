using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressiveReflection.Tests
{
	[TestClass]
    public class MethodReflection_GetMethodExtTests
    {
		static class GenericOverloads
        {
			public static T1 Get<T1,T2>(T2 val) { return default(T1); }
            public static T1 Get<T1, T2, T3>(T2 val, T3 val2) { return default(T1); }
            public static T1 Get<T1, T2, T3, T4>(T2 val, T3 val2, T4 val3) { return default(T1); }

            public static T1 ArrayGet<T1, T2>(T2[] val) { return default(T1); }
            public static T1 ArrayGet<T1, T2, T3>(T2[] val, T3 val2) { return default(T1); }
            public static T1 ArrayGet<T1, T2, T3, T4>(T2[] val, T3 val2, T4 val3) { return default(T1); }
        }
		[TestMethod]
		public void Test001()
        {
            var m1 = Reflection.GetMethod(() => GenericOverloads.Get<string, string, string>(default(string), default(string)));
            var m2 = Reflection.GetMethodExt(typeof(GenericOverloads), nameof(GenericOverloads.Get), new[] { typeof(MethodReflection.T), typeof(MethodReflection.T) });

            Assert.AreEqual(m1, m2.MakeGenericMethod(typeof(string), typeof(string), typeof(string)));
        }
        [TestMethod]
        public void Test002()
        {
            var m1 = Reflection.GetMethod(() => GenericOverloads.Get<string, string>(default(string)));
            var m2 = Reflection.GetMethodExt(typeof(GenericOverloads), nameof(GenericOverloads.Get), new[] { typeof(MethodReflection.T) });

            Assert.AreEqual(m1, m2.MakeGenericMethod(typeof(string), typeof(string)));
        }
        [TestMethod]
        public void Test003()
        {
            var m1 = Reflection.GetMethod(() => GenericOverloads.ArrayGet<string, string, string>(default(string[]), default(string)));
            var m2 = Reflection.GetMethodExt(typeof(GenericOverloads), nameof(GenericOverloads.ArrayGet), new[] { typeof(MethodReflection.T[]), typeof(MethodReflection.T) });

            Assert.AreEqual(m1, m2.MakeGenericMethod(typeof(string), typeof(string), typeof(string)));
        }
        [TestMethod]
        public void Test004()
        {
            var m1 = Reflection.GetMethod(() => GenericOverloads.ArrayGet<string, string>(default(string[])));
            var m2 = Reflection.GetMethodExt(typeof(GenericOverloads), nameof(GenericOverloads.ArrayGet), new[] { typeof(MethodReflection.T[]) });

            Assert.AreEqual(m1, m2.MakeGenericMethod(typeof(string), typeof(string)));
        }
    }
}
