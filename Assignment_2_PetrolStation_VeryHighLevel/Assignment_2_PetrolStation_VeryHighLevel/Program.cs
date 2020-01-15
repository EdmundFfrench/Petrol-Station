// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Ed French" file="Program.cs">
//   Copyright of Ed French @ARU
// </copyright>
// <summary>
//                  The main entry for the application and main application loop
// </summary>

// <rationale>
//                  N.B. Code is based on the skeletal code supplied as a download in Canvas
//                  I have added a method to display totals and transactions.
//                  I have added code to pause, resume and exit the program if the user presses 'p', 'r' or 'Esc'. This will help debugging and testing.
//                  If a user presses 'Esc' then the totals and the transaction list will be written as two time stamped CSV files that can then be opened in excel.
//                  This will facilitate testing and allow further analysis. 
//                  IN FUTURE DEVELOPMENT if I had time I would write directly to excel and neaten the report up. 
//                  Again if I had time I would have written the code to restart the application on another key press.
//                  I have kept display functionality separate from data manipulation
// </rationale>
// <optimisation> 
//                  Stop have a private setter as we do not want anything else touching it
// </optimisation> 
// --------------------------------------------------------------------------------------------------------------------
namespace Assignment_2_PetrolStation_VeryHighLevel
{
    using System;
    using System.Diagnostics;
    using System.Timers;

    /// <summary>
    /// A program for an automated demo of a petrol station management app
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Getss the global timer for program loop
        /// </summary>
        public static Timer Timer { get; private set; }

        /// <summary>
        /// Gets the stopwatch for displaying the run time of the application
        /// </summary>
        public static Stopwatch Stopwatch { get; private set; }

        /// <summary>
        /// Start the program running
        /// </summary>
        /// <param name="args">start up args</param>
        public static void Main(string[] args)
        {
            // Stop watch is for the application run time data in the top right of the display.
            Stopwatch = new Stopwatch();
            Stopwatch.Start();

            Timer = new Timer();
            
            Data.Initialise();

            Timer.Interval = 500;
            Timer.AutoReset = true; // repeat every .5 seconds
            Timer.Elapsed += RunProgramLoop; // set up event
            Timer.Enabled = true;
            Timer.Start();

            // Got logic idea from:
            // https://social.msdn.microsoft.com/Forums/en-US/8fd189ca-576b-438e-af88-25fa16f9df23/detect-key-press?forum=netfxbcl
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    // Pause application
                    ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);
                    if (consoleKeyInfo.Key == ConsoleKey.P)
                    {
                        Timer.Stop();
                        Data.Timer.Stop();
                        Pump.Timer.Stop();
                        Stopwatch.Stop();
                    }

                    // Resume application running
                    if (consoleKeyInfo.Key == ConsoleKey.R)
                    {
                        Timer.Start();
                        Data.Timer.Start();
                        Pump.Timer.Start();
                        Stopwatch.Start();
                    }

                    // Exit application
                    if (consoleKeyInfo.Key == ConsoleKey.Escape)
                    {
                        Timer.Stop();
                        Data.Timer.Stop();
                        Pump.Timer.Stop();
                        Stopwatch.Stop();
                        Display.WriteOutputToFile();
                        Environment.Exit(0);
                    }
                }
            }
        }

        /// <summary>
        /// Refresh the UI every 0.5 seconds or so and assign new vehicles to pumps
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event args</param>
        public static void RunProgramLoop(object sender, ElapsedEventArgs e)
        {
            if (Timer.Enabled)
            {
                Display.DrawUi();
                Data.AssignVehicleToPump(); // try to assign vehicle to a pump
            }
        }
    }
}
