// TaskRunner.cs
//
// ************************
// (c) 2001 Luke Venediger
//
// For Comments, Feedback and good South African Beer
// please contact me on the address below:
// lukev123@hotmail.com
// ************************
//
// This object will be used to run tasks submitted by
// clients on their behalf. The tasks will be executed in
// the Application Domain of the server.
//
// The TaskRunner object is passed by reference to the client.
// We do not serialize and pass this object to the client. Rather,
// we keep a call-by-reference messaging limitation that will allow
// the client to execute his task in the server's Application Domain.
//
// Note that the TaskRunner accepts all tasks that implement the ITask
// interface. It only wants the two methods: Run() and Identify().

using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

namespace TaskServer {
    
    // Make our class inherit from MarshalByRefObject so that
    // we keep the actual instantiation of the object within
    // the server's Application Domain.    
    public class TaskRunner : MarshalByRefObject {

        // The object holder for our task
        ITask remoteTaskObject;
        
        // Constructor
        public TaskRunner() {
            Console.WriteLine("\n[i] TaskRunner activated");
        }
        
        // Takes as input a task object that implements the
        // ITask interface.
        public string LoadTask(ITask task) {
            
            Console.WriteLine("\n[i] Loading new task...");
            
            // Check that we have a valid object reference
            if(task == null) {
                Console.WriteLine("[e] Task reference is NULL. Task not Loaded.");
                return "[e] Task not loaded.";
            }
            
            // Copy the reference to our class-wide object variable
            remoteTaskObject = task;
            Console.WriteLine("[i] Task has been loaded.");
            Console.WriteLine("[i] Task ID: " + remoteTaskObject.Identify() + "\n");
            return "[i] Task loaded. Welcome to the All Powerful TaskServer.";
        }
        
        // This is the method that the client will use
        // to run the task on his behalf. The task will
        // run in the server's Application Domain.
        public object RunTask() {
            
            Console.WriteLine("\n[i] Running the task...");

            // Run the task, catch the result.
            object result = remoteTaskObject.Run();
            
            Console.WriteLine("[i] Task finished.");
            
            // Return the object that the task gave us on completion.
            return result;
        }
    }
}
