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
           private static double ticketPrice = 900;
           private static Int32 amountOfTickets = 1000; // default starts at 1000 tickets to purchase
           private static Int32 numPriceCuts = 0; // counts how many price cut events occurs
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
                    if (priceCut != null) {  //there is at least a subscriber
                        priceCut(airlineName, prevAmt, prevPrice, newPrice);
                        numPriceCuts++;
                    }

                }
                ticketPrice = newPrice;
            }
           public void priceModel(string name)
            {
                while(numPriceCuts < 20)
                {
                    Thread.Sleep(500);
                    //Take the order from the queue of the orders;
                    string orderString = Program.bufferRef.getOneCell();
                    OrderObject order = Decoder.decrypt(orderString);
                    //Decide the price based on the orders
                    if(name == order.getReceiverID()){
                        amountOfTickets = order.getAmount();
                        Thread orderProc = new Thread(new ThreadStart(() => op.orderFunc(order)));
                        orderProc.Start();
                        Program.bufferRef.eraseCell(Encoder.encrypt(order));
                    } 

                    Int32 p = rng.Next(100, 900);
                    //Console.WriteLine("New Price is {0}", p);
                    Airline.changePrice(name, amountOfTickets, ticketPrice, p);
                }
            }

       })

        
}

    

