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
        private static double ticketPrice = 900;
        private static Int32 amountOfTickets = 1000; // default starts at 1000 tickets to purchase
        public static Int32 numPriceCuts = 0; // counts how many price cut events occurs
        private OrderProcessing op = new OrderProcessing();
        public double getPrice()
        {
            return ticketPrice;
        }
        public Int32 getNumPriceCuts()
        {
            return numPriceCuts;
        }
        public static void changePrice(string airlineName, int prevAmt, double prevPrice, double newPrice)
        {
            if (newPrice < ticketPrice) //a price cut
            { //a price cut
                if (priceCut != null)
                {  //there is at least a subscriber
                    Console.WriteLine("price cut for " + airlineName + " for " + newPrice);
                    priceCut(airlineName, prevAmt, prevPrice, newPrice);
                    // Console.WriteLine("price cut for " + airlineName + " for " + newPrice);
                    numPriceCuts++;
                }

            }
            ticketPrice = newPrice;
        }
        public void priceModel(string name)
        {
            while (numPriceCuts < 20)
            {
                Console.WriteLine("numPriceCuts= " + numPriceCuts);
                Thread.Sleep(500);
                // Console.WriteLine(name + " is also here before");
                //Take the order from the queue of the orders;
                string orderString = "";

                //Program.rwlock.AcquireReaderLock(300);
              //  try
              //  {
                    orderString = Program.bufferRef.getOneCell();
               // }
              //  finally
              //  {
               //     Program.rwlock.ReleaseReaderLock();
               // }

                if (orderString != "")
                {
                    //Console.WriteLine("Im here2");
                    OrderObject order = Decoder.decrypt(orderString);
                    //Decide the price based on the orders
                    if (name == order.getReceiverID())
                    {
                        amountOfTickets = order.getAmount();
                        Thread orderProc = new Thread(new ThreadStart(() => op.orderFunc(order)));
                        orderProc.Start();
                        Program.bufferRef.eraseCell(Encoder.encrypt(order));
                    }
                }

                Int32 p = rng.Next(100 + numPriceCuts, 900 + numPriceCuts);
                //Console.WriteLine("New Price is {0}", p);
                Airline.changePrice(name, amountOfTickets, ticketPrice, p);

            }
        }
    }
}
    

