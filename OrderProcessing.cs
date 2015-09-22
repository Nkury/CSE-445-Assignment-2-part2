using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2
{
    public class OrderProcessing
    {
        public void orderFunc(string airlineName, OrderObject order)
        {
            //start thread
            //check if card is valid
            int cardNo = order.getCardNum();

            // check if airline matches receiver ID
            if (cardNo >= 5000 && cardNo <= 7000)
            {
                //card is valid, process order
                double orderTotal = order.getUnitPrice() * order.getAmount() * .081; //( tax + locationCharge??)  

                //order confirmation
                OrderProcessing.confirm(order);
            }
        }
        public bool confirm(OrderObject order) //set default to false?
        {
            Int32 a = order.getAmount();
            Int32 p = order.getUnitPrice();
            Int32 c = order.getCardNo();
            Console.WriteLine("Order confirmed for {0} tickets at ${1} each, purchased with card # {2}", a, p, c);
            return true;


            /*
             * class confirmation{
             *  string travelName;
             *  bool confirm;
             *  }
             *  
             * Array<List> confirmation;
             * 
             * confirmation.add(new confirmation(senderID, confirm))
             */
        }
    }
}
