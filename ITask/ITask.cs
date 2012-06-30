
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
