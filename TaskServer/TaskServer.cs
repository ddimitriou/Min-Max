using System;
using System.IO;

// This library is used to register our object with the remoting back-end
using System.Runtime.Remoting;
// This library is used to register our TCP channel device with 
// the channel services
using System.Runtime.Remoting.Channels;
// This library provides us with a TCP channel that we will use to
// communicate with the remote Application Domain (the client)
using System.Runtime.Remoting.Channels.Tcp;

namespace TaskServer {
    
    public class TaskServerEngine {
        
        // We will only need one method, and it can be static
        // because there is no need for an instance of this class.
        // This method will simply create and initialise our TaskServerEngine.        
        public static void Main() {

            // Tell the user that we are starting the task server
            Console.WriteLine("MinMax Server Started");
            Console.WriteLine("\nStarting Task Server...");
            
            // We need to make sure that any exceptions are
            // caught and dealt with. This server is not mission-critical,
            // so we will exit on all exceptions. Thus, because of our approach,
            // we will only test for generic errors, namely Exceptions.
            try {
                // Create our TCP communication channel, which will sit in
                // our local TCP/IP stack on port 8085.
                Console.WriteLine("1 -- Creating TCP channel");
                TcpChannel chan = new TcpChannel(8085);
                
                // Register our new TcpChannel with .NET's Remoting Services
                // in order to make the service visible to clients.
                Console.WriteLine("2 -- Registering TCP channel");
                ChannelServices.RegisterChannel(chan,false);
                
                // Register our TaskRunner object with the remoting back-end.
                // The RegisterWellKnownServiceType method will perform that operation, and takes
                // the following parameters:
                //  * A "Type" object which holds a reflection of our TaskRunner object
                //  * The name by which it will be publicly known to clients
                //  * The mode of the object: Singleton means that one object services all incoming client calls
                //                            SingleCall means that a new object services every new client call
                Console.WriteLine("3 -- Registering the TaskRunner object");
                Type theType = new TaskRunner().GetType();  
                RemotingConfiguration.RegisterWellKnownServiceType(theType, "TaskServer", WellKnownObjectMode.Singleton);
                
                // Our server is up. The remoting back-end life-span is dependent on this application's
                // thread, which means that if we exit this Application Domain, our remoting back-end
                // will be killed. So we wait around using a ReadLine() to stop the app from exiting.
                Console.WriteLine("MinMax Server started, press <enter> to exit...");
                Console.ReadLine();
             } catch (Exception e) { 
                 Console.WriteLine("[!] An error occured while initialising the MinMax Server Engine.");
                 Console.WriteLine(e);
             }
        }
    }
}

