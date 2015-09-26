using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Assignment2
{
    public class OrderProcessing
    {
        public void orderFunc(OrderObject order)
        {
            //start thread
            //check if card is valid
            int cardNo = order.getCardNum();

            // check if airline matches receiver ID
            if (cardNo >= 5000 && cardNo <= 7000)
            {
                //card is valid, process order
                double orderTotal = order.getUnitPrice() * order.getAmount() * .081; //(unit price * amount of tickets * sales tax) 
                Program.confirm.setCell(order.getSenderID(), order, orderTotal);   // place the confirmation in the confirmation buffer   
            }
        }
    }
}
