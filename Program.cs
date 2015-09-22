using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Assignment2
{
    class Program
    {
         public static AutoResetEvent orderCompleted = new AutoResetEvent(false); // when the airline is done processing the order, this event
                                                                          // coordinates with the travel agency so it can print the timestamp
         public static MultiCellBuffer bufferRef = new MultiCellBuffer(); // instantiate buffer that will hold the orders of travel agencies
        static void Main(string[] args)
        {
            // change
            Airline air = new Airline(); // create an airline to pass the airline function for threading
            
            // Instantiate 3 airline threads and then start them
            Thread airline1 = new Thread(new ThreadStart(() => air.priceModel("airline1"))); // start the thread with the passed-in parameter airline1 string
            Thread airline2 = new Thread(new ThreadStart(() => air.priceModel("airline2"))); // start the thread with the passed-in parameter airline2 string
            Thread airline3 = new Thread(new ThreadStart(() => air.priceModel("airline3"))); // start the thread with the passed-in parameter airline3 string
            airline1.Name = "airline 1";
            airline2.Name = "airline 2";
            airline3.Name = "airline 3";
            airline1.Start();
            airline2.Start();
            airline3.Start();
            
            // Instantiate a travel agency object in order to use its methods
            TravelAgency travel = new TravelAgency();
            Airline.priceCut += new priceCutEvent(travel.ticketsOnSale);       // have the travel agency method subscribe to airline event

            Thread[] travelAgencies = new Thread[5]; // create 5 travel agency threads
            for (int i = 0; i < 3; i++) // N = 5 (for five travel agency threads)
            {
                // Start 5 travel agency threads
                travelAgencies[i] = new Thread(new ThreadStart(travel.travelAgencyFunc));
                travelAgencies[i].Name = "travel agency " + (i+1).ToString();
                travelAgencies[i].Start();
            }

            // create buffer classes, etc.

        }
    }
}
