using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using HappyClientWithTask;


namespace MinMax
{
    public partial class Client : Form
    {
        public Client()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {   //Showing the About Form.
            AboutBox1 Box = new AboutBox1();
            Box.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {   //Button Calculate has been pressed.
            //Creating a new Responce form
            Responce RForm = new Responce();
            RForm.Show();
            //We will exit for every exception catched.
            try
            {   //Initializing the ServerTask with the desired input.
                ClientTask ServerTask = new ClientTask(Double.Parse(this.textBox4.Text), (Double.Parse(this.textBox4.Text)+Math.Abs(Double.Parse(this.textBox5.Text) - Double.Parse(this.textBox4.Text)) / 2.0),
                    Double.Parse(this.textBox3.Text), this.textBox1.Text);
                RForm.label1.Text += "\n1 - Getting a reference to the TaskRunner Object";
                //Creating a reference from the Servers already running objects.
                TaskServer.TaskRunner taskRunner = (TaskServer.TaskRunner)Activator.GetObject(typeof(TaskServer.TaskRunner),
                                                            "tcp://" + this.textBox2.Text + ":8085/TaskServer");
                RForm.label1.Text += "\n2 - We have an object reference!";

                //Sending the serialized HappyClientWithTask class and the desired inputs as arguments.
                RForm.label1.Text += "\nSubmitting our task to the server...";
                string response = taskRunner.LoadTask(ServerTask);

                // Display the server's response
                RForm.label1.Text += "\nServer says: " + response;

                
                RForm.label1.Text += "\nRunning the task and awaiting feedback...";
                //Running the desired task at the Servers Domain.
                object serveresult = taskRunner.RunTask();
                string ssresult = (string)serveresult;
                //Spliting the Results so as to be able to make the necessairy comparison.
                string [] split=null;
                
                split = ssresult.Split(' ');

                RForm.label1.Text += "\nThe Server Calculates:\nMinimum :" + Double.Parse(split[0]) + "\nMaximum :" + Double.Parse(split[1]);
                RForm.label1.Text += "\nLoading the other half calculation field to the client...";
                //Creating the clientask, solely on the Client application domain.
                ClientTask clientask = new HappyClientWithTask.ClientTask((Double.Parse(this.textBox4.Text) + Math.Abs(Double.Parse(this.textBox5.Text) - Double.Parse(this.textBox4.Text)) / 2.0), Double.Parse(this.textBox5.Text), Double.Parse(this.textBox3.Text), this.textBox1.Text);
                RForm.label1.Text += "\nTask Loaded, Running the Task at the Client...";
                //Running the Task at the Client Application domain.
                object clientresult = clientask.Run();
                RForm.label1.Text += "\nThe client Calculates:\nMinimum "+clientask.min+"\nMaximum "+clientask.max +".";
                //Making the comparison of the 2 minimum and maximums at the Clients application Domain.
                if (clientask.max >= Double.Parse(split[1]))
                {
                    RForm.label1.Text += "\nTherefore, The Maximum of the whole field is :" + clientask.max + ".";
                }
                else
                {
                    RForm.label1.Text += "\nTherefore, The Maximum of the whole field is :" + Double.Parse(split[1]) + ".";
                }

                if (clientask.min >= Double.Parse(split[0]))
                {
                    RForm.label1.Text += "\nAnd The Minimum of the whole field is:" + Double.Parse(split[0]) + ".";
                }
                else
                {
                    RForm.label1.Text += "\nAnd The Minimum of the whole field is:" + clientask.min + ".";
                }
            

               

            }
            catch (Exception z)
            {

                
                RForm.label1.Text = RForm.label1.Text + "\n[e] An exception occurred.\n";
                RForm.label1.Text = RForm.label1.Text + z;
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Button Connect has been pressed
            MinMax.Server_Client_Dialogue ConnectForm = new MinMax.Server_Client_Dialogue();
            // Create a new Connect Form.
            ConnectForm.Show();
            

            ConnectForm.label1.Text = "1 - Connecting to the Server at the IP :"+this.textBox2.Text;
            ConnectForm.label1.Text = ConnectForm.label1.Text + "\n2 - Opening TCP Channel.";
            TcpChannel chan = new TcpChannel();          
            try {
                // Attempt to get a connection and a
                // channel opening to the TaskServer
       
                
                
                ChannelServices.RegisterChannel(chan,false);
                ConnectForm.label1.Text += "\n3 - Registering TCP Channel.";
           
                TaskServer.TaskRunner taskRunner = (TaskServer.TaskRunner)Activator.GetObject(typeof(TaskServer.TaskRunner),
                                                              "tcp://" + this.textBox2.Text + ":8085/TaskServer");
                //The Server is Not Running if taskRunner is null.
                if (taskRunner == null)
                {
                    ConnectForm.label1.Text += "\nError -  Could not locate server.";
                    chan.StopListening(true);
                    return;
                }
                
                
                
                
                ConnectForm.label1.Text += "\nClick Calculate to begin the Calculation.";
                
            } catch (Exception t) {
                
               
                ConnectForm.label1.Text = ConnectForm.label1.Text +"\n[e] An exception occurred." ;
                ConnectForm.label1.Text= ConnectForm.label1.Text + t;
                chan.StopListening(true);
            }
            
        }

       
        }
    }

