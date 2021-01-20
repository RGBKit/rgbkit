using System.Linq;
using System.Collections.Generic;
using System.Timers;

namespace RGBKit.Core
{
    /// <summary>
    /// The RGB Kit service
    /// </summary>
    public class RGBKitService : IRGBKitService
    {
        /// <summary>
        /// The list of device providers
        /// </summary>
        public IEnumerable<IDeviceProvider> DeviceProviders { get => _deviceProviders; }

        /// <summary>
        /// The device providers
        /// </summary>
        private List<IDeviceProvider> _deviceProviders;

        /// <summary>
        /// The health check timer
        /// </summary>
        private Timer _healthCheckTimer;

        /// <summary>
        /// Creates an instance of the RGB Kit service
        /// </summary>
        public RGBKitService()
        {
            _deviceProviders = new List<IDeviceProvider>();
            _healthCheckTimer = new Timer(15000);
            _healthCheckTimer.Elapsed += HealthCheckTimer_Elapsed;
        }

        /// <summary>
        /// Adds a device provider to the service
        /// </summary>
        /// <param name="provider">The provider to add</param>
        public void AddProvider(IDeviceProvider provider)
        {
            _deviceProviders.Add(provider);
        }

        /// <summary>
        /// Initializes the RGB Kit service
        /// </summary>
        public void Initialize()
        {
            foreach (var provider in DeviceProviders)
            {
                provider.Initialize();

                foreach (var device in provider.Devices)
                {
                    var duplicateDevices = provider.Devices.Where(d => d.Name == device.Name).ToArray();

                    if (duplicateDevices.Length > 1)
                    {
                        for (int i = 0; i < duplicateDevices.Length; i++)
                        {
                            duplicateDevices[i].Name = duplicateDevices[i].Name + " " + (i + 1);
                        }
                    }
                }

                provider.RequestControl();
            }

            _healthCheckTimer.Start();
        }

        /// <summary>
        /// Occurs during a health check cycle
        /// </summary>
        /// <param name="sender">The sending object</param>
        /// <param name="e">The arguments</param>
        private void HealthCheckTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            foreach (var provider in DeviceProviders)
            {
                provider.PerformHealthCheck();
                provider.RequestControl();
            }
        }
    }
}
