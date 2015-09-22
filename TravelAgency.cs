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
  
        public void travelAgencyFunc()  // for starting thread
        {
            Airline air = new Airline(); // creates an airline object for checking the prices
            for (Int32 i = 0; i < 10; i++) // loops 10 times to periodically check if there is a price cut
            {
                Thread.Sleep(500);
                double p = air.getPrice();   // calls method of airline to find its price
                Console.WriteLine("Airline{0} has everyday low price: ${1} each", Thread.CurrentThread.Name, p); // Thread.CurrentThread.Name 
                                                                                                               // prints the thread name
            }
        }

        // event handler that takes in the previous price and amount and the new price to calculate how many tickets should be bought
        public void ticketsOnSale(string airlineName, int prevAmt, double prevPrice, double newPrice)  // event handler
        {
            int newAmt = (int)(prevPrice * prevAmt / newPrice); // calculate new amount by taking the cost of the order previously 
                                                                // and divide it by the new price

            OrderObject newOrder = new OrderObject(Thread.CurrentThread.Name, OrderObject.generateRandomCreditCardNo(), 
                airlineName, newAmt, newPrice); // create new orderObject

            string encryptedObject = Encoder.encrypt(newOrder); // send the orderObject to Encoder for encryption and converting to string
            Monitor.Wait(encryptedObject);   // wait for airline to finish processing the order before continuing
            int month = DateTime.Now.Month;
            int day = DateTime.Now.Day;
            int year = DateTime.Now.Year;
            int hour = DateTime.Now.Hour;
            int min = DateTime.Now.Minute;
            int sec = DateTime.Now.Second;
            Program.bufferRef.setOneCell(encryptedObject);
            // CONFIRMATION USING BUFFER 
            double orderTotal = 0;  // initializes it to zero in order to go through a loop and keep checking the confirmation buffer
            while (orderTotal == 0)
            {
                Thread.Sleep(1000);
                orderTotal = Program.confirm.getCell(Thread.CurrentThread.Name);
            }
            Console.WriteLine("Order processed for $" + orderTotal + ". Order finished processing at: " + month + "/" + day + "/" + year + " @ "
              + hour + ":" + min + ":" + sec + " and took " + (DateTime.Now.Second - sec) + " seconds"); // print out the timestamp the order was finished processing

         }
    }
}
