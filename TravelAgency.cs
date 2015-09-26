// Programmer:  Nizar Kury
// Instructor:  Professor Chen
// Date:        9/25/2015
// Description: A class where threads are instantiated from the main-- the actions of this class are event-driven as there is an event-handler
//              that is responsible for indicating whether a price cut occurs or not and will place orders accordingly to the airline. 
//              The travel agency thread will then repeatedly check the confirmation buffer to see whether or not the order was processed.
//              If processed, the travel agency will then print an order confirmation detailing the order information and the time it took for
//              the order to process.


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
        static Random rdm = new Random();   // used for assigning a travel agency to an order from the airline
        private int month = DateTime.Now.Month; // used to determine the timestamp of order 
        private int day = DateTime.Now.Day; // used to determine the timestamp of order 
        private int year = DateTime.Now.Year;   // used to determine the timestamp of order 
        private int hour = DateTime.Now.Hour;   // used to determine the timestamp of order 
        private int min = DateTime.Now.Minute;  // used to determine the timestamp of order     
        private int sec = DateTime.Now.Second;  // used to determine the timestamp of order and time taken for order to process
        private int msec = DateTime.Now.Millisecond;    // used to determine the timestamp of order  and time taken for order to process

        public void travelAgencyFunc()  // for starting thread
        {
            for(int i = 0; i < 200; i++) // loops 200 times to periodically check if there is a price cut (covers all bases)
                                         // Because it loops 200 times, the Console application will not immediately terminate, but will
                                         // over time (for grading purposes and to make sure all orders have been processed).
            {
                Thread.Sleep(800); // wait for the confirm buffer to be released
             
                confirmObject cOrder = null;  // initializes it to null in order to go through a loop and keep checking the confirmation buffer
    
                // until the confirmation order object retrieves something from the confirmation buffer, a loop will continually check 
                // the confirmation buffer every second until a confirmation is found.
                while (cOrder == null && Program.confirm.getSize() > 0)
                {
                    Thread.Sleep(200); // checks the confirmation buffer every .2 seconds
                    cOrder = Program.confirm.getCell(Thread.CurrentThread.Name);
                }

                // prints the order confirmation depending on if an order was read
                if (cOrder != null && cOrder.orderTotal != 0)
                {
                    // the following conditionals for printing the order confirmation make sure that the time elapsed from when the
                    // order was placed to when the order was confirmed is printed correctly without negatives
                    if (DateTime.Now.Second >= sec)
                    {
                        if(DateTime.Now.Millisecond > msec)
                            Console.WriteLine("ORDER: " + cOrder.order.getAmount() + " tickets at $" + cOrder.order.getUnitPrice() +
                            " each for a total of $" + cOrder.orderTotal + " from " + Thread.CurrentThread.Name +
                                " to " + cOrder.order.getReceiverID() + " using credit card number " + cOrder.order.getCardNum()
                                + ". Order finished processing at: " + month + "/" + day + "/" + year + " @ "
                              + hour + ":" + min + ":" + sec + " and took " + (DateTime.Now.Second - sec) +
                              "." + (DateTime.Now.Millisecond - msec) + " secs"); 
                        else
                            Console.WriteLine("ORDER: " + cOrder.order.getAmount() + " tickets at $" + cOrder.order.getUnitPrice() +
                          " each for a total of $" + cOrder.orderTotal + " from " + Thread.CurrentThread.Name +
                              " to " + cOrder.order.getReceiverID() + " using credit card number " + cOrder.order.getCardNum()
                              + ". Order finished processing at: " + month + "/" + day + "/" + year + " @ "
                            + hour + ":" + min + ":" + sec + " and took " + (DateTime.Now.Second - sec) +
                            "." + ((DateTime.Now.Millisecond + 1000) - msec) + " secs"); 
                    }
                    else
                    {
                        if(DateTime.Now.Millisecond > msec)
                            Console.WriteLine("ORDER: " + cOrder.order.getAmount() + " tickets at $" + cOrder.order.getUnitPrice() +
                           " each for a total of $" + cOrder.orderTotal + " from " + Thread.CurrentThread.Name +
                               " to " + cOrder.order.getReceiverID() + " using credit card number " + cOrder.order.getCardNum()
                               + ". Order finished processing at: " + month + "/" + day + "/" + year + " @ "
                             + hour + ":" + min + ":" + sec + " and took " + (DateTime.Now.Second + 60 - sec) +
                             "." + (DateTime.Now.Millisecond - msec) + " secs"); 
                        else
                            Console.WriteLine("ORDER: " + cOrder.order.getAmount() + " tickets at $" + cOrder.order.getUnitPrice() +
                            " each for a total of $" + cOrder.orderTotal + " from " + Thread.CurrentThread.Name +
                              " to " + cOrder.order.getReceiverID() + " using credit card number " + cOrder.order.getCardNum()
                              + ". Order finished processing at: " + month + "/" + day + "/" + year + " @ "
                             + hour + ":" + min + ":" + sec + " and took " + (DateTime.Now.Second + 60 - sec) +
                             "." + ((DateTime.Now.Millisecond + 1000) - msec) + " secs"); 
                    }
                }
            }
        }

        // event handler that takes in the previous price and amount and the new price to calculate how many tickets should be bought
        public void ticketsOnSale(string airlineName, int prevAmt, double prevPrice, double newPrice)  // event handler
        {
            
            int newAmt = (int)(prevPrice * 100 / newPrice); // calculate new amount by taking the cost of the order previously multiplied with
                                                                // the default amount of tickets (100) and divide it by the new price
                Int32 travelNum = rdm.Next(1, 6);   // uses a random number generator to assign a random travel agency to place an order from
                                                    // a price cut
                
                // place the order by making an OrderObject, which has a senderID, credit card number, receiverID, order amount, and unit Price
                // as parameters
                OrderObject newOrder = new OrderObject(("travel agency " + (travelNum).ToString()), OrderObject.generateRandomCreditCardNo(), 
                    airlineName, newAmt, newPrice); // create new orderObject
      
                // encrypts the string in order for it to go through the buffer (protect the information)
                string encryptedObject = Encoder.encrypt(newOrder);

                // saves the time information for calculating the timestamp and amount of time taken between placing the order and confirming
                // the order
                month = DateTime.Now.Month;
                day = DateTime.Now.Day;
                year = DateTime.Now.Year;
                hour = DateTime.Now.Hour;
                min = DateTime.Now.Minute;
                sec = DateTime.Now.Second;
                msec = DateTime.Now.Millisecond;

                // sends the encrypted order into the multicell buffer in order for the airline to receive and process it
                Program.bufferRef.setOneCell(encryptedObject);
        }
    }
}

