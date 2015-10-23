using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressiveReflection.Tests
{
    [TestClass]
    public class APIEquivalenceComparisonTests
    {
        [TestMethod]
        public void Test001()
        {
            var comparison = new APIEquivalenceComparison();
            
            var member1 = typeof(string).GetMethod("ToString", new Type[] { });
            var member2 = typeof(object).GetMethod("ToString", new Type[] { });
            
            Assert.IsTrue(comparison.Matches(member1, member2));
        }

        [TestMethod]
        public void Test002()
        {
            var comparison = new APIEquivalenceComparison();

            var member1 = typeof(string).GetMethod("ToString", new Type[] { typeof(IFormatProvider) });
            var member2 = typeof(object).GetMethod("ToString", new Type[] { });

            Assert.IsFalse(comparison.Matches(member1, member2));
        }

        class Dummy01 : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                throw new NotImplementedException();
            }
        }
        [TestMethod]
        public void Test003()
        {
            var comparison = new APIEquivalenceComparison();

            var member1 = typeof(Dummy01).GetMethod("GetEnumerator", new Type[] { });
            var member2 = typeof(IEnumerable).GetMethod("GetEnumerator", new Type[] { });
            
            Assert.IsTrue(comparison.Matches(member1, member2));
        }
        class Dummy02 : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                throw new NotImplementedException();
            }
        }
        [TestMethod]
        public void Test004()
        {
            var comparison = new APIEquivalenceComparison();

            var member1 = typeof(Dummy01).GetMethod("GetEnumerator", new Type[] { });
            var member2 = typeof(Dummy02).GetMethod("GetEnumerator", new Type[] { });

            Assert.IsTrue(comparison.Matches(member1, member2));
        }

        [TestMethod]
        public void Test005()
        {
            var comparison = new APIEquivalenceComparison();

            var member1 = typeof(ICollection<string>).GetProperty("Count");
            var member2 = typeof(List<string>).GetProperty("Count");

            Assert.IsTrue(comparison.Matches(member1, member2));
        }

        [TestMethod]
        public void Test006()
        {
            var comparison = new APIEquivalenceComparison();

            var member1 = typeof(List<string>).GetProperty("Count");
            var member2 = typeof(ICollection<string>).GetProperty("Count");

            Assert.IsTrue(comparison.Matches(member1, member2));
        }
        [TestMethod]
        public void Test007()
        {
            var comparison = new APIEquivalenceComparison();

            var member1 = typeof(HashSet<string>).GetProperty("Count");
            var member2 = typeof(List<string>).GetProperty("Count");

            Assert.IsTrue(comparison.Matches(member1, member2));
        }
        [TestMethod]
        public void Test008()
        {
            var comparison = new APIEquivalenceComparison();

            var member1 = typeof(List<string>).GetProperty("Count");
            var member2 = typeof(HashSet<string>).GetProperty("Count");

            Assert.IsTrue(comparison.Matches(member1, member2));
        }

        interface IDummy03 {
            int X { set; }
        }
        class Dummy04 : IDummy03 {
            public int X { set { } }
        }
        class Dummy05 : IDummy03 {
            public int X { set { } }
        }

        [TestMethod]
        public void Test009()
        {
            var comparison = new APIEquivalenceComparison();

            var member1 = typeof(IDummy03).GetProperty("X");
            var member2 = typeof(Dummy04).GetProperty("X");

            Assert.IsTrue(comparison.Matches(member1, member2));
        }

        [TestMethod]
        public void Test010()
        {
            var comparison = new APIEquivalenceComparison();

            var member1 = typeof(Dummy04).GetProperty("X");
            var member2 = typeof(IDummy03).GetProperty("X");

            Assert.IsTrue(comparison.Matches(member1, member2));
        }

        [TestMethod]
        public void Test011()
        {
            var comparison = new APIEquivalenceComparison();

            var member1 = typeof(Dummy05).GetProperty("X");
            var member2 = typeof(Dummy04).GetProperty("X");

            Assert.IsTrue(comparison.Matches(member1, member2));
        }

        [TestMethod]
        public void Test012()
        {
            var comparison = new APIEquivalenceComparison();

            var member1 = typeof(Dummy04).GetProperty("X");
            var member2 = typeof(Dummy05).GetProperty("X");

            Assert.IsTrue(comparison.Matches(member1, member2));
        }

        class Dummy06 {
            public virtual int X { get { return default(int); } }
        }
        class Dummy07 : Dummy06
        {
        }
        class Dummy08 : Dummy06 {
            public override int X { get { return default(int); } }
        }

        [TestMethod]
        public void Test013()
        {
            var comparison = new APIEquivalenceComparison();

            var member1 = typeof(Dummy06).GetProperty("X");
            var member2 = typeof(Dummy07).GetProperty("X");

            Assert.IsTrue(comparison.Matches(member1, member2));
        }
        [TestMethod]
        public void Test014()
        {
            var comparison = new APIEquivalenceComparison();

            var member1 = typeof(Dummy06).GetProperty("X");
            var member2 = typeof(Dummy08).GetProperty("X");

            Assert.IsTrue(comparison.Matches(member1, member2));
        }
        [TestMethod]
        public void Test015()
        {
            var comparison = new APIEquivalenceComparison();

            var member1 = typeof(Dummy07).GetProperty("X");
            var member2 = typeof(Dummy06).GetProperty("X");

            Assert.IsTrue(comparison.Matches(member1, member2));
        }
        [TestMethod]
        public void Test016()
        {
            var comparison = new APIEquivalenceComparison();

            var member1 = typeof(Dummy08).GetProperty("X");
            var member2 = typeof(Dummy06).GetProperty("X");

            Assert.IsTrue(comparison.Matches(member1, member2));
        }

        [TestMethod]
        public void Test017()
        {
            var comparison = new APIEquivalenceComparison();

            var member1 = typeof(Dummy08).GetProperty("X");
            var member2 = typeof(Dummy07).GetProperty("X");

            Assert.IsTrue(comparison.Matches(member1, member2));
        }
        [TestMethod]
        public void Test018()
        {
            var comparison = new APIEquivalenceComparison();

            var member1 = typeof(Dummy07).GetProperty("X");
            var member2 = typeof(Dummy08).GetProperty("X");

            Assert.IsTrue(comparison.Matches(member1, member2));
        }


        class Dummy09
        {
            public virtual int X { set {  } }
        }
        class Dummy10 : Dummy09
        {
        }
        class Dummy11 : Dummy09
        {
            public override int X { set {  } }
        }

        [TestMethod]
        public void Test019()
        {
            var comparison = new APIEquivalenceComparison();

            var member1 = typeof(Dummy09).GetProperty("X");
            var member2 = typeof(Dummy10).GetProperty("X");

            Assert.IsTrue(comparison.Matches(member1, member2));
        }
        [TestMethod]
        public void Test020()
        {
            var comparison = new APIEquivalenceComparison();

            var member1 = typeof(Dummy09).GetProperty("X");
            var member2 = typeof(Dummy11).GetProperty("X");

            Assert.IsTrue(comparison.Matches(member1, member2));
        }
        [TestMethod]
        public void Test021()
        {
            var comparison = new APIEquivalenceComparison();

            var member1 = typeof(Dummy10).GetProperty("X");
            var member2 = typeof(Dummy09).GetProperty("X");

            Assert.IsTrue(comparison.Matches(member1, member2));
        }
        [TestMethod]
        public void Test022()
        {
            var comparison = new APIEquivalenceComparison();

            var member1 = typeof(Dummy11).GetProperty("X");
            var member2 = typeof(Dummy09).GetProperty("X");

            Assert.IsTrue(comparison.Matches(member1, member2));
        }

        [TestMethod]
        public void Test023()
        {
            var comparison = new APIEquivalenceComparison();

            var member1 = typeof(Dummy11).GetProperty("X");
            var member2 = typeof(Dummy10).GetProperty("X");

            Assert.IsTrue(comparison.Matches(member1, member2));
        }
        [TestMethod]
        public void Test024()
        {
            var comparison = new APIEquivalenceComparison();

            var member1 = typeof(Dummy10).GetProperty("X");
            var member2 = typeof(Dummy11).GetProperty("X");

            Assert.IsTrue(comparison.Matches(member1, member2));
        }

    }
}
