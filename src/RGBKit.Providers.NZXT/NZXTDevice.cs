using System.Collections.Generic;
using RGBKit.Core;
using NZXTSharp.KrakenX;
using System.Linq;
using System;

namespace RGBKit.Providers.NZXT
{
    /// <summary>
    /// An Aura device
    /// </summary>
    class NZXTDevice : IDevice
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
        private KrakenX _device;

        /// <summary>
        /// The lights the device has
        /// </summary>
        private List<NZXTDeviceLight> _lights;

        /// <summary>
        /// Creates an NZXT KrakenX device
        /// </summary>
        /// <param name="device">The device</param>
        internal NZXTDevice(KrakenX device)
        {
            _device = device;
            Name = _device.DeviceID.ToString();
            _lights = new List<NZXTDeviceLight>();

            for (int i = 0; i < 8; i++)
            {
                _lights.Add(new NZXTDeviceLight());
            }
        }

        /// <summary>
        /// Applies light changes to the device
        /// </summary>
        public void ApplyLights()
        {
            //KrakenX Ring
            var header = new byte[4] { 0x22, 0x10, 0b010, 0x00 };
            byte[] color = new byte[0];
            var i = 0;
            foreach (NZXTDeviceLight light in _lights)
            {
                var tmp = new byte[3] { light.Color.G, light.Color.R, light.Color.B };
                color = color.Concat(tmp).ToArray();
                i += 3;
            }
            while (i < 120)
            {
                color = color.Concat(new byte[1] { 0 }).ToArray();
                i++;
            }
            _device.WriteCustom(header.Concat(color).ToArray());
            var buffer = new byte[4] { 0x22, 0x11, 0b010, 0x00 };
            _device.WriteCustom(buffer);
            buffer = new byte[16] { 0x22, 0xa0, 0b010, 0x00, 0x01, 0x00, 0x00, 0x08, 0x00, 0x00, 0x80, 0x00, 0x32, 0x00, 0x00, 0x01 };
            _device.WriteCustom(buffer);

            //KrakenX Logo
            header = new byte[4] { 0x22, 0x10, 0b100, 0x00 };
            _device.WriteCustom(header.Concat(color).ToArray());
            buffer = new byte[4] { 0x22, 0x11, 0b100, 0x00 };
            _device.WriteCustom(buffer);
            buffer = new byte[16] { 0x22, 0xa0, 0b100, 0x00, 0x01, 0x00, 0x00, 0x08, 0x00, 0x00, 0x80, 0x00, 0x32, 0x00, 0x00, 0x01 };
            _device.WriteCustom(buffer);
        }
    }
}
