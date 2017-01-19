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
            private Dictionary<int, string> _dataOne = new Dictionary<int, string>();
            private Dictionary<string, string> _dataTwo = new Dictionary<string, string>();
            public string this[int index]
            {
                get { return _dataOne[index]; }
                set { _dataOne[index] = value; }
            }
            public string this[string index]
            {
                get { return _dataTwo[index]; }
                set { _dataTwo[index] = value; }
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
            public readonly string ReadonlyField = "REAODNLYFIELD";
            public string ReadonlyProperty { get { return "READONLYPROPERTY"; } }

            public string MutableField;
            public string MutableProperty { get; set; }
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

        [TestMethod]
        public void Test018()
        {
            var member = new MemberReflection();
            var mi = member.From(()=>default(Dummy02).MutableField);
            var instance = new Dummy02 {
                MutableField = "ABC",
                MutableProperty = "DEF",
            };

            Assert.AreEqual(instance.MutableField, member.GetValue<string>(mi, instance));
        }

        [TestMethod]
        public void Test019()
        {
            var member = new MemberReflection();
            var mi = member.From(() => default(Dummy02).MutableProperty);
            var instance = new Dummy02
            {
                MutableField = "ABC",
                MutableProperty = "DEF",
            };

            Assert.AreEqual(instance.MutableProperty, member.GetValue<string>(mi, instance));
        }

        [TestMethod]
        public void Test020()
        {
            var member = new MemberReflection();
            var mi = member.From(() => default(Dummy02).ReadonlyField);
            var instance = new Dummy02
            {
                MutableField = "ABC",
                MutableProperty = "DEF",
            };

            Assert.AreEqual(instance.ReadonlyField, member.GetValue<string>(mi, instance));
        }

        [TestMethod]
        public void Test021()
        {
            var member = new MemberReflection();
            var mi = member.From(() => default(Dummy02).ReadonlyProperty);
            var instance = new Dummy02
            {
                MutableField = "ABC",
                MutableProperty = "DEF",
            };

            Assert.AreEqual(instance.ReadonlyProperty, member.GetValue<string>(mi, instance));
        }

        [TestMethod]
        public void Test022()
        {
            var member = new MemberReflection();
            var mi = member.From(() => default(Dummy02).MutableField);
            var instance = new Dummy02
            {
                MutableField = "ABC",
                MutableProperty = "DEF",
            };

            Assert.AreEqual(instance.MutableField, "ABC");
            member.SetValue(mi, instance, "GHI");
            Assert.AreEqual(instance.MutableField, "GHI");
        }

        [TestMethod]
        public void Test023()
        {
            var member = new MemberReflection();
            var mi = member.From(() => default(Dummy02).MutableProperty);
            var instance = new Dummy02
            {
                MutableField = "ABC",
                MutableProperty = "DEF",
            };

            Assert.AreEqual(instance.MutableProperty, "DEF");
            member.SetValue(mi, instance, "GHI");
            Assert.AreEqual(instance.MutableProperty, "GHI");
        }


        [TestMethod]
        public void Test024()
        {
            var member = new MemberReflection();
            var mi = member.From(() => default(Dummy01)[default(int)]);
            var instance = new Dummy01();
            instance[1] = "ABC";

            Assert.AreEqual(member.GetValue(mi, instance, 1), "ABC");
        }

        [TestMethod]
        public void Test025()
        {
            var member = new MemberReflection();
            var mi = member.From(() => default(Dummy01)[default(string)]);
            var instance = new Dummy01();
            instance["DEF"] = "ABC";

            Assert.AreEqual(member.GetValue(mi, instance, "DEF"), "ABC");
        }
        [TestMethod]
        public void Test026()
        {
            var member = new MemberReflection();
            var mi = member.From(() => default(Dummy01)[default(int)]);
            var instance = new Dummy01();
            instance[1] = "ABC";

            Assert.AreEqual(instance[1], "ABC");

            member.SetValue(mi, instance, "DEF", 1);

            Assert.AreEqual(instance[1], "DEF");
        }

        [TestMethod]
        public void Test027()
        {
            var member = new MemberReflection();
            var mi = member.From(() => default(Dummy01)[default(string)]);
            var instance = new Dummy01();
            instance["DEF"] = "ABC";

            Assert.AreEqual(instance["DEF"], "ABC");

            member.SetValue(mi, instance, "GHI", "DEF");

            Assert.AreEqual(instance["DEF"], "GHI");
        }

        class TransmuteTestClass001<T>
        {
            public T GenericField;
            public T GenericProperty { get; set; }
            public string StringFIeld;
            public string StringProperty { get; set; }
        }

        [TestMethod]
        public void Test030()
        {
            var member = new MemberReflection();
            var mi = member.From(() => default(TransmuteTestClass001<int>).GenericField);
            var newMi = member.Transmute(mi, typeof(Guid));
            var expectedMi = member.From(() => default(TransmuteTestClass001<Guid>).GenericField);

            Assert.AreEqual(expectedMi, newMi);
        }
        [TestMethod]
        public void Test031()
        {
            var member = new MemberReflection();
            var mi = member.From(() => default(TransmuteTestClass001<int>).GenericProperty);
            var newMi = member.Transmute(mi, typeof(Guid));
            var expectedMi = member.From(() => default(TransmuteTestClass001<Guid>).GenericProperty);

            Assert.AreEqual(expectedMi, newMi);
        }
        [TestMethod]
        public void Test032()
        {
            var member = new MemberReflection();
            var mi = member.From(() => default(TransmuteTestClass001<int>).StringFIeld);
            var newMi = member.Transmute(mi, typeof(Guid));
            var expectedMi = member.From(() => default(TransmuteTestClass001<Guid>).StringFIeld);

            Assert.AreEqual(expectedMi, newMi);
        }
        [TestMethod]
        public void Test033()
        {
            var member = new MemberReflection();
            var mi = member.From(() => default(TransmuteTestClass001<int>).StringProperty);
            var newMi = member.Transmute(mi, typeof(Guid));
            var expectedMi = member.From(() => default(TransmuteTestClass001<Guid>).StringProperty);

            Assert.AreEqual(expectedMi, newMi);
        }
    }
}
