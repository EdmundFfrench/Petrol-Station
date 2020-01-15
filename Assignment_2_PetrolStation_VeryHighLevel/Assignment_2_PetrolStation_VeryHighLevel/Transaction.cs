// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Ed French" file="Transaction.cs">
//   Copyright of Ed French @ARU
// </copyright>

// <summary>
//          The code is intended to represent the basic level of the Petrol station application
// </summary>

// <rationale>
//              I have added the class transaction.cs to the solution to represent in individual transaction of a car re-fuelling at a pump
//              I have added vehicle id to the properties of this class for testing and debugging purposes
// </rationale>
// <optimisation> 
//               
// </optimisation> 

// --------------------------------------------------------------------------------------------------------------------

namespace Assignment_2_PetrolStation_VeryHighLevel
{
    /// <summary>
    /// The transaction representing each sale to a vehicle
    /// </summary>
    public class Transaction
    {
        /// <summary>
        /// Counter for calculating next trasnaction ID - static so not changed by instantiation. Initialise to 1 rather than 0 for better UX
        /// </summary>
        private static int nextTransactionId = 1;

        /// <summary>
        /// Initializes a new instance of the <see cref="Transaction"/> class. 
        /// </summary>
        /// <param name="vehicle">
        /// The vehicle.
        /// </param>
        /// <param name="numberOfLitres">
        /// Number of litres sold in each transaction
        /// </param>
        /// <param name="pumpNumber">
        /// The pump Number.
        /// </param>
        public Transaction(Vehicle vehicle, decimal numberOfLitres, int pumpNumber)
        {
            this.Vehicle = vehicle;
            this.NumberOfLitresSold = numberOfLitres;
            this.PumpNumber = pumpNumber;
            this.TransactionId = nextTransactionId++; // increment id in the constructor - it will only get set once when a new vehicle is created.
        }

        /// <summary>
        /// Gets the vehicle associated with each transaction
        /// </summary>
        public Vehicle Vehicle { get; }

        /// <summary>
        /// Gets the number of litres sold for each transaction
        /// </summary>
        public decimal NumberOfLitresSold { get; }

        /// <summary>
        /// Gets the number of the pump from which the fuels was sold
        /// </summary>
        public int PumpNumber { get; }

        /// <summary>
        ///  Gets the Id of the transaction
        /// </summary>
        public int TransactionId { get; }
    }
}
