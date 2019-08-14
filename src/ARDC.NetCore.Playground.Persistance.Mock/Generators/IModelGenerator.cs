using System.Collections.Generic;

namespace ARDC.NetCore.Playground.Persistence.Mock.Generators
{
    /// <summary>
    /// Describe methods to Generate random data.
    /// </summary>
    /// <typeparam name="T">The class for which data should be generated</typeparam>
    public interface IModelGenerator<T> where T : class
    {
        /// <summary>
        /// Get a single random instance.
        /// </summary>
        /// <returns>A new instance containing random data</returns>
        T Get();

        /// <summary>
        /// Get many random instances.
        /// </summary>
        /// <param name="count">The amount of instances to be generated</param>
        /// <returns>A list containing the generated instances</returns>
        IList<T> Get(int count = 2);

        /// <summary>
        /// Get a random amount of random instances.
        /// </summary>
        /// <param name="min">The minimium amount of instances to be generated</param>
        /// <param name="max">The maximumn amount of instances to be generated</param>
        /// <returns>A list containing the generated instances</returns>
        IList<T> Get(int min = 2, int max = 1000);
    }
}
