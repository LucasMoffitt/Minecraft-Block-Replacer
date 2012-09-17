using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Substrate;
using Substrate.Core;

namespace Minecraft_BlockReplacer {
    public class ReplaceBlock {

        public void ReplaceThisBlock(string operationWorld, int blockId, int replacementId) {

            int itemsinchunk = 0;
            int itemstotal = 0;
            long chunkcount = 0;

            NbtWorld world = NbtWorld.Open(operationWorld);

            IChunkManager chunkManager = world.GetChunkManager();

            foreach(ChunkRef chunk in chunkManager) {
                chunkcount++;

                int xdim = chunk.Blocks.XDim;
                int ydim = chunk.Blocks.YDim;
                int zdim = chunk.Blocks.ZDim;

                for(int x = 0; x < xdim; x++) {
                    for(int z = 0; z < zdim; z++) {
                        for(int y = 0; y < ydim; y++) {
                            var block = chunk.Blocks.GetID(x, y, z);
                            if(block == blockId) {
                                chunk.Blocks.SetData(x, y, z, 0);
                                chunk.Blocks.SetID(x, y, z, replacementId);
                                itemsinchunk++;
                            }
                        }
                    }
                }

                chunkManager.Save();
                
                Console.WriteLine("Chunk:{0}, Position:{1}.{2} = Replaced {3} item(s)", chunkcount, chunk.X, chunk.Z, itemsinchunk);
                itemstotal = itemstotal + itemsinchunk;
                itemsinchunk = 0;
            }

            Console.WriteLine("Replaced: " + itemstotal);
            Console.WriteLine("Task Complete...");
        }
    
    }
}
