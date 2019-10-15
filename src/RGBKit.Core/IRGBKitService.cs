using System.Collections.Generic;

namespace RGBKit.Core
{
    /// <summary>
    /// The RGB Kit service definition
    /// </summary>
    public interface IRGBKitService
    {
        /// <summary>
        /// The list of device providers
        /// </summary>
        IEnumerable<IDeviceProvider> DeviceProviders { get; }

        /// <summary>
        /// Adds a device provider to the service
        /// </summary>
        /// <param name="provider">The provider to add</param>
        void AddProvider(IDeviceProvider provider);

        /// <summary>
        /// Initializes the RGB Kit service
        /// </summary>
        void Initialize();
    }
}
