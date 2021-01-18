using System.Threading;
using System.Collections.Generic;
using System.Diagnostics;
using AuraServiceLib;
using RGBKit.Core;

namespace RGBKit.Providers.Aura
{
    /// <summary>
    /// The Aura device provider
    /// </summary>
    class AuraDeviceProvider : IDeviceProvider
    {
        /// <summary>
        /// The provider name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The devices the provider has
        /// </summary>
        public IEnumerable<IDevice> Devices { get => _devices; }

        /// <summary>
        /// The devices the provider has
        /// </summary>
        private List<AuraDevice> _devices;

        /// <summary>
        /// The provider sdk
        /// </summary>
        private IAuraSdk _sdk;

        /// <summary>
        /// If the provider is in exclusive mode
        /// </summary>
        private bool inExcluseMode;

        /// <summary>
        /// Creates an Aura device provider
        /// </summary>
        public AuraDeviceProvider()
        {
            Name = "AURA";
            _devices = new List<AuraDevice>();
            _sdk = new AuraSdk();

            inExcluseMode = false;
        }

        /// <summary>
        /// Initializes the provider
        /// </summary>
        public void Initialize()
        {
            PerformHealthCheck();

            Thread.Sleep(10000);

            foreach (IAuraSyncDevice device in _sdk.Enumerate(0))
            {
                _devices.Add(new AuraDevice(device));
            }
        }

        /// <summary>
        /// Performs a health check on the provider
        /// </summary>
        public void PerformHealthCheck()
        {
            var lightingServiceRunning = Process.GetProcessesByName("LightingService").Length != 0;

            while (!lightingServiceRunning)
            {
                Thread.Sleep(1000);
                lightingServiceRunning = Process.GetProcessesByName("LightingService").Length != 0;
            }
        }

        /// <summary>
        /// Requests exclusive control over the provider
        /// </summary>
        public void RequestControl()
        {
            if (!inExcluseMode)
            {
                _sdk.SwitchMode();
            }
        }

        /// <summary>
        /// Releases exclusive control over the provider
        /// </summary>
        public void ReleaseControl()
        {
            if (inExcluseMode)
            {
                _sdk.SwitchMode();
            }
        }
    }
}
