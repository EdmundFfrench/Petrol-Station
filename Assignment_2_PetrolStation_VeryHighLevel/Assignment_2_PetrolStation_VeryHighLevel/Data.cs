// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Ed French" file="Data.cs">
//   Copyright of Ed French @ARU
// </copyright>
// <summary>//              
//              The code is intended to represent the very high level of the Petrol station application.
// </summary>
// <rationale>
//                  Variables have been given longer names tha those in the skeleton code provided for readability e.g. 'p' has become 'pump/'.
//                  I have added the class transaction.cs to the solution to represent in individual transaction of a car refuelling at a pump.
//                  The code for assigning a vehicle to a pump is hopefully sufficient to the very high level assignment.
//                  I have used specialised method where possible for readability (and potential unit testing).
//                  I have used lists for rows (lanes) of pumps so that we can easily add to the lists. 
// </rationale>
// <optimisation> 
//                  I have used constants for unchanging values. This makes the code more readable by avoiding the use of 'magic numbers'.
//                  I have used the decimal data type for money values and this is the best accepted data type in c# for that job. 
//                  I have continued to use decimal for other totals that are used in calculations with the money fields for readability. 
//                  Although I am aware that decimal uses more memory but accuracy wins here.
//                  I have used a simple for loop where we know the number of iterations.
// </optimisation> 
// --------------------------------------------------------------------------------------------------------------------
namespace Assignment_2_PetrolStation_VeryHighLevel
{
    using System;
    using System.Collections.Generic;
    using System.Timers;

    /// <summary>
    /// Class to hold the data for the application
    /// </summary>
    public class Data
    {
        /// <summary>
        /// A constant cost per litre decided by code author
        /// </summary>
        public const decimal CostPerLitre = 1.23m;

        /// <summary>
        /// A constant to hold the value for the rate at which a pmp can dispense (litres per second)
        /// </summary>
        public const decimal PumpDispensingRate = 1.5m;

        /// <summary>
        /// A constant to hold the percentage for the commission rate
        /// </summary>
        private const decimal CommissionRate = 0.01m;

        /// <summary>
        /// To hold the pump number 
        /// </summary>
        private static int pumpNumber = 1;

        /// <summary>
        /// Gets the timer to control the various time dependent events
        /// </summary>
        public static Timer Timer { get; private set; }

        /// <summary>
        /// Gets or sets a list of vehicles
        /// </summary>
        public static List<Vehicle> Vehicles { get; set; }

        /// <summary>
        /// Gets or sets a list of pumps
        /// </summary>
        public static List<Pump> PumpsRowOne { get; set; }

        /// <summary>
        /// Gets or sets a list of pumps
        /// </summary>
        public static List<Pump> PumpsRowTwo { get; set; }

        /// <summary>
        /// Gets or sets a list of pumps
        /// </summary>
        public static List<Pump> PumpsRowThree { get; set; }

        /// <summary>
        /// Gets or sets a list of the transaction for each vehicle/pump sale
        /// </summary>
        public static List<Transaction> Transactions { get; set; }

        /// <summary>
        /// Gets or sets the total amount of fuel sold during lifecycle of app
        /// </summary>
        public static decimal TotalLitresDispensedUnleaded { get; set; }

        /// <summary>
        /// Gets or sets the total amount of diesel sold during lifecycle of app
        /// </summary>
        public static decimal TotalLitresDispensedDiesel { get; set; }

        /// <summary>
        /// Gets or sets the total amount of Lpg sold during lifecycle of app
        /// </summary>
        public static decimal TotalLitresDispensedLpg { get; set; }

        /// <summary>
        /// Gets or sets the total amount of fuel sold during lifecycle of app
        /// </summary>
        public static decimal NumberOfLitresSoldForTransaction { get; set; }

        /// <summary>
        /// Gets or sets the total commission earned on all th transactions made during the life cycle of the application
        /// </summary>
        public static decimal TotalCommissionEarned => TotalRevenue * CommissionRate;

        /// <summary>
        /// Gets or sets total number of vehicles that made successful transactions
        /// </summary>
        public static int TotalNumberOfVehiclesServiced { get; set; }

        /// <summary>
        /// Gets or sets total number of vehicles that left the forecourt without getting fuel
        /// </summary>
        public static int TotalNumberOfVehiclesNotServiced { get; set; }

        /// <summary>
        /// The total amount of revenue for all transactions made during hte application life cycle
        /// </summary>
        public static decimal TotalRevenue =>
            (TotalLitresDispensedUnleaded + TotalLitresDispensedDiesel + TotalLitresDispensedLpg) * CostPerLitre;

        /// <summary>
        /// Intialise data for the start of the app
        /// </summary>
        public static void Initialise()
        {
            InitialisePumps();
            InitialiseVehicles();
            Transactions = new List<Transaction>();
        }

        /// <summary>
        /// Assign the vehicle to an available pump
        /// </summary>
        public static void AssignVehicleToPump()
        {
            // If there are no vehicle in the queue simply return.
            if (Vehicles.Count == 0)
            {
                return;
            }

            // Check third row first - a more natural way of assigning pumps
            if (AssignToRowsIfVehicleInQueue(PumpsRowThree))
            {
                return;
            }

            if (AssignToRowsIfVehicleInQueue(PumpsRowTwo))
            {
                return;
            }

            if (AssignToRowsIfVehicleInQueue(PumpsRowOne))
            {
                // no need to return
            }
        }

        /// <summary>
        /// Loop through each row in a backwards direction
        /// </summary>
        /// <param name="pumps">Represent a row of pumps</param>
        /// <returns>true if the are no vehicle left in queue</returns>
        private static bool AssignToRowsIfVehicleInQueue(List<Pump> pumps)
        {
            Pump pumpOne = pumps[0];
            Pump pumpTwo = pumps[1];
            Pump pumpThree = pumps[2];

            Vehicle vehicle;

            if (pumpThree.IsAvailable() && pumpOne.IsAvailable() & pumpTwo.IsAvailable())
            {
                vehicle = Vehicles[0]; // get first vehicle
                Vehicles.RemoveAt(0); // remove vehicles from queue
                pumpThree.AssignVehicle(vehicle); // assign it to the pump

                // if we run out of vehicle in the list leave the for loop
                if (Vehicles.Count == 0)
                {
                    return true;
                }

                return false;
            }

            if (!pumpThree.IsAvailable() && pumpOne.IsAvailable() & pumpTwo.IsAvailable())
            {
                vehicle = Vehicles[0]; // get first vehicle
                Vehicles.RemoveAt(0); // remove vehicles from queue
                pumpTwo.AssignVehicle(vehicle); // assign it to the pump

                // if we run out of vehicle in the list leave the for loop
                if (Vehicles.Count == 0)
                {
                    return true;
                }

                return false;
            }

            if (!pumpThree.IsAvailable() && !pumpTwo.IsAvailable() && pumpOne.IsAvailable())
            {
                vehicle = Vehicles[0]; // get first vehicle
                Vehicles.RemoveAt(0); // remove vehicles from queue
                pumpOne.AssignVehicle(vehicle); // assign it to the pump

                // if we run out of vehicle in the list leave the for loop
                if (Vehicles.Count == 0)
                {
                    return true;
                }

                return false;
            }

            return false;
        }

        /// <summary>
        /// Create a instance of the vehicles class and add it to the list
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event args</param>
        private static void CreateVehicle(object sender, ElapsedEventArgs e)
        {
            if (Program.Timer.Enabled)
            {
                Random random = new Random();
                int randomNumber = 0;

                var vehicle = ReturnARandomVehicleType(random);

                switch (vehicle.VehicleType)
                {
                    case "Car":
                        randomNumber = random.Next(1, 4);
                        break;
                    case "Van":
                        randomNumber = random.Next(1, 3);
                        break;
                    case "HGV":
                        randomNumber = random.Next(1, 2);
                        break;
                }

                vehicle.FuelType = CreateRandomFuelType(randomNumber);

                Vehicles.Add(vehicle);
            
                // Create timer to remove vehicle from queue after 1.5 seconds BUT only if that car iexist in quere *if it has been serviced it will have been removed from the queue
                Timer = new Timer();
                Timer.Interval = random.Next(
                    1000,
                    2000); // random time for vehicle to be serviced in or it will be removed from forecourt  
                Timer.AutoReset = false; // don;t repeat as this is for each individual vehicle
                Timer.Elapsed += RemoveVehicleFromQueue;
                Timer.Enabled = true;
                Timer.Start();
            }
        }

        /// <summary>
        /// Returns a random vehicle type as string
        /// </summary>
        /// <param name="random">A randomiser
        /// </param>
        /// <returns>A randon vehicle type i.e. Car, Van or HGV
        /// </returns>
        private static Vehicle ReturnARandomVehicleType(Random random)
        {
            int vehicleTypeRandomiser = random.Next(1, 4);

            Vehicle vehicle = null;

            switch (vehicleTypeRandomiser)
            {
                case 1:
                    // Create car
                    vehicle = new Car();
                    break;
                case 2:
                    // Create Van
                    vehicle = new Van();
                    break;
                case 3:
                    // Create HGV
                    vehicle = new Hgv();
                    break;
            }

            return vehicle;
        }

        /// <summary>
        /// Set up the 9 pumps to be used throughout application
        /// </summary>
        private static void InitialisePumps()
        {
            FuelType[] fuelTypes = { new Unleaded(), new Diesel(), new Lpg() };  // initialise fuel types

            PumpsRowOne = new List<Pump>(3); // initialise lists
            PumpsRowTwo = new List<Pump>(3); // initialise lists
            PumpsRowThree = new List<Pump>(3); // initialise lists

            PumpsRowOne = AddPumpToRow(fuelTypes);
            PumpsRowTwo = AddPumpToRow(fuelTypes);
            PumpsRowThree = AddPumpToRow(fuelTypes);
        }

        /// <summary>
        /// Add pump to row.
        /// </summary>
        /// <param name="fuelTypes">
        /// The fuel types.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        private static List<Pump> AddPumpToRow(FuelType[] fuelTypes) 
        {
            Pump pump;

            List<Pump> pumpRow = new List<Pump>();

            // Use a simple for loop as we know the number if iterations required before hand
            for (int i = 0; i < 3; i++)
            {
                pump = new Pump(fuelTypes, pumpNumber, pumpRow); // create a new pump
                pumpRow.Add(pump); // add pump to the list
                pumpNumber++;
            }

            return pumpRow;
        }

        /// <summary>
        /// Use timer to create a new vehicle every 1.5 seconds
        /// </summary>
        private static void InitialiseVehicles()
        {
            Vehicles = new List<Vehicle>(5); // Create the list of vehicles 

            Random randon = new Random();

            // https://msdn.microsoft.com/en-us/library/system.timers.timer(v=vs.71).aspx
            Timer = new Timer();
            Timer.Interval = randon.Next(1500, 2200); // a range for a random number
            Timer.AutoReset = true; // keep repeating every 1.5 -2  seconds
            Timer.Elapsed += CreateVehicle; // attached method to timer event
            Timer.Enabled = true;
            Timer.Start();
        }

        /// <summary>
        /// Remove a certain vehicle from the forecourt queue
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private static void RemoveVehicleFromQueue(object sender, ElapsedEventArgs e)
        {
            if (Vehicles.Count > 0)
            {
                Vehicles.RemoveAt(0); // remove vehicle from queue
                TotalNumberOfVehiclesNotServiced++;
            }
        }

        /// <summary>
        /// Create a random fuel type of either Lpg, unleaded or diesel
        /// </summary>
        /// <param name="randomNumber">
        /// The random Number.
        /// </param>
        /// <returns>
        /// The <see cref="FuelType"/>.
        /// </returns>
        private static FuelType CreateRandomFuelType(int randomNumber)
        {
            FuelType fuelType = null;

            switch (randomNumber)
            {
                case 1:
                    // Create diesel
                    fuelType = new Diesel();
                    break;
                case 2:
                    // Create Lpg
                    fuelType = new Lpg();
                    break;
                case 3:
                    // Create unleaded
                    fuelType = new Unleaded();
                    break;
            }

            return fuelType;
        }
    }
}
