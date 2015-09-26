// Programmer:  Nizar Kury
// Instructor:  Professor Chen
// Date:        9/14/2015
// Description: A class where threads are instantiated from the main-- the actions of this class are event-driven as there is an event-handler
//              that is responsible for indicating whether a price cut occurs or not.


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Assignment2
{
   
    public class TravelAgency
    {
        static Random rdm = new Random();
  
        public void travelAgencyFunc()  // for starting thread
        {
            for(int i = 0; i < 20; i++)// loops 20 times to periodically check if there is a price cut (20 seconds)
            {
                Thread.Sleep(800); // wait for the confirm buffer to be released
                int month = DateTime.Now.Month;
                int day = DateTime.Now.Day;
                int year = DateTime.Now.Year;
                int hour = DateTime.Now.Hour;
                int min = DateTime.Now.Minute;
                int sec = DateTime.Now.Second;
                int msec = DateTime.Now.Millisecond;
                confirmObject cOrder = null;  // initializes it to zero in order to go through a loop and keep checking the confirmation buffer
                //int count = 0;
                // Monitor.Wait(Program.confirm); // wait for the confirm buffer to be released
                while (cOrder == null&& Program.confirm.getSize() > 0/* && count < 10*/)
                {
                    Thread.Sleep(1000);
                    // Program.rwlock.AcquireReaderLock(300);
                    // try{
                   // Console.WriteLine("CHECK NAME: " + Thread.CurrentThread.Name + " with a size of " + Program.confirm.getSize());
                    cOrder = Program.confirm.getCell(Thread.CurrentThread.Name);
                    //count++;
                    //  }
                    // finally{
                    //     Program.rwlock.ReleaseReaderLock();
                    // }
                }

                if(cOrder != null && cOrder.orderTotal != 0)
                Console.WriteLine("ORDER: " + cOrder.order.getAmount() + " tickets at $" + cOrder.order.getUnitPrice() +
                " each for a total of $" + cOrder.orderTotal + " from " + Thread.CurrentThread.Name +
                    " to " + cOrder.order.getReceiverID() + ". Order finished processing at: " + month + "/" + day + "/" + year + " @ "
                  + hour + ":" + min + ":" + sec + " and took " + (DateTime.Now.Second - sec) +
                  "." + (DateTime.Now.Millisecond - msec) + " secs"); // print out the timestamp the order was finished processing
            }
        }

        // event handler that takes in the previous price and amount and the new price to calculate how many tickets should be bought
        public void ticketsOnSale(string airlineName, int prevAmt, double prevPrice, double newPrice)  // event handler
        {
            //Console.WriteLine("--- " + airlineName + " initiated a PRICE CUT from $" + prevPrice + " to $" + newPrice);
            int newAmt = (int)(prevPrice * 100 / newPrice); // calculate new amount by taking the cost of the order previously multiplied with
                                                                // the default amount of tickets and divide it by the new price
                Int32 travelNum = rdm.Next(1, 6);
                OrderObject newOrder = new OrderObject(("travel agency " + (travelNum).ToString()), OrderObject.generateRandomCreditCardNo(), 
                    airlineName, newAmt, newPrice); // create new orderObject
               // Console.WriteLine(Encoder.encrypt(newOrder));
                string encryptedObject = Encoder.encrypt(newOrder);
               // Program.rwlock.AcquireWriterLock(300);
                //try
              //  {
                    Program.bufferRef.setOneCell(encryptedObject);
               // }
              //  finally
              //  {
               //     Program.rwlock.ReleaseWriterLock();
               // }
            }


            // send the orderObject to Encoder for encryption and converting to string
            // Monitor.Wait(encryptedObject);   // wait for airline to finish processing the order before continuing
            /*int month = DateTime.Now.Month;
            int day = DateTime.Now.Day;
            int year = DateTime.Now.Year;
            int hour = DateTime.Now.Hour;
            int min = DateTime.Now.Minute;
            int sec = DateTime.Now.Second;*/
            /*Program.rwlock.AcquireWriterLock(300);
            try {
                Program.bufferRef.setOneCell(encryptedObject);
            } finally{
                Program.rwlock.ReleaseWriterLock();
            }*/
            // CONFIRMATION USING BUFFER 
            //double orderTotal = 0;  // initializes it to zero in order to go through a loop and keep checking the confirmation buffer
            //Monitor.Wait(Program.confirm); // wait for the confirm buffer to be released
           /* while (orderTotal == 0)
            {
                Thread.Sleep(1000);
                orderTotal = Program.confirm.getCell(Thread.CurrentThread.Name);
            } */
           // Console.WriteLine("Order processed for $" + orderTotal + ". Order finished processing at: " + month + "/" + day + "/" + year + " @ "
            //  + hour + ":" + min + ":" + sec + " and took " + (DateTime.Now.Second - sec) + " seconds"); // print out the timestamp the order was finished processing

         }
    }

