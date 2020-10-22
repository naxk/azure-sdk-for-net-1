// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
        private readonly ClientDiagnostics _clientDiagnostics;
        private readonly Uri _endpoint;
        private readonly TokenCredential _credential;
        private readonly ComputationClientOptions _computationClientOptions;

        //private ComputeNodeAdministrationRestClient GetAdministrationRestClient(string nodeName = "")
        //{
        //    return new ComputeNodeAdministrationRestClient(
        //        new ClientDiagnostics(_computationClientOptions),
        //        HttpPipelineBuilder.Build(_computationClientOptions),
        //        nodeName,
        //        _endpoint);
        //}

        private ComputationRestClient GetComputationRestClient(string nodeName)
        {
            return new ComputationRestClient(
                new ClientDiagnostics(_computationClientOptions),
                HttpPipelineBuilder.Build(_computationClientOptions),
                nodeName,
                _endpoint);
        }

        private ExampleComputationServiceForAzureSDKAPIDesignTrainingUnitRestClient GetComputationAdminRestClient()
        {
            return new ExampleComputationServiceForAzureSDKAPIDesignTrainingUnitRestClient(
                new ClientDiagnostics(_computationClientOptions),
                HttpPipelineBuilder.Build(_computationClientOptions),
                _endpoint);
        }

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
            : this(endpoint, credential, computationClientOptions, new ClientDiagnostics(computationClientOptions))
        {
        }

        /// <summary>
        /// ComputationClient
        /// </summary>
        internal ComputationClient(Uri endpoint, TokenCredential credential, ComputationClientOptions computationClientOptions, ClientDiagnostics clientDiagnostics)
        {
            _endpoint = endpoint;
            _credential = credential;
            _computationClientOptions = computationClientOptions;
            _clientDiagnostics = clientDiagnostics;
        }

        /// <summary>
        /// ComputationClient
        /// </summary>
        protected ComputationClient()
        {
        }


        /// <summary>
        /// Starts compute Pi operation
        /// </summary>
        /// <param name="computeNode"></param>
        /// <param name="precision"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual ComputePiOperation StartComputePi(ComputeNode computeNode, int? precision, CancellationToken cancellationToken = default)
        {
            using DiagnosticScope scope = _clientDiagnostics.CreateScope($"{nameof(ComputationClient)}.{nameof(StartComputePi)}");
            scope.Start();

            try
            {
                var response = GetComputationRestClient(computeNode.Name).ComputePi(precision, cancellationToken);
                return new ComputePiOperation(this, response.Headers.OperationLocation);
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        /// <summary>
        /// Starts compute Pi operation
        /// </summary>
        /// <param name="computeNode"></param>
        /// <param name="precision"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ComputePiOperation> StartComputePiAsync(ComputeNode computeNode, int? precision, CancellationToken cancellationToken = default)
        {
            using DiagnosticScope scope = _clientDiagnostics.CreateScope($"{nameof(ComputationClient)}.{nameof(StartComputePi)}");
            scope.Start();

            try
            {
                var response = await GetComputationRestClient(computeNode.Name).ComputePiAsync(precision, cancellationToken).ConfigureAwait(false);
                return new ComputePiOperation(this, response.Headers.OperationLocation);
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        internal virtual ResponseWithHeaders<ComputationOperation, ExampleComputationServiceForAzureSDKAPIDesignTrainingUnitComputationHeaders> GetComputeOperation(string computationId)
        {
            var computation = GetComputationAdminRestClient().Computation(computationId);
            return computation;
        }

        internal virtual async Task<ResponseWithHeaders<ComputationOperation, ExampleComputationServiceForAzureSDKAPIDesignTrainingUnitComputationHeaders>> GetComputeOperationAsync(string computationId)
        {
            var computation = await GetComputationAdminRestClient().ComputationAsync(computationId).ConfigureAwait(false);
            return computation;
        }
    }
}
