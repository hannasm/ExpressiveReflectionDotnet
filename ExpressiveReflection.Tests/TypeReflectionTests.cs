using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressiveReflection.Tests
{
    [TestClass]
    public class TypeReflectionTests
    {
        [TestMethod]
        public void Test001()
        {
            var type = new TypeReflection();
            var result = type.From(() => "x" == "y");

            Assert.AreEqual(typeof(bool), result);
        }
        [TestMethod]
        public void Test002()
        {
            var type = new TypeReflection();
            var result = type.From(() => 1 + 3.4M);

            Assert.AreEqual(typeof(decimal), result);
        }

        [TestMethod]
        public void Test003()
        {
            var type = new TypeReflection();
            var result = type.From(() => new
            {
                A = 1,
                B = 2
            });

            Assert.AreEqual((new
            {
                A = 1,
                B = 2
            }).GetType(), result);
        }

        [TestMethod]
        public void Test004()
        {
            var type = new TypeReflection();

            Assert.IsTrue(type.IsNullableType(typeof(long?)));
        }
        [TestMethod]
        public void Test005()
        {
            var type = new TypeReflection();

            Assert.IsTrue(type.IsNullableType(typeof(int?)));
        }
        [TestMethod]
        public void Test006()
        {
            var type = new TypeReflection();

            Assert.IsFalse(type.IsNullableType(typeof(string)));
        }
        [TestMethod]
        public void Test007()
        {
            var type = new TypeReflection();

            Assert.IsFalse(type.IsNullableType(typeof(int)));
        }


        [TestMethod]
        public void Test008()
        {
            var type = new TypeReflection();

            Assert.IsTrue(type.IsNullAssignable(typeof(long?)));
        }
        [TestMethod]
        public void Test009()
        {
            var type = new TypeReflection();

            Assert.IsTrue(type.IsNullAssignable(typeof(int?)));
        }
        [TestMethod]
        public void Test010()
        {
            var type = new TypeReflection();

            Assert.IsTrue(type.IsNullAssignable(typeof(string)));
        }
        [TestMethod]
        public void Test011()
        {
            var type = new TypeReflection();

            Assert.IsFalse(type.IsNullAssignable(typeof(int)));
        }

        [TestMethod]
        public void Test012()
        {
            var type = new TypeReflection();
            var result = type.MadeNonNullable(typeof(int?));

            Assert.AreEqual(typeof(int), result);
        }
        [TestMethod]
        public void Test013()
        {
            var type = new TypeReflection();
            var result = type.MadeNonNullable(typeof(int));

            Assert.AreEqual(typeof(int), result);
        }
        [TestMethod]
        public void Test014()
        {
            var type = new TypeReflection();
            var result = type.MadeNonNullable(typeof(string));

            Assert.AreEqual(typeof(string), result);
        }

        [TestMethod]
        public void Test015()
        {
            var type = new TypeReflection();
            var result = type.MadeNullAssignable(typeof(int?));

            Assert.AreEqual(typeof(int?), result);
        }
        [TestMethod]
        public void Test016()
        {
            var type = new TypeReflection();
            var result = type.MadeNullAssignable(typeof(int));

            Assert.AreEqual(typeof(int?), result);
        }
        [TestMethod]
        public void Test017()
        {
            var type = new TypeReflection();
            var result = type.MadeNullAssignable(typeof(string));

            Assert.AreEqual(typeof(string), result);
        }

        [TestMethod]
        public void Test018()
        {
            var type = new TypeReflection();
            var result = type.GetDefaultValue(typeof(int));

            Assert.AreEqual(0, result);
        }
        [TestMethod]
        public void Test019()
        {
            var type = new TypeReflection();
            var result = type.GetDefaultValue(typeof(int?));

            Assert.AreEqual(null, result);
        }
        [TestMethod]
        public void Test020()
        {
            var type = new TypeReflection();
            var result = type.GetDefaultValue(typeof(string));

            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void Test021()
        {
            var type = new TypeReflection();
            Assert.IsTrue(type.IsInteger(typeof(short)));
        }
        [TestMethod]
        public void Test022()
        {
            var type = new TypeReflection();
            Assert.IsTrue(type.IsInteger(typeof(byte)));
        }
        [TestMethod]
        public void Test023()
        {
            var type = new TypeReflection();
            Assert.IsFalse(type.IsInteger(typeof(char)));
        }
        [TestMethod]
        public void Test024()
        {
            var type = new TypeReflection();
            Assert.IsTrue(type.IsInteger(typeof(int)));
        }
        [TestMethod]
        public void Test025()
        {
            var type = new TypeReflection();
            Assert.IsTrue(type.IsInteger(typeof(long)));
        }
        [TestMethod]
        public void Test026()
        {
            var type = new TypeReflection();
            Assert.IsTrue(type.IsInteger(typeof(ushort)));
        }
        [TestMethod]
        public void Test027()
        {
            var type = new TypeReflection();
            Assert.IsTrue(type.IsInteger(typeof(sbyte)));
        }
        [TestMethod]
        public void Test028()
        {
            var type = new TypeReflection();
            Assert.IsTrue(type.IsInteger(typeof(uint)));
        }
        [TestMethod]
        public void Test029()
        {
            var type = new TypeReflection();
            Assert.IsTrue(type.IsInteger(typeof(ulong)));
        }


        [TestMethod]
        public void Test031()
        {
            var type = new TypeReflection();
            Assert.IsTrue(type.IsInteger(typeof(short?)));
        }
        [TestMethod]
        public void Test032()
        {
            var type = new TypeReflection();
            Assert.IsTrue(type.IsInteger(typeof(byte?)));
        }
        [TestMethod]
        public void Test033()
        {
            var type = new TypeReflection();
            Assert.IsFalse(type.IsInteger(typeof(char?)));
        }
        [TestMethod]
        public void Test034()
        {
            var type = new TypeReflection();
            Assert.IsTrue(type.IsInteger(typeof(int?)));
        }
        [TestMethod]
        public void Test035()
        {
            var type = new TypeReflection();
            Assert.IsTrue(type.IsInteger(typeof(long?)));
        }
        [TestMethod]
        public void Test036()
        {
            var type = new TypeReflection();
            Assert.IsTrue(type.IsInteger(typeof(ushort?)));
        }
        [TestMethod]
        public void Test037()
        {
            var type = new TypeReflection();
            Assert.IsTrue(type.IsInteger(typeof(sbyte?)));
        }
        [TestMethod]
        public void Test038()
        {
            var type = new TypeReflection();
            Assert.IsTrue(type.IsInteger(typeof(uint?)));
        }
        [TestMethod]
        public void Test039()
        {
            var type = new TypeReflection();
            Assert.IsTrue(type.IsInteger(typeof(ulong?)));
        }

        [TestMethod]
        public void Test040()
        {
            var type = new TypeReflection();
            Assert.IsTrue(type.IsDecimal(typeof(float)));
        }

        [TestMethod]
        public void Test041()
        {
            var type = new TypeReflection();
            Assert.IsTrue(type.IsDecimal(typeof(double)));
        }

        [TestMethod]
        public void Test042()
        {
            var type = new TypeReflection();
            Assert.IsTrue(type.IsDecimal(typeof(decimal)));
        }

        [TestMethod]
        public void Test043()
        {
            var type = new TypeReflection();
            Assert.IsTrue(type.IsDecimal(typeof(float?)));
        }

        [TestMethod]
        public void Test044()
        {
            var type = new TypeReflection();
            Assert.IsTrue(type.IsDecimal(typeof(double?)));
        }

        [TestMethod]
        public void Test045()
        {
            var type = new TypeReflection();
            Assert.IsTrue(type.IsDecimal(typeof(decimal?)));
        }

        [TestMethod]
        public void Test046()
        {
            var type = new TypeReflection();
            Assert.IsFalse(type.IsDecimal(typeof(string)));
        }


        [TestMethod]
        public void Test051()
        {
            var type = new TypeReflection();
            Assert.IsFalse(type.IsDecimal(typeof(short)));
        }
        [TestMethod]
        public void Test052()
        {
            var type = new TypeReflection();
            Assert.IsFalse(type.IsDecimal(typeof(byte)));
        }
        [TestMethod]
        public void Test053()
        {
            var type = new TypeReflection();
            Assert.IsFalse(type.IsDecimal(typeof(char)));
        }
        [TestMethod]
        public void Test054()
        {
            var type = new TypeReflection();
            Assert.IsFalse(type.IsDecimal(typeof(int)));
        }
        [TestMethod]
        public void Test055()
        {
            var type = new TypeReflection();
            Assert.IsFalse(type.IsDecimal(typeof(long)));
        }
        [TestMethod]
        public void Test056()
        {
            var type = new TypeReflection();
            Assert.IsFalse(type.IsDecimal(typeof(ushort)));
        }
        [TestMethod]
        public void Test057()
        {
            var type = new TypeReflection();
            Assert.IsFalse(type.IsDecimal(typeof(sbyte)));
        }
        [TestMethod]
        public void Test058()
        {
            var type = new TypeReflection();
            Assert.IsFalse(type.IsDecimal(typeof(uint)));
        }
        [TestMethod]
        public void Test059()
        {
            var type = new TypeReflection();
            Assert.IsFalse(type.IsDecimal(typeof(ulong)));
        }


        [TestMethod]
        public void Test061()
        {
            var type = new TypeReflection();
            Assert.IsFalse(type.IsDecimal(typeof(short?)));
        }
        [TestMethod]
        public void Test062()
        {
            var type = new TypeReflection();
            Assert.IsFalse(type.IsDecimal(typeof(byte?)));
        }
        [TestMethod]
        public void Test063()
        {
            var type = new TypeReflection();
            Assert.IsFalse(type.IsDecimal(typeof(char?)));
        }
        [TestMethod]
        public void Test064()
        {
            var type = new TypeReflection();
            Assert.IsFalse(type.IsDecimal(typeof(int?)));
        }
        [TestMethod]
        public void Test065()
        {
            var type = new TypeReflection();
            Assert.IsFalse(type.IsDecimal(typeof(long?)));
        }
        [TestMethod]
        public void Test066()
        {
            var type = new TypeReflection();
            Assert.IsFalse(type.IsDecimal(typeof(ushort?)));
        }
        [TestMethod]
        public void Test067()
        {
            var type = new TypeReflection();
            Assert.IsFalse(type.IsDecimal(typeof(sbyte?)));
        }
        [TestMethod]
        public void Test068()
        {
            var type = new TypeReflection();
            Assert.IsFalse(type.IsDecimal(typeof(uint?)));
        }
        [TestMethod]
        public void Test069()
        {
            var type = new TypeReflection();
            Assert.IsFalse(type.IsDecimal(typeof(ulong?)));
        }

        [TestMethod]
        public void Test070()
        {
            var type = new TypeReflection();
            Assert.IsFalse(type.IsInteger(typeof(float)));
        }

        [TestMethod]
        public void Test071()
        {
            var type = new TypeReflection();
            Assert.IsFalse(type.IsInteger(typeof(double)));
        }

        [TestMethod]
        public void Test072()
        {
            var type = new TypeReflection();
            Assert.IsFalse(type.IsInteger(typeof(decimal)));
        }

        [TestMethod]
        public void Test073()
        {
            var type = new TypeReflection();
            Assert.IsFalse(type.IsInteger(typeof(float?)));
        }

        [TestMethod]
        public void Test074()
        {
            var type = new TypeReflection();
            Assert.IsFalse(type.IsInteger(typeof(double?)));
        }

        [TestMethod]
        public void Test075()
        {
            var type = new TypeReflection();
            Assert.IsFalse(type.IsInteger(typeof(decimal?)));
        }

        [TestMethod]
        public void Test076()
        {
            var type = new TypeReflection();
            Assert.IsFalse(type.IsInteger(typeof(string)));
        }

        [TestMethod]
        public void Test080()
        {
            var type = new TypeReflection();
            var result = type.From(typeof(string).GetMember("Length")[0]);

            Assert.AreEqual(typeof(int), result);
        }
        [TestMethod]
        public void Test081()
        {
            var type = new TypeReflection();
            var result = type.From(typeof(string[]).GetMember("Length")[0]);

            Assert.AreEqual(typeof(int), result);
        }
        [TestMethod]
        public void Test082()
        {
            var type = new TypeReflection();
            var result = type.From(typeof(List<string>).GetMember("Count")[0]);

            Assert.AreEqual(typeof(int), result);
        }
        [TestMethod]
        public void Test083()
        {
            var type = new TypeReflection();

            var meths = typeof(string).GetMethods().Where(m=>m.Name == "GetHashCode");
            foreach (var m in meths) {
              Console.WriteLine("Method: {0}", m.ToString());
            }
            var result = type.From(typeof(string).GetMethod("GetHashCode", new Type[]{}));

            Assert.AreEqual(typeof(int), result);
        }
        [TestMethod]
        public void Test084()
        {
            var type = new TypeReflection();
            var result = type.From(typeof(string).GetMethod("GetHashCode", new Type[]{}));

            Assert.AreEqual(typeof(int), result);
        }

        class Dummy01 {
            public string StringData;
        }
        [TestMethod]
        public void Test085()
        {
            var type = new TypeReflection();
            var result = type.From(typeof(Dummy01).GetField("StringData"));

            Assert.AreEqual(typeof(string), result);
        }

        class Dummy02
        {
            public event EventHandler MyEvent;
        }
        [TestMethod]
        public void Test086()
        {
            var type = new TypeReflection();
            var result = type.From(typeof(Dummy02).GetEvent("MyEvent"));

            Assert.AreEqual(typeof(EventHandler), result);
        }

        class Dummy03
        {
            public class Inner {

            }
        }
        [TestMethod]
        public void Test087()
        {
            var type = new TypeReflection();
            var result = type.From(typeof(Dummy03.Inner));

            Assert.AreEqual(typeof(Dummy03.Inner), result);
        }
        [TestMethod]
        public void Test088()
        {
            var type = new TypeReflection();
            var result = type.From(typeof(string));

            Assert.AreEqual(typeof(string), result);
        }
        [TestMethod]
        public void Test089()
        {
            var type = new TypeReflection();
            var result = type.From(typeof(string).GetConstructors()[0]);

            Assert.AreEqual(typeof(string), result);
        }

        [TestMethod]
        public void Test090()
        {
            var type = new TypeReflection();
            var result = type.NameOf(() => default(string));

            Assert.AreEqual("System.String", result);
        }

        [TestMethod]
        public void Test091()
        {
            var type = new TypeReflection();
            var result = type.NameOf(() => default(int?));

            Assert.AreEqual("System.Nullable`1[System.Int32]", result);
        }

        [TestMethod]
        public void Test100()
        {
            var type = new TypeReflection();
            var result = type.From(() => default(List<string>));
            var newResult = type.Transmute(result, typeof(double));

            var typeToAssert = type.From(() => default(List<double>));

            Assert.AreEqual(typeToAssert, newResult);

        }
    }
}
