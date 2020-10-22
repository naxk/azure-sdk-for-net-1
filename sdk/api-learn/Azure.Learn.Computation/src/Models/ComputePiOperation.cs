// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Azure.Learn.Computation.Models
{
    /// <summary>
    /// ComputePiOperation
    /// </summary>
    public class ComputePiOperation : Operation<float>
    {
        private readonly ComputationClient _client;
        private float? _value;
        private bool _hasCompleted;
        private Response _rawResponse;

        private readonly HashSet<ComputationOperationStatus> _terminalStatuses = new HashSet<ComputationOperationStatus>
            {
                ComputationOperationStatus.Succeeded,
                ComputationOperationStatus.Cancelled,
                ComputationOperationStatus.Failed
            };

        /// <summary>
        /// Creates ComputeOeration
        /// </summary>
        /// <param name="client"></param>
        /// <param name="computationId"></param>
        public ComputePiOperation(ComputationClient client, string computationId)
        {
            Id = computationId;
            _client = client;
        }

        /// <inheritdocs />
        public override string Id { get; }

        /// <inheritdocs />
        public override float Value => _value.Value;

        /// <inheritdocs />
        public override bool HasCompleted => _hasCompleted;

        /// <inheritdocs />
        public override bool HasValue => _hasCompleted && _value.HasValue;

        /// <inheritdocs />
        public override Response GetRawResponse()
        {
            return _rawResponse;
        }

        /// <inheritdocs />
        public override Response UpdateStatus(CancellationToken cancellationToken = default)
        {
            if (_hasCompleted)
            {
                return _rawResponse;
            }

            var response = _client.GetComputeOperation(Id);
            _rawResponse = response.GetRawResponse();

            if (IsOperationFinished(response.Value))
            {
                _value = response.Value.Value;
                _hasCompleted = true;
            }

            return response;
        }

        private bool IsOperationFinished(ComputationOperation operation)
        {
            return operation != null &&  operation.Status.HasValue && _terminalStatuses.Contains(operation.Status.Value);
        }

        /// <inheritdocs />
        public override async ValueTask<Response> UpdateStatusAsync(CancellationToken cancellationToken = default)
        {
            if (_hasCompleted)
            {
                return _rawResponse;
            }

            var response = await _client.GetComputeOperationAsync(Id).ConfigureAwait(false);
            _rawResponse = response.GetRawResponse();

            if (IsOperationFinished(response.Value))
            {
                _value = response.Value.Value;
                _hasCompleted = true;
            }

            return response;
        }

        /// <inheritdocs />
        public override ValueTask<Response<float>> WaitForCompletionAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdocs />
        public override ValueTask<Response<float>> WaitForCompletionAsync(TimeSpan pollingInterval, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
