﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Trinity.DynamicCluster.Storage;
using Trinity.Network;
using Trinity.Storage;

namespace Trinity.DynamicCluster.Consensus
{
    /// <summary>
    /// Fault-tolerant Chunk metadata storage.
    /// Each instance represents the chunk table of a partition.
    /// Only the partition leader will update the chunk table for
    /// this partition. The secondary partitions passively receive updates
    /// from the master.
    /// One partition should not update the chunk table of another partition.
    /// </summary>
    public interface IChunkTable: IService
    {
        /// <summary>
        /// Gets a list of chunks held by a replica. The replica can be on any partition.
        /// </summary>
        /// <param name="replicaInfo">The identifier of the replica.</param>
        Task<IEnumerable<Chunk>> GetChunks(ReplicaInformation replicaInfo);
        /// <summary>
        /// Gets a list of distinct chunks held by a partition.
        /// </summary>
        /// <param name="partitionId">The identifier of the partition.</param>
        Task<IEnumerable<Chunk>> GetChunks(int partitionId);
        /// <summary>
        /// Updates the list of chunks held by a replica. The replica must be on the current partition.
        /// </summary>
        /// <param name="replicaId">The identifier of the replica.</param>
        /// <param name="chunks">The updated chunk list.</param>
        Task SetChunks(Guid replicaId, IEnumerable<Chunk> chunks);
        /// <summary>
        /// Deletes the replica entry from the chunk table of the current partition.
        /// </summary>
        /// <param name="replicaId">The replica to be deleted.</param>
        Task DeleteEntry(Guid replicaId);
    }
}