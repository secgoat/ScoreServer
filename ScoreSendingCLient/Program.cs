//http://msdn.microsoft.com/en-us/library/ktfa4fek.aspx

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Net;

namespace ScoreSendingClient
{
    class Program
    {
        static void Main(string[] args)
        {
            bool running = true;
            do
            {
                Console.WriteLine("Would you like to POST or GET? (p/g)");
                string choice = Console.ReadLine().ToLower();
                if (choice == "p")
                    Post();
                else if (choice == "g")
                    Get();
                else
                    Console.WriteLine(choice + " is not a valid choice");
                
                Console.WriteLine("Would you like to try again? (y/n)");
                choice = Console.ReadLine().ToLower();
                if (choice == "n")
                    running = false;

            } while (running);


            Console.WriteLine("Press Any Key To Exit");
            Console.ReadKey();


        }

        static void Post()
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

        }

        static void Get()
        {
            string uriString;
            Console.WriteLine("Please enter the URI of the website to retrieve");
            uriString = Console.ReadLine();

            using (WebClient webCLient = new WebClient())
            {
                webCLient.Headers["User-Agent"] =
                    "Mozilla/4.0 (Compatible; Windows NT 5.1; MSIE 6.0) " +
                    "(compatible; MSIE 6.0; Windows NT 5.1; " +
                    ".NET CLR 1.1.4322; .NET CLR 2.0.50727)";

                //download the data
                byte[] pageArray = webCLient.DownloadData(uriString);
                
                //And display the response
                Console.WriteLine("\nResponse recieved was {0}",
                Encoding.ASCII.GetString(pageArray));
            }

        }
    }
}
