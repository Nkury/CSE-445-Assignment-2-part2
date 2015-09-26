// Programmer:  Nizar Kury
// Instructor:  Professor Chen
// Date:        9/14/2015
// Description: A class that encrypts objects of the OrderObject class in the following format:
//              <senderID>/<card number>/<receiverID>/<amount of tickets>/<unit price

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2
{
    public static class Encoder
    {
        // static method that can be called anywhere to encrypt the object into a string value
        public static string encrypt(OrderObject oo)
        {
            // string encryption as follows: <senderID>/<card number>/<receiverID>/<amount of tickets>/<unit price
            string encrypted = oo.getSenderID() + "/" + oo.getCardNum() + "/" + oo.getReceiverID() + "/" + oo.getAmount() + "/" + oo.getUnitPrice();
            return encrypted;
      
        }
    }
}
