// ITask.cs
//
// ************************
// (c) 2001 Luke Venediger
//
// For Comments, Feedback and good South African Beer
// please contact me on the address below:
// lukev123@hotmail.com
// ************************
//
// This is an interface that will be used by clients to 
// create their own tasks. The interface guarantees the following
// two behaviours:
//  * The server will be able to run the task by calling the 
//    known "Run()" method
//  * The client will ensure that his task's functionality is
//    initiated from the "Run()" method
//
// There is also a handy "Identify()" method, which the server
// will use to display some task information.
// 
// This interface is compiled as a seperate library under the
// same namespace, allowing the Task Server administrator to simply
// distribute the interface as a contract for all tasks that will be run
// on his Task Server.
//
// The client will inherit from this class and create their own
// Task object which will be submitted to the server for execution.

namespace TaskServer {
    
    // We must declare this as an interface
    public interface ITask {
        
        // Purpose: To run the task as created by the client,
        // and return a result as the base class Object. This
        // means that the client can have any type of resultant object
        // returned. After all, everything inherits from Object!
        object Run();
        
        // Purpose: To identify the task to the server. It is 
        // purely for server-side informational use.
        string Identify();
    }
}
