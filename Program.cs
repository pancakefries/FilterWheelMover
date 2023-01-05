// // See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

using System;
// using ASCOM.DeviceInterface;

namespace FilterWheelMover
{
    class Program {
        static void Main(string[] args) {
            short pos;
            string pos_str;
            const int FILTER_WHEEL_TIME_OUT = 3;
            System.DateTime StartTime;
            // ASCOM.DeviceInterface.IFilterWheelV2 m_FilterWheel;

            // Sleep for 3 seconds to allow for filter wheel startup movement
            System.Threading.Thread.Sleep(3000);

            Console.WriteLine("Enter q to quit");
            while (true) {
                Console.Write("Enter a filter position: ");
                pos_str = Console.ReadLine();

                try {
                    pos = (short)int.Parse(pos_str);
                    if (pos < 0 | pos > 9) {
                        throw new ArgumentException("Filter position must be between 0 - 9");
                    }

                    // m_FilterWheel.Position = pos;
                    StartTime = DateTime.Now;
                    Console.Write("Setting filter wheel to position " + pos_str + "...");

                    do {
                        System.Threading.Thread.Sleep(100);
                        Console.WriteLine("Waiting...");
                    } while (!(
                        // (m_FilterWheel.Position == pos) | 
                        (DateTime.Now.Subtract(StartTime).TotalSeconds > FILTER_WHEEL_TIME_OUT)));

                    Console.Write("\n");
                    Console.WriteLine("Set position to: " + pos_str);
                } catch {
                    if (pos_str.Equals("q") | pos_str.Equals("Q")){
                        return;
                    }

                    Console.WriteLine("Not a valid filter position, must be between 0 - 9");
                }
            }
        }
    }
}