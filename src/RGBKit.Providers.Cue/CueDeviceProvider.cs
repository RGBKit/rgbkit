using System.Threading;
using System.Collections.Generic;
using System.Diagnostics;
using Corsair.CUE.SDK;
using RGBKit.Core;

namespace RGBKit.Providers.Cue
{
    /// <summary>
    /// The CUE device provider
    /// </summary>
    class CueDeviceProvider : IDeviceProvider
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
        private List<CueDevice> _devices;

        /// <summary>
        /// Creates a Corsair device provider
        /// </summary>
        public CueDeviceProvider()
        {
            Name = "CUE";
            _devices = new List<CueDevice>();
        }

        /// <summary>
        /// Initializes the provider
        /// </summary>
        public void Initialize()
        {
            PerformHealthCheck();

            Thread.Sleep(30000);

            for (int i = 0; i < CUESDK.CorsairGetDeviceCount(); i++)
            {
                _devices.Add(new CueDevice(i));
            }
        }

        /// <summary>
        /// Performs a health check on the provider
        /// </summary>
        public void PerformHealthCheck()
        {
            var cueRunning = Process.GetProcessesByName("iCUE").Length != 0;

            while (!cueRunning)
            {
                Thread.Sleep(1000);
                cueRunning = Process.GetProcessesByName("iCUE").Length != 0;
            }

            CUESDK.CorsairGetDeviceCount();
            var error = CUESDK.CorsairGetLastError();

            while (error == CorsairError.CE_ServerNotFound ||
                error == CorsairError.CE_ProtocolHandshakeMissing)
            {
                CUESDK.CorsairPerformProtocolHandshake();
                Thread.Sleep(1000);
                error = CUESDK.CorsairGetLastError();
            }
        }

        /// <summary>
        /// Requests exclusive control over the provider
        /// </summary>
        public void RequestControl()
        {
            CUESDK.CorsairRequestControl(CorsairAccessMode.CAM_ExclusiveLightingControl);
        }

        /// <summary>
        /// Releases exclusive control over the provider
        /// </summary>
        public void ReleaseControl()
        {
            CUESDK.CorsairReleaseControl(CorsairAccessMode.CAM_ExclusiveLightingControl);
        }
    }
}
