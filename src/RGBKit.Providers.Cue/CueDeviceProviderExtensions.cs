using RGBKit.Providers.Cue;

namespace RGBKit.Core
{
    /// <summary>
    /// The RGB Kit extension methods
    /// </summary>
    public static class CueDeviceProviderExtensions
    {
        /// <summary>
        /// Uses the CUE device provider
        /// </summary>
        /// <param name="lightSync">The RGB Kit instance to add the provider to</param>
        public static void UseCue(this IRGBKitService rgbKit)
        {
            rgbKit.AddProvider(new CueDeviceProvider());
        }
    }
}
