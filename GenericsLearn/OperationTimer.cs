using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace GenericsLearn
{
    internal sealed class OperationTimer : IDisposable
    {
        private Int64 m_startTime;
        private String m_text;
        private Int32 m_collectionCount;
        private Stopwatch timer;
        
        public OperationTimer(String text)
        {
            PrepareForOperation();
            m_text = text;
            m_collectionCount = GC.CollectionCount(0);
            // Эта команда должна быть последней в этом методе
            // для максимально точной оценки быстродействия
            timer = new Stopwatch();
            m_startTime = timer.ElapsedMilliseconds;
            timer.Start();
        }
        public void Dispose()
        {
            Console.WriteLine("{0} (GCs={1}) {3}", timer.Elapsed,
            GC.CollectionCount(0), m_collectionCount, m_text);
        }
        private static void PrepareForOperation()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }
    }
}
