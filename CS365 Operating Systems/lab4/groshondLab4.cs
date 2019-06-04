using System;
using System.Threading;

namespace groshongLab4
{
    class Bridge
    {
        private static Semaphore signal = new Semaphore(0,2);

        // enter the bridge and 
        // aquire the lock on the semaphore
        public void enter()
        {
            signal.WaitOne();
        }
        // exit the bridge and
        // release the lock on the semaphore
        public void exit()
        {
            signal.Release();
        }
    }
}
