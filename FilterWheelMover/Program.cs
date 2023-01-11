// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

using ASCOM.Common.DeviceInterfaces;

namespace FilterWheelMover
{
    class Program
    {
        static void Main(string[] args)
        {
            short pos;
            string pos_str;
            const int FILTER_WHEEL_TIME_OUT = 3;
            DateTime StartTime;
            
            var chooser = new ASCOM.Com.Chooser();
            chooser.DeviceType = ASCOM.Common.DeviceTypes.FilterWheel;
            var prog_id = chooser.Choose("ASCOM.SBIG.USB_FW.FilterWheel");
            Console.WriteLine(prog_id);
            //IFilterWheelV2 m_FilterWheel = new ASCOM.Com.DriverAccess.FilterWheel(prog_id);
            var m_FilterWheel = new ASCOM.Com.DriverAccess.FilterWheel(prog_id);
            m_FilterWheel.Connected = true;


            // Sleep for 3 seconds to allow for filter wheel startup movement
            Thread.Sleep(3000);

            Console.WriteLine("Enter q to quit");
            while (true)
            {
                Console.Write("Enter a filter position: ");
                pos_str = Console.ReadLine();

                try
                {
                    pos = (short)int.Parse(pos_str);
                    if (pos < 0 | pos > 9)
                    {
                        throw new ArgumentException("Filter position must be between 0 - 9");
                    }

                    m_FilterWheel.Position = pos;
                    StartTime = DateTime.Now;
                    Console.Write("Setting filter wheel to position " + pos_str + "...");

                    do
                    {
                        Thread.Sleep(100);
                        Console.WriteLine("Waiting...");
                    } while (!(
                        (m_FilterWheel.Position == pos) |
                        (DateTime.Now.Subtract(StartTime).TotalSeconds > FILTER_WHEEL_TIME_OUT)));

                    Console.Write("\n");
                    Console.WriteLine("Set position to: " + pos_str);
                }
                catch
                {
                    if (pos_str.Equals("q") | pos_str.Equals("Q"))
                    {
                        return;
                    }

                    Console.WriteLine("Not a valid filter position, must be between 0 - 9");
                }
            }
        }
    }
}