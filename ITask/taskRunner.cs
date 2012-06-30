
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
           Console.WriteLine("\nTaskRunner activated.");
        }
        
        // Takes as input a task object that implements the
        // ITask interface.
        public string LoadTask(ITask task) {
            
            Console.WriteLine("\n1 - Loading new task...");
            
            // Check that we have a valid object reference
            if(task == null) {
                Console.WriteLine("\nError Task reference is NULL. Task not Loaded.");
                return "\nError Task not loaded.";
            }
            
            // Copy the reference to our class-wide object variable
            remoteTaskObject = task;
            Console.WriteLine("\n1 - Task has been loaded.");
            Console.WriteLine("\nTask ID: " + remoteTaskObject.Identify() + "\n");
            return "Task loaded. Connected with the Server.\n Task consists of "+remoteTaskObject.Identify();
        }
        
        // This is the method that the client will use
        // to run the task on his behalf. The task will
        // run in the server's Application Domain.
        public object RunTask() {
            
            Console.WriteLine("\nRunning the task...");
            
            // Run the task, catch the result.
            object result = remoteTaskObject.Run();
            
            Console.WriteLine("\nTask finished.");
            
            // Return the object that the task gave us on completion.
            return result;
        }
    }
}
