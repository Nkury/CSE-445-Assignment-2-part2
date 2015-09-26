using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2
{
    public static class Decoder
    {
        public static OrderObject decrypt(String s)
        {
            String[] items = s.Split('/');
            OrderObject order = new OrderObject(items[0], Convert.ToInt32(items[1]), items[2], 
                Convert.ToInt32(items[3]), Convert.ToDouble(items[4]));
            return order;
        }
     }
}
