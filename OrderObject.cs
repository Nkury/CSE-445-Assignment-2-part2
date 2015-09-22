// Programmer:  Nizar Kury
// Instructor:  Professor Chen
// Date:        9/14/2015
// Description: A class that contains information of an order from an airline.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2
{
    public class OrderObject
    {

        private string senderID; // the identity of the sender (thread name or thread id)
        private int cardNo;      // an integer representing a credit card number
        private string receiverID; // the identity of the receiver (thread name or thread id)
        private int amount;     // integer representing the number of tickets to order
        private double unitPrice;  // price of the ticket received from the airline
        static Random rng = new Random(); // generate random numbers for credit card numbers
        public OrderObject(string SID, int cNo, string RID, int amt, double unitP)
        {
            senderID = SID;
            cardNo = cNo;
            receiverID = RID;
            amount = amt;
            unitPrice = unitP;
        }

        // accessor method that returns the sender ID of the order object
        public string getSenderID()
        {
            return senderID;
        }

        // accessor method that returns the credit card number of the order object
        public int getCardNum()
        {
            return cardNo;
        }

        // accessor method that returns the receiver ID of the order object
        public string getReceiverID()
        {
            return receiverID;
        }

        // accessor method that returns the amount of tickets in the order object
        public int getAmount()
        {
            return amount;
        }

        // accessor method that returns the unit price of each ticket in the order object
        public double getUnitPrice()
        {
            return unitPrice;
        }

        // mutator method that assigns the parameter SID to the sender ID
        public void setSenderID(string SID)
        {
            senderID = SID;
        }

        // mutator method that assigns the parameter newCardNO to the credit card number of order object
        public void setCreditNum(int newCardNo)
        {
            cardNo = newCardNo;
        }

        // mutator method that assigns the parameter RID to receiver ID
        public void setReceiverID(string RID)
        {
            receiverID = RID;
        }

        // mutator method that assigns the parameter newAmt to the number of tickets in the order object
        public void setAmount(int newAmt)
        {
            amount = newAmt;
        }

        // mutator method that assigns the parameter newPrice to the unit price of each ticket in the order object
        public void setPrice(double newPrice)
        {
            unitPrice = newPrice;
        }

        public static int generateRandomCreditCardNo(){
            return rng.Next(5000, 7000);
        }
    }
}
