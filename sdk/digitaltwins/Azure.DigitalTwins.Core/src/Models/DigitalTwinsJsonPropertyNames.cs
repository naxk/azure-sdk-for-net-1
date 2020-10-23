// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.DigitalTwins.Core
{
    /// <summary>
    /// String constants for use in JSON de/serialization for custom types.
    /// </summary>
    /// <remarks>
    /// Useful with <see cref="IDigitalTwinRelationship"/>.
    /// </remarks>
    public static class DigitalTwinsJsonPropertyNames
    {
        /// <summary>
        /// The JSON property name for the relationship Id field.
        /// </summary>
        public const string RelationshipId = "$relationshipId";

        /// <summary>
        /// The JSON property name for the source Id field.
        /// </summary>
        public const string RelationshipSourceId = "$sourceId";

        /// <summary>
        /// The JSON property name for the target Id field.
        /// </summary>
        public const string RelationshipTargetId = "targetId";

        /// <summary>
        /// The JSON property name for the relationship Id field.
        /// </summary>
        public const string RelationshipName = "$relationshipName";
    }
}
