using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace MinMax
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]

        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Client());
        }
    }
}
namespace HappyClientWithTask
{

    // Before we create our task it is important that we 
    // specify the Serialisable compiler attribute. Anything that
    // we want to send to the server must be serialized first. So,
    // anything used by the task object including the object itself
    // must be able to be serialized.    
    [Serializable()]

    // This is our task!
    class ClientTask : TaskServer.ITask
    {

        // This is the clien task parser
        // takes as input the function string and calculates the desired polynomial.
        public double min,max;
        private double num1,num2,i,step;
        private string function;
        string result;

        // Constructor, taking the necessairy inputs
        public  ClientTask(double num1, double num2,double step,string function)
        {
            this.num1 = num1;
            this.num2 = num2;
            this.function = function;
            this.step = step;
        }

        // Our implementation of the Run() method.
        
        public object Run()
        {   
         
            for (i = num1; i <  num2; i += step)
            {
                if(i==num1){
                    min=f(num1);
                    max=f(num1);
                }
                if(f(i)<min){min=f(i);}
                if(f(i)>max){max=f(i);}
        }
            result = min + " " + max;
            return (object)result;
        }
        // The Polynomial Parser
        private double f(double x){
        
            int i;
            int j = 0;
            double final=0;
            double fck=0;
            string [] factors = null;
            factors = function.Split(' ');
            foreach (string word in factors)
            {  j++;
            }
     
            for (i=0;i<j;i++){
                fck = Math.Pow(x, (j - 1 - i));
                final += Double.Parse(factors[i])*fck;
            }
            return (final);
        }
        public string Identify()
        {   //The Identification Method, derived form the ITask interface in order to identify our Polynomial.
            string[] factor = null;
            int j=0;
            string def=" " ;
            factor = function.Split(' ');
            string exit = "The Polynomial P(x)=";
            foreach (string word in factor){
                j++;}
            foreach (string word in factor)
            {
                j--;
                if (j > 0)
                    def = def +"("+ word +")"+ "x^(" + j + ")" + "+";
                else
                    def += "("+word +")"+ "x^(" + j + ")"; 
            }
            return (exit+def);
        }
    }
}