using System.Drawing;
using RGBKit.Core;

namespace RGBKit.Providers.NZXT
{
    /// <summary>
    /// An Aura device light
    /// </summary>
    class NZXTDeviceLight : IDeviceLight
    {
        /// <summary>
        /// The device lights color
        /// </summary>
        public Color Color { get => GetColor(); set => SetColor(value); }

        /// <summary>
        /// The device light color
        /// </summary>
        public Color _device_color;

        internal NZXTDeviceLight()
        {
            _device_color = Color.FromArgb(255, 255, 255);
        }

        /// <summary>
        /// Gets the color
        /// </summary>
        /// <returns>The color</returns>
        private Color GetColor()
        {
            return Color.FromArgb(_device_color.R, _device_color.G, _device_color.B);
        }

        /// <summary>
        /// Sets the color
        /// </summary>
        /// <param name="value">The color</param>
        private void SetColor(Color value)
        {
            _device_color = Color.FromArgb(value.R, value.G, value.B);
        }
    }
}
