using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Convert; 

namespace Assignment2
{
    public delegate void priceCutEvent(string airlineName, int prevAmt, double prevPrice, double newPrice); //delegate??
       
       public class Airline
       {
           //TEST!!
           static Random rng = new Random();
           public static event priceCutEvent priceCut;
           private static Int32 ticketPrice = 900;
           private static Int32 numPriceCuts = 0;
           private OrderProcessing op();
           public Int32 getPrice() 
           {
               return ticketPrice;
           }
           public Int32 getNumPriceCuts()
           {
               return numPriceCuts;
           }
           public static void changePrice(string airlineName, int prevAmt, double prevPrice, double newPrice) 
           {  
                if (price < ticketPrice) //a price cut
                { //a price cut
                    numPriceCuts++;
                    if (priceCut != null)   //there is at least a subscriber
                        priceCut(price);
                }
                ticketPrice = price;
            }
           public void priceModel(string name)
            {
                for (Int32 i = 0; i < 20; i++)
                {
                    Thread.Sleep(500);
                    //Take the order from the queue of the orders;
                    string orderString = Program.bufferRef.getOneCell();
                    OrderObject order = Decoder.decrypt(orderString);
                    //Decide the price based on the orders
                    if(name == order.getReceiverID()){
                        OrderProcessing orderProcess = new OrderProcessing();
                        Thread orderProc = new Thread(new ThreadStart(orderProcess.orderFunc));
                        Program.bufferRef.eraseCell(Encoder.encrypt(order));
                    } 

                    Int32 p = rng.Next(100, 900);
                    //Console.WriteLine("New Price is {0}", p);
                    Airline.changePrice();
                }
            }

       })

        
}

    

