// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Ed French" file="Vehicle.cs">
//   Copyright of Ed French @ARU
// </copyright>
// <summary>
//                  A base class representing a generic vehicle contains common features for all vehicles
// </summary>
// <rationale>
//                    I have intiailised the vehicle id to 1 rather than zero for user experience. 
//                    All the properties only have getters and are set in the constructor as they do not need to change after instantiation of object
// </rationale>
// <optmisation>
//                  Fields have been encapsulated and are only  accessible only through a getter as it is set internally in the constructor of the class. 
//                  A I have used double for the fuel time as it is smaller than a decimal ad doesn't need the precision here. This means some casting is needed.
// </optmisation>
// --------------------------------------------------------------------------------------------------------------------

namespace Assignment_2_PetrolStation_VeryHighLevel
{
    using System;

    /// <summary>
    /// The vehicle.
    /// </summary>
    public class Vehicle
    {
        /// <summary>
        /// Counter for calculating next card ID - static so not changed by instantiation. Initialise to 1 rather than 0 for better UX
        /// </summary>
        private static int nextVehicleId = 1;

        /// <summary>
        /// Initializes a new instance of the <see cref="Vehicle"/> class. 
        /// </summary>
        /// <param name="vehicleType">
        /// The vehicle Type.
        /// </param>
        /// <param name="fuelCapacity">
        /// The fuel Capacity.
        /// </param>
        public Vehicle(string vehicleType, int fuelCapacity)
        {
            Random random = new Random();

            this.VehicleId = nextVehicleId++;
            this.VehicleType = vehicleType;
            this.FuelCapacity = fuelCapacity;
            this.StartingFuelAmount = random.Next(0, this.FuelCapacity / 4);

            // The time it take to fill the car is the flow rate multiplied by the remaining capacity left in the tank. 
            this.FuelTime = (this.FuelCapacity - this.StartingFuelAmount) / Data.PumpDispensingRate;
        }

        /// <summary>
        /// Enaum to hold the vehicle types
        /// </summary>
        public enum VehicleTypes
        {
            /// <summary>
            /// Car vehicle type
            /// </summary>
            Car,

            /// <summary>
            /// Van vehicle type
            /// </summary>
            Van,

            /// <summary>
            /// HGV vehicle type
            /// </summary>
            HGV
        }

        /// <summary>
        /// Gets or sets the type of fuel a vehicle consumes i.e. diesel or petrol
        /// </summary>
        public FuelType FuelType { get; set; }

        /// <summary>
        /// Gets the type of vehicle 
        /// </summary>
        public string VehicleType { get; }

        /// <summary>
        /// Gets the time is take to fill the vehicle with fuel. In milliseconds,
        /// </summary>
        public decimal FuelTime { get; }

        /// <summary>
        ///  Gets the Id of the vehicle
        /// </summary>
        public int VehicleId { get; }

        /// <summary>
        /// Gets the vehicle fuel capacity
        /// </summary>
        public int FuelCapacity { get; }

        /// <summary>
        /// Gets the starting fuel amount.
        /// </summary>
        public decimal StartingFuelAmount { get; }
    }
}
