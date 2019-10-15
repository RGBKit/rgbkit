using System.Collections.Generic;
using Corsair.CUE.SDK;
using RGBKit.Core;

namespace RGBKit.Providers.Cue
{
    /// <summary>
    /// A CUE device
    /// </summary>
    class CueDevice : IDevice
    {
        /// <summary>
        /// The device name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The lights the device has
        /// </summary>
        public IEnumerable<IDeviceLight> Lights { get => GetLights(); }

        /// <summary>
        /// The device index
        /// </summary>
        private int _deviceIndex;

        /// <summary>
        /// The device
        /// </summary>
        private CorsairDeviceInfo _device;

        /// <summary>
        /// The lights the device has
        /// </summary>
        private List<CueDeviceLight> _lights;

        /// <summary>
        /// Creates a CUE device
        /// </summary>
        /// <param name="deviceIndex">The device index</param>
        internal CueDevice(int deviceIndex)
        {
            _deviceIndex = deviceIndex;
            _device = CUESDK.CorsairGetDeviceInfo(_deviceIndex);
            Name = _device.model;
            _lights = new List<CueDeviceLight>();

            var positions = CUESDK.CorsairGetLedPositionsByDeviceIndex(_deviceIndex);

            foreach (var position in positions.pLedPosition)
            {
                _lights.Add(new CueDeviceLight(new CorsairLedColor() { ledId = position.ledId }));
            }
        }

        /// <summary>
        /// Applies light changes to the device
        /// </summary>
        public void ApplyLights()
        {
            var buffer = new CorsairLedColor[_lights.Count];

            for (int i = 0; i < _lights.Count; i++)
            {
                buffer[i] = _lights[i]._deviceLight;
            }

            CUESDK.CorsairSetLedsColorsBufferByDeviceIndex(_deviceIndex, buffer.Length, buffer);
            CUESDK.CorsairSetLedsColorsFlushBuffer();
        }

        /// <summary>
        /// Gets the lights the device has
        /// </summary>
        /// <returns>The device lights</returns>
        private IEnumerable<IDeviceLight> GetLights()
        {
            var buffer = new CorsairLedColor[_lights.Count];

            for (int i = 0; i < _lights.Count; i++)
            {
                buffer[i] = _lights[i]._deviceLight;
            }

            CUESDK.CorsairGetLedsColorsByDeviceIndex(_deviceIndex, buffer.Length, buffer);
            return _lights;
        }
    }
}
