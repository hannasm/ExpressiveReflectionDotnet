using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExpressiveReflection.Tests
{
    [TestClass]
    public class MemberReflectionTests
    {
        [TestMethod]
        public void Test001()
        {
            var member = new MemberReflection();
            var result = member.From(()=>default(string).Length);

            Assert.AreEqual(
                typeof(string).GetMember("Length").Single(),
                result);
        }
        [TestMethod]
        public void Test002()
        {
            var member = new MemberReflection();
            var result = member.From(() => default(string)[0]);

            Assert.AreEqual(
                typeof(string).GetProperty("Chars"),
                result);
        }
        [TestMethod]
        public void Test003()
        {
            try {
                var member = new MemberReflection();
                var result = member.From(() => default(string[])[0]);

                Assert.Fail("Expected an exception to be thrown");
            }
            catch (InvalidExpressionException eError) {
                // there is not indexer property for arrays
            }
        }

        public class Dummy01 {
            public string this[int index]
            {
                get { return default(string); }
                set { }
            }
            public string this[string index]
            {
                get { return default(string); }
                set { }
            }
        }

        [TestMethod]
        public void Test004()
        {
            var member = new MemberReflection();
            var result = member.From(() => default(Dummy01)[default(int)]);

            Assert.AreEqual(
                typeof(Dummy01).GetProperty("Item", new Type[] { typeof(int) }),
                result);
        }

        [TestMethod]
        public void Test005()
        {
            var member = new MemberReflection();
            var result = member.From(() => default(Dummy01)[default(string)]);

            Assert.AreEqual(
                typeof(Dummy01).GetProperty("Item", new Type[] { typeof(string) }),
                result);
        }

        [TestMethod]
        public void Test006()
        {
            var member = new MemberReflection();
            // is there no way to generate an IndexExpression using a lambda?
            var result = member.From<string>(
                Expression.Lambda<Func<string>>(
                    Expression.MakeIndex(
                        Expression.Default(typeof(Dummy01)), 
                        typeof(Dummy01).GetProperty("Item", new Type[] { typeof(int) }), 
                        new Expression[] { Expression.Default(typeof(int)) }
                    )
                )
            );

            Assert.AreEqual(
                typeof(Dummy01).GetProperty("Item", new Type[] { typeof(int) }),
                result);
        }

        class Dummy02 {
            public readonly string ReadonlyField;
            public string ReadonlyProperty { get { return default(string); } }

            public string MutableField;
            public string MutableProperty { get { return default(string); } set { } }
        }

        [TestMethod]
        public void Test007()
        {
            var member = new MemberReflection();
            var result = member.IsReadOnly(typeof(Dummy02).GetMember("ReadonlyField")[0]);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test008()
        {
            var member = new MemberReflection();
            var result = member.IsReadOnly(typeof(Dummy02).GetMember("ReadonlyProperty")[0]);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test009()
        {
            var member = new MemberReflection();
            var result = member.IsReadOnly(typeof(Dummy02).GetMember("MutableField")[0]);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test010()
        {
            var member = new MemberReflection();
            var result = member.IsReadOnly(typeof(Dummy02).GetMember("MutableProperty")[0]);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test011()
        {
            var member = new MemberReflection();
            var result = member.IsReadOnly(typeof(string).GetMethod("Trim", new Type[] { }));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test012()
        {
            var member = new MemberReflection();
            var result = member.IsReadOnly(typeof(string).GetConstructors()[0]);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test013()
        {
            var member = new MemberReflection();
            var result = member.NameOf(()=>default(string).Length);

            Assert.AreEqual("Length", result);
        }

        [TestMethod]
        public void Test014()
        {
            var member = new MemberReflection();
            var result = member.NameOf(() => default(Version).MajorRevision);

            Assert.AreEqual("MajorRevision", result);
        }

        class Dummy03 {
            public int FieldOne;
        }
        [TestMethod]
        public void Test015()
        {
            var member = new MemberReflection();
            var result = member.NameOf(() => default(Dummy03).FieldOne);

            Assert.AreEqual("FieldOne", result);
        }


        [TestMethod]
        public void Test016()
        {
            var member = new MemberReflection();
            var result = member.NameOf(() => default(Dummy01)[default(int)]);

            Assert.AreEqual("Item", result);
        }

        [TestMethod]
        public void Test017()
        {
            var member = new MemberReflection();
            var result = member.NameOf(() => default(Dummy01)[default(string)]);

            Assert.AreEqual("Item", result);
        }
    }
}
