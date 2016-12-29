using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace ExpressiveReflection.Tests
{
    [TestClass]
    public class CollectionReflectionTests
    {

        class IEnumerableBase : IEnumerable<string>
        {
            public IEnumerator<string> GetEnumerator()
            {
                throw new NotImplementedException();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                throw new NotImplementedException();
            }
        }
        class IEnumerabelDervied : IEnumerableBase
        {
        }
        class IEnumerabelDervied2 : IEnumerabelDervied
        {
        }

        interface InterfaceIEnumerable : IEnumerable<string>
        {
        }
        class InterfaceIEnumerableDervied : InterfaceIEnumerable
        {
            public IEnumerator<string> GetEnumerator()
            {
                throw new NotImplementedException();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                throw new NotImplementedException();
            }
        }


        [TestMethod]
        public void Test001()
        {
            var cr = new CollectionReflection();
            var ele = cr.FindIEnumerable(null);

            Assert.IsNull(ele);
        }
        [TestMethod]
        public void Test002()
        {
            var cr = new CollectionReflection();
            var ele = cr.FindIEnumerable(typeof(string));

            Assert.IsNull(ele);
        }
        [TestMethod]
        public void Test003()
        {
            var cr = new CollectionReflection();
            var ele = cr.FindIEnumerable(typeof(List<string>));

            Assert.AreEqual(typeof(IEnumerable<string>), ele);
        }
        [TestMethod]
        public void Test004()
        {
            var cr = new CollectionReflection();
            var ele = cr.FindIEnumerable(typeof(string[]));

            Assert.AreEqual(typeof(IEnumerable<string>), ele);
        }
        [TestMethod]
        public void Test005()
        {
            var cr = new CollectionReflection();
            var ele = cr.FindIEnumerable(typeof(IEnumerabelDervied));

            Assert.AreEqual(typeof(IEnumerable<string>), ele);
        }
        [TestMethod]
        public void Test006()
        {
            var cr = new CollectionReflection();
            var ele = cr.FindIEnumerable(typeof(InterfaceIEnumerableDervied));

            Assert.AreEqual(typeof(IEnumerable<string>), ele);
        }
        [TestMethod]
        public void Test007()
        {
            var cr = new CollectionReflection();
            var ele = cr.FindIEnumerable(typeof(IEnumerabelDervied2));

            Assert.AreEqual(typeof(IEnumerable<string>), ele);
        }

        [TestMethod]
        public void Test010()
        {
            var cr = new CollectionReflection();
            var ele = cr.GetElementType(typeof(IEnumerable<string>));

            Assert.AreEqual(typeof(string), ele);
        }
        [TestMethod]
        public void Test011()
        {
            var cr = new CollectionReflection();
            var ele = cr.GetElementType(typeof(List<string>));

            Assert.AreEqual(typeof(string), ele);
        }
        [TestMethod]
        public void Test012()
        {
            var cr = new CollectionReflection();
            var ele = cr.GetElementType(typeof(string));

            Assert.AreEqual(typeof(string), ele);
        }
        [TestMethod]
        public void Test013()
        {
            var cr = new CollectionReflection();
            var ele = cr.GetElementType(typeof(int));

            Assert.AreEqual(typeof(int), ele);
        }
        [TestMethod]
        public void Test014()
        {
            var cr = new CollectionReflection();
            var ele = cr.GetElementType(typeof(Dictionary<bool, bool>));

            Assert.AreEqual(typeof(KeyValuePair<bool,bool>), ele);
        }
        [TestMethod]
        public void Test020()
        {
            var cr = new CollectionReflection();
            var ele = cr.GetIEnumerableType(typeof(string));

            Assert.AreEqual(typeof(IEnumerable<string>), ele);
        }
        [TestMethod]
        public void Test021()
        {
            var cr = new CollectionReflection();
            var ele = cr.GetIEnumerableType(typeof(IEnumerable<string>));

            Assert.AreEqual(typeof(IEnumerable<IEnumerable<string>>), ele);
        }
        [TestMethod]
        public void Test022()
        {
            var cr = new CollectionReflection();
            var ele = cr.GetIEnumerableType(typeof(int));

            Assert.AreEqual(typeof(IEnumerable<int>), ele);
        }
    }
}
