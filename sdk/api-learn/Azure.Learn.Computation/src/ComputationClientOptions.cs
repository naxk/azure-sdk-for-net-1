using System;
using System.Collections.Generic;
using System.Text;
using Azure.Core;

namespace Azure.Learn.Computation
{
    /// <summary>
    /// Computation Client Options
    /// </summary>
    public class ComputationClientOptions : ClientOptions
    {
        internal string Version { get; }

        /// <summary>
        /// Creates ComputationClientOptions
        /// </summary>
        /// <param name="version"></param>
        public ComputationClientOptions(ServiceVersion version = ServiceVersion.V1_0)
        {
            Version = version switch
            {
                ServiceVersion.V1_0 => "1.0",
                _ => throw new ArgumentException($"The service version {version} is not supported by this library.", nameof(version))
            };
        }

        /// <summary>
        /// Computation Service Version
        /// </summary>
        public enum ServiceVersion
        {
            /// <summary>
            /// Version 1.0
            /// </summary>
#pragma warning disable CA1707 // Identifiers should not contain underscores
            V1_0
#pragma warning restore CA1707 // Identifiers should not contain underscores
        }
    }
}
