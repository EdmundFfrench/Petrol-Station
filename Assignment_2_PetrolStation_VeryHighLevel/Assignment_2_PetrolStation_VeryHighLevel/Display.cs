// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Ed French" file="Display.cs">
//   Copyright of Ed French @ARU
// </copyright>
// <summary>
//           Outputs the data to the user
//
// </summary>
// <rationale>
//                  I have display totals and each transactions for user experience and testing.
//                  I have added a stopwatch for help testing.
//                  For display purposes I have rounded the decimal to two decimal places
//                  I have set the console cursor position so that the user always see the top information first
//                  they can scroll down to see transaction list
//                  I have set the console to be max size for UX
// </rationale>
// <optmisation>
//                  I have used a simple for loop when we know the number of iterations in advance//                  
//                  I have used different number formatting for screen display and for file writing- the display includes commas for thousands separators but the file output does not,
//                  this is the avoid a problem when opening the files in excel. Again with more time a more elegant reporting system would be developed
// </optmisation>
// --------------------------------------------------------------------------------------------------------------------
namespace Assignment_2_PetrolStation_VeryHighLevel
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// A class which control the UI
    /// </summary>
    public class Display
    {
        /// <summary>
        /// Draw all the elements of the UI
        /// </summary>
        public static void DrawUi()
        {
            Console.Clear(); // clear the UI    
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);

            DrawEscapeMessage();  // show the user they can stop the program by pressing escape
            DrawVehicles(); // draw the vehicle queue to console

            Console.WriteLine();
            Console.WriteLine();

            DrawPumps(); // draw the pumps to the console

            Console.WriteLine();

            DisplayTotals();

            DisplayTransactions(); // display transactions

            Console.SetCursorPosition(0, 0); // make sure we are always at top for UX
        }

        /// <summary>
        /// Tell the user about pressing escape to hold execution
        /// </summary>
        public static void DrawEscapeMessage()
        {
            Console.WriteLine("Broken Petrol Ltd Petrol Station Management Application. " + "       Application run time:" + Program.Stopwatch.Elapsed.ToString("hh\\:mm\\:ss"));
            Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            Console.WriteLine();
            Console.WriteLine("Please press 'p' to pause, 'r' to resume and 'Esc' to stop the application and write output to file....");
            Console.WriteLine();
        }

        /// <summary>
        /// Draw the vehicles.
        /// </summary>
        public static void DrawVehicles()
        {
            Console.WriteLine("Vehicles Queue:");
            Console.WriteLine("===============");

            // Use a simple for loop as we know the number of iterations in advance
            foreach (Vehicle vehicle in Data.Vehicles.ToList())
            {
                Console.Write("Id: {0}, Type: {1}, Fuel Type: {2}", vehicle.VehicleId, vehicle.VehicleType, vehicle.FuelType.FuelName);
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Draw the pump
        /// </summary>
        public static void DrawPumps()
        {
            Console.WriteLine("Pumps Status:");
            Console.WriteLine("=============");

            DrawRow(Data.PumpsRowOne);
            Console.WriteLine();

            DrawRow(Data.PumpsRowTwo);
            Console.WriteLine();

            DrawRow(Data.PumpsRowThree);
        }

        /// <summary>
        /// Display the totals - use rounding for decimals for readability
        /// </summary>
        public static void DisplayTotals()
        {
            Console.WriteLine();
            Console.WriteLine("Totals for key information");
            Console.WriteLine("==========================");
            Console.WriteLine("Total litres of unleaded dispensed: {0}", Math.Round(Data.TotalLitresDispensedUnleaded, 2).ToString("N"));
            Console.WriteLine("Total litres of diesel dispensed:   {0}", Math.Round(Data.TotalLitresDispensedDiesel, 2).ToString("N"));
            Console.WriteLine("Total litres of Lpg dispensed:      {0}", Math.Round(Data.TotalLitresDispensedLpg, 2).ToString("N"));
            Console.WriteLine("Total litres of ALL Fuel:           {0}", Math.Round(Data.TotalLitresDispensedLpg + Data.TotalLitresDispensedDiesel + Data.TotalLitresDispensedUnleaded, 2).ToString("N"));
            Console.WriteLine();
            Console.WriteLine("Total revenue:                     £{0}", Math.Round(Data.TotalRevenue, 2).ToString("N"));
            Console.WriteLine("Commission earned:                 £{0}", Math.Round(Data.TotalCommissionEarned, 2).ToString("N"));
            Console.WriteLine();
            Console.WriteLine("Total # of vehicles serviced:       {0}", Data.TotalNumberOfVehiclesServiced);
            Console.WriteLine("Total # of vehicles NOT serviced:   {0}", Data.TotalNumberOfVehiclesNotServiced);
            Console.WriteLine();
        }

        /// <summary>
        /// The display transaction. Use rounding to two decimal places for readability
        /// </summary>
        public static void DisplayTransactions()
        {
            Console.WriteLine();
            Console.WriteLine("Transactions:");
            Console.WriteLine("=============");

            lock (Data.Transactions)
            {
                // Use for as this will stop error if list is modified whilst we are trying to access it.
                for (int i = 0; i < Data.Transactions.Count; i++)
                {
                    Console.WriteLine(
                        "Id: {0}. {1} ({2}). Litres sold: {3}. Pump: {4}. "
                        + "{5}. Capacity (ltr): {6}. Initial fuel (ltr): {7}. Fuelling time (secs): {8}",
                        Data.Transactions[i].TransactionId,
                        Data.Transactions[i].Vehicle.VehicleType,
                        Data.Transactions[i].Vehicle.VehicleId,
                        Math.Round(Data.Transactions[i].NumberOfLitresSold, 2).ToString("N"),
                        Data.Transactions[i].PumpNumber,
                        Data.Transactions[i].Vehicle.FuelType.FuelName,
                        Data.Transactions[i].Vehicle.FuelCapacity / 1000,  // convert back from millitres
                        Math.Round(Data.Transactions[i].Vehicle.StartingFuelAmount / 1000, 2),
                        Math.Round(Data.Transactions[i].Vehicle.FuelTime / 1000, 2).ToString("N")); // Display in seconds rather than milliseconds for UX/readabily
                }
            }
        }

        /// <summary>
        /// Write the current data to a text fileWriter
        /// </summary>
        public static void WriteOutputToFile()
        {
            var totalsReportPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\PetrolStationTotalsReport" + string.Format("{0:yyyy-MM-dd_HH-mm-ss}", DateTime.UtcNow) + ".csv";
            var fileWriter = new StreamWriter(totalsReportPath);
            WriteTotals(fileWriter);
            fileWriter.Close();

            var transactionReportPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\PetrolStationTransactionsReport" + string.Format("{0:yyyy-MM-dd_HH-mm-ss}", DateTime.UtcNow) + ".csv";
            fileWriter = new StreamWriter(transactionReportPath);
            WriteTransactions(fileWriter);
            fileWriter.Close();
        }

        /// <summary>
        /// The write transactions to a text fileWriter
        /// </summary>
        /// <param name="fileWriter">
        /// The fileWriter.
        /// </param>
        private static void WriteTransactions(StreamWriter fileWriter)
        {
            fileWriter.WriteLine();
            fileWriter.WriteLine("Transactions:");
            fileWriter.WriteLine("=============");

            fileWriter.WriteLine("Id, Type, Veh. Id, Litres sold, Pump #, Fuel Type, Capacity (ltr), Initial fuel (ltr), Fuelling time");

            // Use for each as we do not know how many transactions we have
            foreach (Transaction transaction in Data.Transactions)
            {   
                fileWriter.WriteLine(transaction.TransactionId + ", " + transaction.Vehicle.VehicleType + ", " + transaction.Vehicle.VehicleId + ", " 
                                     + Math.Round(transaction.NumberOfLitresSold, 2).ToString("F") + ", " + transaction.PumpNumber + ", " + transaction.Vehicle.FuelType.FuelName + ", " 
                                     + transaction.Vehicle.FuelCapacity / 1000 + ", " + Math.Round(transaction.Vehicle.StartingFuelAmount / 1000, 2) + ", " +
                                     Math.Round(transaction.Vehicle.FuelTime / 1000, 2).ToString("F")); // Display in seconds rather than milliseconds for UX/readabily. convert millilitres back to litres;
            }
        }

        /// <summary>
        /// The write to fileWriter.
        /// </summary>
        /// <param name="fileWriter">
        /// The file Write.
        /// </param>
        private static void WriteTotals(StreamWriter fileWriter)
        {
            fileWriter.WriteLine("Totals for key information");
            fileWriter.WriteLine("==========================");
            fileWriter.WriteLine("Total litres of unleaded dispensed, {0}", Math.Round(Data.TotalLitresDispensedUnleaded, 2).ToString("F"));
            fileWriter.WriteLine("Total litres of diesel dispensed,   {0}", Math.Round(Data.TotalLitresDispensedDiesel, 2).ToString("F"));
            fileWriter.WriteLine("Total litres of Lpg dispensed,      {0}", Math.Round(Data.TotalLitresDispensedLpg, 2).ToString("F"));
            fileWriter.WriteLine("Total litres of ALL Fuel,           {0}", Math.Round(Data.TotalLitresDispensedLpg + Data.TotalLitresDispensedDiesel + Data.TotalLitresDispensedUnleaded, 2).ToString("F"));
            fileWriter.WriteLine("Total revenue,                      {0}", Math.Round(Data.TotalRevenue, 2).ToString("F"));
            fileWriter.WriteLine("Commission earned,                  {0}", Math.Round(Data.TotalCommissionEarned, 2).ToString("F"));
            fileWriter.WriteLine("Total # of vehicles serviced,       {0}", Data.TotalNumberOfVehiclesServiced);
            fileWriter.WriteLine("Total # of vehicles NOT serviced,   {0}", Data.TotalNumberOfVehiclesNotServiced);
        }

        /// <summary>
        /// Draw row to console
        /// </summary>
        /// <param name="row">a row of pumps</param>
        private static void DrawRow(List<Pump> row)
        {
            foreach (Pump pump in row)
            {
                Console.Write("#{0} ", pump.PumpNumber);
                Console.Write(
                    pump.IsAvailable()
                        ? "FREE".PadRight(24)
                        : "BUSY with Vehicle " + pump.CurrentVehicle.VehicleId
                                               + string.Empty.PadRight(6 - pump.CurrentVehicle.VehicleId.ToString().Length));
                Console.Write(" | ");
            }
        }
    }
}
