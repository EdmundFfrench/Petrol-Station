// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Ed French" file="HGV.cs">
//   Copyright of Ed French @ARU
// </copyright>
// <summary>
//              This class represent a HGV which inherits from the base class vehicle - so we can add HGVs to a list of vehicles
// </summary>
// <rationale>
//              
// </rationale>
// <optmisation>
//                  the fuel capacity is defined as a constant as it is unchanging
// </optmisation>
// --------------------------------------------------------------------------------------------------------------------
namespace Assignment_2_PetrolStation_VeryHighLevel
{
    using System;

    /// <summary>
    /// A car class that inherits from the base class Vehicle
    /// </summary>
    public class Hgv : Vehicle
    {
        /// <summary>
        /// The maximum fuel capacity.
        /// </summary>
        private new const int FuelCapacity = 150000; // millitres for more accurate calcs

        /// <summary>
        /// Initializes a new instance of the <see cref="Hgv"/> class. 
        /// Constructor
        /// </summary>
        public Hgv()
            : base(VehicleTypes.HGV.ToString(), FuelCapacity)
        {
        }
    }
}