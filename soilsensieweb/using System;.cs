using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace micetrap
{
    public partial class Form1 : Form
    {
        private DateTime? mouseCapture1 = null;
        private DateTime? mouseCapture2 = null;
        private Timer timer1;
        private Timer timer2;


        

        
        public Form1()
        {
            InitializeComponent();

            timer1 = new Timer();
            timer2 = new Timer();
            timer1.Interval = 10000000; // 1 second
            timer1.Tick += Timer1_Tick;
            timer2.Interval = 10000000; // 1 second
            timer2.Tick += Timer2_Tick;
            
        }

        private void info1_Click(object sender, EventArgs e)
        {
            info1r.Visible = true;
            info1.Visible = false;
            label1.Visible = true;
            label3.Visible = true;
            
        }

        private void info1r_Click(object sender, EventArgs e)
        {
            info1r.Visible = false;
            info1.Visible = true;
            label1.Visible = false;
            label3.Visible = false;
           

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!serialPort1.IsOpen)
            {
                serialPort1.Open();
            }
            MessageBox.Show("Connected");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!serialPort1.IsOpen)
            {
                serialPort1.Close();
                

            }
            MessageBox.Show("Disconnected");
        }

        private void InitializeSerialPort()
        {
            serialPort1 = new SerialPort();
            serialPort1.PortName = "COM4"; // Change this to match the COM port of your Arduino board
            serialPort1.BaudRate = 9600;
            serialPort1.DataReceived += new SerialDataReceivedEventHandler(serialPort1_DataReceived);

        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string data = serialPort1.ReadLine().Trim();
            if (data == "MOUSE DETECTED")
            {
                label1.Invoke(new Action(() => {
                    label1.Text = "MOUSE DETECTED ON TRAP 1";
                    label1.BackColor = System.Drawing.Color.Yellow;
                }));

                mouseCapture1 = DateTime.Now;
                timer1.Stop();

                panel1.Invoke(new Action(() =>
                {
                    
                    panel2.Visible = true;
                }));
                panel2.Invoke(new Action(() =>
                {
                   
                    panel1.Visible =false;
                }));
                
            }
            else if (data == "NO MOUSE INSIDE")
            {
                label1.Invoke(new Action(() => {
                    label1.Text = "No Mouse ON TRAP1";
                    label1.BackColor = System.Drawing.Color.White;
                }));

                panel1.Invoke(new Action(() =>
                {

                    panel2.Visible = false;
                }));
                panel2.Invoke(new Action(() =>
                {

                    panel1.Visible = true;
                }));
                
                mouseCapture1 = null;
                
            }
            else if (data == "MOUSE DETECTED2")
            {
                label2.Invoke(new Action(() => {
                    label2.Text = "MOUSE DETECTED ON TRAP 2";
                    label2.BackColor = System.Drawing.Color.Yellow;
                }));

                mouseCapture2 = DateTime.Now;
                timer2.Stop();
                panel7.Invoke(new Action(() =>
                {

                    panel7.Visible = true;
                }));
                panel8.Invoke(new Action(() =>
                {

                    panel8.Visible = false;
                }));
            }
            else if (data == "NO MOUSE INSIDE2")
            {
                label2.Invoke(new Action(() => {
                    label2.Text = "NO MOUSE ON TRAP 2";
                    label2.BackColor = System.Drawing.Color.White;
                }));
                panel7.Invoke(new Action(() =>
                {

                    panel7.Visible = false;
                }));
                panel8.Invoke(new Action(() =>
                {

                    panel8.Visible = true;
                }));
                mouseCapture2 = null;
                ;

            }
            else if (data == "SENSOR ERROR")
            {
                mouseCapture1 = null;
                mouseCapture2 = null;
                label3.Invoke(new Action(() => {
                    label3.Text = "Waiting for Mouse...........";
                }));
                label4.Invoke(new Action(() => {
                    label4.Text = "Waiting for Mouse...........";
                }));
            }

            UpdateLabels();
        }
        private void UpdateLabels()
        {
            if (mouseCapture1.HasValue)
            {
                label3.Invoke(new Action(() =>
                {
                    label3.Text = $"Mouse captured  at {mouseCapture1.Value.ToString("h:mm tt, MMMM d, yyyy")}";

                }));
            }
            else
            {
                label3.Invoke(new Action(() =>
                {
                    label3.Text = "Waiting for Mouse...........";
                 
                }));
            }

            if (mouseCapture2.HasValue)
            {
                label4.Invoke(new Action(() =>
                {
                    label4.Text = $"Mouse captured at {mouseCapture2.Value.ToString("h:mm tt, MMMM d, yyyy")}";
                    timer2.Stop();
                }));
            }
            else
            {
                label4.Invoke(new Action(() =>
                {
                    label4.Text = "Waiting for Mouse...........";
                  
                }));
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            label3.Text = "Waiting for Mouse...........";
           
        }

        private void Timer2_Tick(object sender, EventArgs e)
        {
            label4.Text = "Waiting for Mouse...........";
            
        }


        private void info2r_Click_1(object sender, EventArgs e)
        {
            info2.Visible = true;
            info2r.Visible = false;
            label2.Visible = false;
            label4.Visible = false;

        }

        private void info2_Click_1(object sender, EventArgs e)
        {
            label2.Visible = true;
            label4.Visible = true;
            info2.Visible = false;
            info2r.Visible = true;

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void panel10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            panel_1.Visible = true;
            panel_main.Visible = false; 
        }

        private void button7_Click(object sender, EventArgs e)
        {
            panel_main.Visible = true ;
        }

        private void button_HOME_Click(object sender, EventArgs e)
        {
            com_pan.Visible = true;
        }

        private void button9_Click(object sender, EventArgs e)
        {

            com_pan.Visible = false;
        }

        private void btn_bt_Click(object sender, EventArgs e)
        {
            if (serialPort1.PortName == "COM4")
            {
                serialPort1.PortName = "COM8";
            }
            else
            {
                serialPort1.PortName = "COM4";
            }

            MessageBox.Show("bluetooth mode");
        }

        private void btn_wire_Click(object sender, EventArgs e)
        {
            if (serialPort1.PortName == "COM8")
            {
                serialPort1.PortName = "COM4";
            }
            else
            {
                serialPort1.PortName = "COM4";
            }
            MessageBox.Show("WIRE Mode");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialPort1.IsOpen)
                {
                    serialPort1.Close();
                    MessageBox.Show("Serial port closed.");
                }
                else
                {
                    MessageBox.Show("Serial port is not open.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error closing serial port: " + ex.Message);
            }
        }
    }
}

