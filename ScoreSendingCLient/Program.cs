//http://msdn.microsoft.com/en-us/library/ktfa4fek.aspx

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace ScoreSendingClient
{
    class Program
    {
        static void Main(string[] args)
        {
            string uriString;
            Console.WriteLine("Please enter the URI topost data to");
            uriString = Console.ReadLine();

            //create webclient isntance
            WebClient webClient = new WebClient();
            Console.WriteLine("Please enter the data to be posted to the URI");
            string postData = Console.ReadLine();
            webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

            //display the headers in the request
            Console.Write("Resulting Request Headers:  ");
            Console.Write(webClient.Headers.ToString() + "\n");

            //apply ACII encoding to obtain the string as a byte array
            byte[] byteArray = Encoding.ASCII.GetBytes(postData);
            Console.WriteLine("Uploading to {0}", uriString);

            //upload the input string using http 1.0 POST method
            byte[] responseArray = webClient.UploadData(uriString, "POST", byteArray);

            //decode and display the response
            Console.WriteLine("\nResponse recieved was {0}",
                Encoding.ASCII.GetString(responseArray));

            Console.ReadKey();


        }
    }
}
