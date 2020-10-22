// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure.Core;


namespace Azure.Learn.Computation.Models
{
    /// <summary>
    /// ComputeNode
    /// </summary>
    [CodeGenModel("ComputeNode")]
    public partial class ComputeNode
    {
        /// <summary>
        /// Http ETag
        /// </summary>
        [CodeGenMember("Etag")]
        public ETag ETag { get; }
    }
}
