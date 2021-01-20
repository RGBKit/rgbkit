using System.Threading;
using System.Collections.Generic;
using RGBKit.Core;
using NZXTSharp.KrakenX;

namespace RGBKit.Providers.NZXT
{
    /// <summary>
    /// The Aura device provider
    /// </summary>
    class NZXTDeviceProvider : IDeviceProvider
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
        private List<NZXTDevice> _devices;

        /// <summary>
        /// If the provider is in exclusive mode
        /// </summary>
        private bool inExcluseMode;

        /// <summary>
        /// Creates an NZXT device provider
        /// </summary>
        public NZXTDeviceProvider()
        {
            Name = "NZXT";
            _devices = new List<NZXTDevice>();

            inExcluseMode = false;
        }

        /// <summary>
        /// Initializes the provider
        /// </summary>
        public void Initialize()
        {
            PerformHealthCheck();

            Thread.Sleep(30000);

            _devices.Add(new NZXTDevice(new KrakenX()));
        }

        /// <summary>
        /// Performs a health check on the provider
        /// </summary>
        public void PerformHealthCheck()
        {
            ///
            /// TO-DO Using NZXTSharp
            ///
        }

        /// <summary>
        /// Requests exclusive control over the provider
        /// </summary>
        public void RequestControl()
        {
            if (!inExcluseMode)
            {
                ///
                /// TO-DO
                ///
            }
        }

        /// <summary>
        /// Releases exclusive control over the provider
        /// </summary>
        public void ReleaseControl()
        {
            if (inExcluseMode)
            {
                ///
                /// TO-DO
                ///
            }
        }
    }
}
