using System.Collections.Generic;

namespace RGBKit.Core
{
    /// <summary>
    /// The device provider interface
    /// </summary>
    public interface IDeviceProvider
    {
        /// <summary>
        /// The provider name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The devices the provider has
        /// </summary>
        IEnumerable<IDevice> Devices { get; }

        /// <summary>
        /// Initializes the provider
        /// </summary>
        void Initialize();

        /// <summary>
        /// Performs a health check on the provider
        /// </summary>
        void PerformHealthCheck();

        /// <summary>
        /// Requests exclusive control over the provider
        /// </summary>
        void RequestControl();

        /// <summary>
        /// Releases exclusive control over the provider
        /// </summary>
        void ReleaseControl();
    }
}
