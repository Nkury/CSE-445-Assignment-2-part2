using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;


namespace Assignment2
{
    public delegate void priceCutEvent(string airlineName, int prevAmt, double prevPrice, double newPrice); //delegate??

    public class Airline
    {
        static Random rng = new Random();
        public static event priceCutEvent priceCut;
        private double ticketPrice = 900;
        private Int32 amountOfTickets = 100; // default starts at 100 tickets to purchase
        public Int32 numPriceCuts = 0; // counts how many price cut events occurs
        private OrderProcessing op = new OrderProcessing();
        public double getPrice()
        {
            return ticketPrice;
        }
        public Int32 getNumPriceCuts()
        {
            return numPriceCuts;
        }
        public void changePrice(string airlineName, int prevAmt, double prevPrice, double newPrice)
        {
            if (newPrice < ticketPrice) //a price cut
            { //a price cut
                if (priceCut != null)
                {  //there is at least a subscriber
                    Program.semaphore.WaitOne();
                    Console.WriteLine("--- " + airlineName + " initiated PRICE CUT #" + (numPriceCuts +1) + " from $" + prevPrice + " to $" + newPrice);
                    priceCut(airlineName, prevAmt, prevPrice, newPrice);
                    numPriceCuts++;
                }

            }
            ticketPrice = newPrice;
        }
        public void priceModel(string name)
        {
            while (numPriceCuts < 20)
            {
                int tempNumCuts = numPriceCuts;
                Thread.Sleep(2000);
                Int32 p = rng.Next(100 + numPriceCuts, 900 + numPriceCuts);
                changePrice(name, amountOfTickets, ticketPrice, p);
            
                //Take the order from the queue of the orders;
                string orderString = "";

                if (tempNumCuts != numPriceCuts)
                {
                    while (orderString == "")
                    {
                        orderString = Program.bufferRef.getOneCell();
                        OrderObject order = null;
                        if (orderString != "")
                            order = Decoder.decrypt(orderString);

                        //Decide the price based on the orders
                        if (order != null && name == order.getReceiverID())
                        {
                            amountOfTickets = order.getAmount();
                            Thread orderProc = new Thread(new ThreadStart(() => op.orderFunc(order)));
                            orderProc.Start();
                            Program.bufferRef.eraseCell(Encoder.encrypt(order));
                        }
                    }
                }
            }

            Console.WriteLine("****************" + name + " HAS FINISHED ITS PRICE CUTS ****************");
        }
    }
}
    

