// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Linq;
using System.Threading.Tasks;
using Azure.Core.TestFramework;
using Azure.Learn.Computation.Models;
using NUnit.Framework;

namespace Azure.Learn.Computation.Samples
{
    public class Sample1_HelloWorld : SamplesBase<LearnComputationTestEnvironment>
    {
        public async Task ComputeTest()
        {
            //Azure.Learn.Computation.Models.
            string connectionString = "";

            ComputationClient client = new ComputationClient(new Uri(connectionString), null!);

            Pageable<ComputeNode> nodes = client.GetComputeNodes();

            ComputeNode node = nodes.Count() > 0
                ? nodes.First()
                : client.CreateWindowsComputeNode("my_node", "user_name");

            var linuxNode = client.CreateLinuxComputeNode("my_node", sshPublicKey: "key");

            // client.CreateComputeNode(new LinuxComputeNode("my_node", sshPublicKey: "key"));

            var pi = await client.StartComputePi(node, 2).WaitForCompletionAsync().ConfigureAwait(false);

            Assert.AreEqual(3.14, pi.Value);
        }
    }
}
