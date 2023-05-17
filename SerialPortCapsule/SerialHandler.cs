using System.Diagnostics; 
using System.IO.Ports;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace SerialPortCapsule
{
    public class SerialHandler
    {


        public SerialPort ser = null ;

        public String RxBuffer = String.Empty;
        public String TxBuffer = String.Empty;


        public SerialHandler()
        {
                        
        }

        public String[] GetSerial()
        {
            return SerialPort.GetPortNames();
        }

        public void CreateSerialPort(String port, int brate, int wto, int rto)
        {
         
            if (ser != null)
            {
                if (ser.IsOpen)
                {
                    ser.Close();
                    ser.Dispose();
                    ser = null;


                    Debug.WriteLine("[Serial] Serial resetted !");

                }
            }

            if (ser == null)
            {
                ser = new SerialPort();

                ser.BaudRate = brate;
                ser.ReadTimeout = rto;
                ser.WriteTimeout = wto;
                ser.PortName = port;


                ser.DataReceived += new SerialDataReceivedEventHandler(Ser_DataReceived);

                

                Debug.WriteLine("[Serial] Serial created !");
            }
        }

        public void OpenSerial()
        {
            if (ser != null)
            {
                ser.Open();
            }
        }
        public void CloseSerial()
        {
            if (ser != null)
            {
                if (ser.IsOpen)
                {
                    ser.Close();
                    ser.Dispose();
                    ser = null;
                }
            }
        }

        public void SerialTxLine (String sdata)
        {

            if (ser.IsOpen)
            {
                ser.WriteLine(sdata);
            }
            
        }

        public void SerialTx ( String sdata)
        {

            if (ser.IsOpen)
            {
                ser.Write(sdata);
            }
        }

        private void Ser_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadExisting();

            RxFilter(indata);


        }

        public void RxFilter ( String rxdata)
        {
            this.RxBuffer = rxdata;
            Debug.WriteLine("[Serial] Data Received: ");
            Debug.Write(RxBuffer);
        }
    }
}