using System;
using System.Collections.Generic;
using System.Text;
using Azure.Core;
using Azure.Core.Pipeline;
using Azure.Learn.Computation.Models;

namespace Azure.Learn.Computation
{
    /// <summary>
    /// Computation client
    /// </summary>
    public class ComputationClient
    {
        private ServiceRestClient ServiceRestClient { get; }

        /// <summary>
        /// ComputationClient
        /// </summary>
        public ComputationClient(Uri endpoint, TokenCredential credential) : this(endpoint, credential, new ComputationClientOptions())
        {
        }

        /// <summary>
        /// ComputationClient
        /// </summary>
        public ComputationClient(Uri endpoint, TokenCredential credential, ComputationClientOptions computationClientOptions)
        {
            ServiceRestClient = new ServiceRestClient(
                new ClientDiagnostics(computationClientOptions),
                HttpPipelineBuilder.Build(computationClientOptions),
                endpoint);
        }

        /// <summary>
        /// ComputationClient
        /// </summary>
        protected ComputationClient()
        {
        }

        //public AsyncPageable<ComputeNode> GetComputeNodesAsync()
        //{
        //}
    }
}
