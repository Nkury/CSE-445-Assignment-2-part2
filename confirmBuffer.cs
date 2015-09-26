using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2
{
    public class confirmObject{
        public string travelAgency;
        public OrderObject order;
        public double orderTotal;

        public confirmObject(string n, OrderObject o, double ot)
        {
            travelAgency = n;
            order = o;
            orderTotal = ot;
        }
    }

    public class confirmBuffer
    {
        private List<confirmObject> buffer = new List<confirmObject>();

        public void setCell(string name, OrderObject order, double orderTotal)
        {
            lock (buffer)
            {
                buffer.Add(new confirmObject(name, order, orderTotal));
            }
        }

        public confirmObject getCell(string name)
        {
            lock (buffer)
            {
                for (int i = 0; i < buffer.Count; i++)
                {
                    if (buffer[i].travelAgency == name)
                    {
                         confirmObject temp = buffer[i];
                         buffer.RemoveAt(i);
                         return temp;
                    }
                }
            }

            return null; // couldn't find the confirmation
        }

        public int getSize()
        {
            return buffer.Count;
        }
    }
}
