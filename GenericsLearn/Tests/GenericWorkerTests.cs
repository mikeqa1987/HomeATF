using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace GenericsLearn.Tests
{
    public class GenericWorkerTests
    {
        public void SwapTest()
        {
            var worker = new GenericWorker();
            int first = 1;
            int second = 2;

            for (int i = 0; i < 100; i++)
            {
                first = i;
                second = i * 2;
                worker.Swap<int>(ref first, ref second);
                Assert.AreEqual(first, i * 2);
                Assert.AreEqual(second, i);

            }
        }

        public void Parameterized_SwapTest(int firstValue, int secondValue)
        {
            Warn.If(firstValue == secondValue, $"Warning: {firstValue} {secondValue}");
            var worker = new GenericWorker();
            int first = firstValue;
            int second = secondValue;

            worker.Swap<int>(ref first, ref second);
            Assert.AreEqual(first, secondValue);
            Assert.AreEqual(second, firstValue);

        }
    }
}

