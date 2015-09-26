// Programmer:  Nizar Kury
// Instructor:  Professor Chen
// Date:        9/25/2015
// Description: The Main Class creates and starts all the airline and travel agency threads, instantiates global buffers and semaphores, and
//              begins the program.

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
         public static MultiCellBuffer bufferRef = new MultiCellBuffer(); // instantiate multicell buffer that will hold the orders of travel agencies
         public static confirmBuffer confirm = new confirmBuffer();     // instantiate the confirmation buffer that will alert travel agencies
                                                                        // that their order has been successfully processed
         public static Semaphore semaphore = new Semaphore(0, 3);   // semaphore for keeping track of how many cells are open
      
        static void Main(string[] args)
        {
            Program.semaphore.Release(3); // sets the semaphore to the max value of the multicell buffer size, which is 3

            // create 3 airline objects to pass the airline function for threading 
            Airline air = new Airline(); 
            Airline air2 = new Airline();
            Airline air3 = new Airline();

            // Instantiate 3 airline threads and then start them
            Thread airline1 = new Thread(new ThreadStart(() => air.priceModel("airline1"))); // start the thread with the passed-in parameter airline1 string
            Thread airline2 = new Thread(new ThreadStart(() => air2.priceModel("airline2"))); // start the thread with the passed-in parameter airline2 string
            Thread airline3 = new Thread(new ThreadStart(() => air3.priceModel("airline3"))); // start the thread with the passed-in parameter airline3 string
            airline1.Name = "airline 1";    // give the thread a unique name
            airline2.Name = "airline 2";    // give the thread a unique name
            airline3.Name = "airline 3";    // give the thread a unique name
            airline1.Start();   // start the thread
            airline2.Start();   // start the thread
            airline3.Start();   // start the thread
            
            // Instantiate a travel agency object in order to use its methods
            TravelAgency travel = new TravelAgency();
            Airline.priceCut += new priceCutEvent(travel.ticketsOnSale);       // have the travel agency method subscribe to airline event

            Thread[] travelAgencies = new Thread[5]; // create 5 travel agency threads

            for (int i = 0; i <= 4; i++) // N = 5 (for five travel agency threads)
            {
                // Start 5 travel agency threads
                travelAgencies[i] = new Thread(new ThreadStart(travel.travelAgencyFunc));   // starts the thread with the travelAgencyFunc method
                travelAgencies[i].Name = "travel agency " + (i+1).ToString();   // gives each thread a unique name
                travelAgencies[i].Start();  // starts the thread
            }

        }
    }
}
