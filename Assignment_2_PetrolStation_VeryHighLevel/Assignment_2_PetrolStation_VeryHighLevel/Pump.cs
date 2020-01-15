// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Ed French" file="Pump.cs">
//   Copyright of Ed French @ARU
// </copyright>
// <summary>
//          A class representing a single pump in the forecourt.
// </summary>
// <rationale>
//                   
// </rationale>
// <optimisation> 
//                      Current vehicle does not need to public - it should be private.       
//                      Fields have been encapsulated and are only accessible only through a getter as it is set internally in the constructor of the class. 
// </optimisation> 
// --------------------------------------------------------------------------------------------------------------------
namespace Assignment_2_PetrolStation_VeryHighLevel
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Timers;

    using Timer = System.Timers.Timer;

    /// <summary>
    /// Represents an individual fuel pump
    /// </summary>
    public class Pump
    {
        /// <summary>
        /// A constant to hold the value for hte rate at which a pmp can dispense (litres per second)
        /// </summary>
        private const decimal PumpDispensingRate = 1.5m;

        /// <summary>
        /// Initializes a new instance of the <see cref="Pump"/> class. 
        /// Constructor which set the pump''s fuel type
        /// </summary>
        /// <param name="fuelTypes">
        /// The fuel Types.
        /// </param>
        /// <param name="pumpNumber">
        /// The pump Number.
        /// </param>
        /// <param name="pumpRow">
        /// The pump Row.
        /// </param>
        public Pump(FuelType[] fuelTypes, int pumpNumber, List<Pump> pumpRow) 
        {
            // set this property in constructor - we never want to change it again
            this.FuelTypes = fuelTypes;
            this.PumpNumber = pumpNumber;
            this.PumpRow = pumpRow;
        }

        /// <summary>
        /// Gets the timer to control the various time dependent events
        /// </summary>
        public static Timer Timer { get; private set; }

        /// <summary>
        /// Gets the current vehicle.
        /// </summary>
        public Vehicle CurrentVehicle { get; private set; }

        /// <summary>
        /// Gets the pump's fuel type 
        /// </summary>
        public FuelType[] FuelTypes { get; }

        /// <summary>
        /// Gets the # of the number
        /// </summary>
        public int PumpNumber { get; }

        /// <summary>
        /// Gets or sets the row of pump to which this pump belongs
        /// </summary>
        public List<Pump> PumpRow { get; set; }

        /// <summary>
        /// Checks if the pump has a vehicle assigned to it
        /// </summary>
        /// <returns>True if the pump is available</returns>
        public bool IsAvailable()
        {
            // returns TRUE if currentVehicle is NULL, meaning available
            // returns FALSE if currentVehicle is NOT NULL, meaning busy
            return this.CurrentVehicle == null;
        }
        
        /// <summary>
        /// Associate a vehicle with this pump
        /// </summary>
        /// <param name="vehicle">The vehicle to be assigned to this particular pump</param>
        public void AssignVehicle(Vehicle vehicle)
        {
            this.CurrentVehicle = vehicle;
            if (Program.Timer.Enabled)
            {
                Timer = new Timer();
                Timer.Interval = (double)vehicle.FuelTime;
                Timer.AutoReset = true; // repeat
                Timer.Elapsed += this.ReleaseVehicle;
                Timer.Enabled = true;
                Timer.Start();
            }
        }

        /// <summary>
        /// Disassociate vehicle from pump 
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event arguments</param>
        public void ReleaseVehicle(object sender, ElapsedEventArgs e)
        {
            if (this.CheckRouteIsClearToLeavePump())
            {
                if (this.CurrentVehicle != null)
                {
                    Data.NumberOfLitresSoldForTransaction = Math.Round(this.CurrentVehicle.FuelTime / 1000, 2) * PumpDispensingRate;

                    CreateTransaction(this.CurrentVehicle, Data.NumberOfLitresSoldForTransaction, this.PumpNumber);

                    Data.TotalNumberOfVehiclesServiced++; // increase the counter for the number of vehicles serviced

                    if (this.CurrentVehicle != null)
                    {
                        switch (this.CurrentVehicle.FuelType.FuelName)
                        {
                            case "Unleaded":
                                Data.TotalLitresDispensedUnleaded += Data.NumberOfLitresSoldForTransaction;
                                break;

                            case "Lpg":
                                Data.TotalLitresDispensedLpg += Data.NumberOfLitresSoldForTransaction;
                                break;

                            case "Diesel":
                                Data.TotalLitresDispensedDiesel += Data.NumberOfLitresSoldForTransaction;
                                break;
                        }
                    }

                    this.CurrentVehicle = null;
                }
            }
        }

        /// <summary>
        /// Create a record of the sale
        /// </summary>
        /// <param name="vehicle">
        /// The vehicle buying fuel
        /// </param>
        /// <param name="numberOfLitresSoldForTransaction">
        /// How many litres were sold
        /// </param>
        /// <param name="pumpNumber">
        /// The pump Number.
        /// </param>
        private static void CreateTransaction(Vehicle vehicle, decimal numberOfLitresSoldForTransaction, int pumpNumber)
        {
            // create a new transaction and add it to the list. Add 1 to pump number as zero looks weird for user.
            Transaction transaction = new Transaction(vehicle, numberOfLitresSoldForTransaction, pumpNumber);
            
            Data.Transactions.Add(transaction);
        }

        /// <summary>
        /// The spaces to right have to be free to let the vehicle leave the forecourt
        /// </summary>
        /// <returns>True if the vehicle can leave</returns>
        private bool CheckRouteIsClearToLeavePump()
        {
            // if we are on the first two pump in the row we need to check the position in the pump row are free before we can exit
            if (this.PumpNumber == 1 || this.PumpNumber == 4 || this.PumpNumber == 7)
            {
                if (this.PumpRow[1].IsAvailable() && this.PumpRow[2].IsAvailable())
                {
                    return true;
                }
            }

            if (this.PumpNumber == 2 || this.PumpNumber == 5 || this.PumpNumber == 8)
            {
                if (this.PumpRow[2].IsAvailable())
                {
                    return true;
                }
            }

            if (this.PumpNumber == 3 || this.PumpNumber == 6 || this.PumpNumber == 9)
            {
                return true;
            }

            return false;
        }
    }
}
