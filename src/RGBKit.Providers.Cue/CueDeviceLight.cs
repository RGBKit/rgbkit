using System.Drawing;
using Corsair.CUE.SDK;
using RGBKit.Core;

namespace RGBKit.Providers.Cue
{
    /// <summary>
    /// A CUE device light
    /// </summary>
    class CueDeviceLight : IDeviceLight
    {
        /// <summary>
        /// The device lights color
        /// </summary>
        public Color Color { get => GetColor(); set => SetColor(value); }

        /// <summary>
        /// The device light
        /// </summary>
        internal CorsairLedColor _deviceLight;

        /// <summary>
        /// Creates a CUE device light
        /// </summary>
        /// <param name="deviceLight">The device light</param>
        internal CueDeviceLight(CorsairLedColor deviceLight)
        {
            _deviceLight = deviceLight;
        }

        /// <summary>
        /// Gets the color
        /// </summary>
        /// <returns>The color</returns>
        private Color GetColor()
        {
            return Color.FromArgb(_deviceLight.r, _deviceLight.g, _deviceLight.b);
        }

        /// <summary>
        /// Sets the color
        /// </summary>
        /// <param name="value">The color</param>
        private void SetColor(Color value)
        {
            _deviceLight.r = value.R;
            _deviceLight.g = value.G;
            _deviceLight.b = value.B;
        }
    }
}
