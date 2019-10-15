using System;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace RGBKit.Core
{
    /// <summary>
    /// The RGB Kit extension methods
    /// </summary>
    public static class RGBKitExtensions
    {
        /// <summary>
        /// Configures RGB Kit
        /// </summary>
        /// <param name="builder">The host builder</param>
        /// <param name="auraConnectDelegate">The action to execute upon configuring</param>
        /// <returns>The original host builder for chaining</returns>
        public static IHostBuilder ConfigureRGBKit(this IHostBuilder builder, Action<IRGBKitService> auraConnectDelegate)
        {
            var auraConnect = new RGBKitService();

            auraConnectDelegate(auraConnect);

            return builder.ConfigureServices(services =>
            {
                services.AddSingleton<IRGBKitService>(auraConnect);
            });
        }
    }
}
