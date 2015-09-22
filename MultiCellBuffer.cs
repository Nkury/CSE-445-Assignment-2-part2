using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Assignment2
{
    public class MultiCellBuffer
    {
        private Semaphore semaphore = new Semaphore(0, 3); // semaphore for keeping track of how many cells are open
        private string[] buffer = new string[3];

        public MultiCellBuffer()
        {
            for (int i = 0; i < 3; i++)
                buffer[i] = "";
            semaphore.Release(3); // set the semaphore at max value to indicate that all the cells are empty

        }
        public void setOneCell(string order)
        {
            // IF THERE IS AN ERROR, THEN IT HAS TO TO WITH THIS

            semaphore.WaitOne(); // acquire one resource
            lock (buffer)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (buffer[i] == "")
                    {
                        buffer[i] = order;
                        i = 4;
                    }
                }
            }
        }

        public string getOneCell()
        {
            lock (buffer)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (buffer[i] != "")
                    {
                        string temp = buffer[i];
                        return temp;
                    }
                }
            }

            return ""; // to satisfy condition to have all code paths return a value

        }

        public void eraseCell(string order)
        {
            for (int i = 0; i < 3; i++)
            {
                if (buffer[i] == order)
                {
                    semaphore.Release(); // release one resource
                    buffer[i] = "";
                    i = 4;
                }
            }

        }
    }
}
