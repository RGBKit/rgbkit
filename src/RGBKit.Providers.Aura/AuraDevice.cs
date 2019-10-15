using System.Collections.Generic;
using AuraServiceLib;
using RGBKit.Core;

namespace RGBKit.Providers.Aura
{
    /// <summary>
    /// An Aura device
    /// </summary>
    class AuraDevice : IDevice
    {
        /// <summary>
        /// The device name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The lights the device has
        /// </summary>
        public IEnumerable<IDeviceLight> Lights { get => _lights; }

        /// <summary>
        /// The device
        /// </summary>
        private IAuraSyncDevice _device;

        /// <summary>
        /// The lights the device has
        /// </summary>
        private List<AuraDeviceLight> _lights;

        /// <summary>
        /// Creates an Aura device
        /// </summary>
        /// <param name="device">The device</param>
        internal AuraDevice(IAuraSyncDevice device)
        {
            _device = device;
            Name = _device.Name;
            _lights = new List<AuraDeviceLight>();

            foreach (IAuraRgbLight light in _device.Lights)
            {
                _lights.Add(new AuraDeviceLight(light));
            }
        }

        /// <summary>
        /// Applies light changes to the device
        /// </summary>
        public void ApplyLights()
        {
            _device.Apply();
        }
    }
}
