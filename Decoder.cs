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
            Console.WriteLine(s);
            String[] items = s.Split('/');
            OrderObject order = new OrderObject(items[0], Convert.ToInt32(items[1]), items[2], 
                Convert.ToInt32(items[3]), Convert.ToDouble(items[4]));
            /*order.setSenderID(items[0]);
            order.setCreditNum(Convert.ToInt32(items[1]));
            order.setReceiverID(items[2]);
            order.setAmount(Convert.ToInt32(items[3]));
            order.setPrice(Convert.ToInt32(items[4])); */
            return order;
        }
     }
}
