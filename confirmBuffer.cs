using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2
{
    public class confirmObject{
        public string travelAgency;
        public double orderTotal;
        public confirmObject(string n, double o)
        {
            travelAgency = n;
            orderTotal = o;
        }
    }

    public class confirmBuffer
    {
        private List<confirmObject> buffer = new List<confirmObject>();

        public void setCell(string name, double order)
        {
            //lock (buffer)
            //{
                buffer.Add(new confirmObject(name, order));
            //}
        }

        public double getCell(string name)
        {
            //lock (buffer)
            //{
                for (int i = 0; i < buffer.Count; i++)
                {
                    if (buffer[i].travelAgency == name)
                    {
                        double temp = buffer[i].orderTotal;
                         buffer.RemoveAt(i);
                         return temp;
                    }
              //  }
            }

            return 0; // couldn't find the confirmation
        }
    }
}
