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
        private string[] buffer = new string[3];

        public MultiCellBuffer()
        {
            for (int i = 0; i < 3; i++)
                buffer[i] = "";
        }

        public void setOneCell(string order)
        {
         
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
                for (int i = 0; i < 3; i++)
                {
                    if (buffer[i] != "")
                    {
                        return buffer[i];
                    }
                }

            return ""; // to satisfy condition to have all code paths return a value

        }

        public void eraseCell(string order)
        {
            lock (buffer)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (buffer[i] == order)
                    {
                        Program.semaphore.Release(); // release one resource
                        buffer[i] = "";
                        i = 4;
                    }
                }
            }
        
        }
    }
}
