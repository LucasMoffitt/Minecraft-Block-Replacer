using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using Substrate;
using Substrate.Core;

namespace Minecraft_BlockReplacer {
    class Program {

        private static string world;
        private static int blockId;
        private static int replacementId;

        static void Main(string[] args) {

            Console.WriteLine("===================================");
            Console.WriteLine("     MINECRAFT BLOCK REPLACER      ");
            Console.WriteLine("          Lucas Moffitt            ");
            Console.WriteLine("===================================");
            Console.WriteLine("");

            GatherInformation();
        }

        static void GatherInformation() {
            GetWorld();
        }
        
        static void GetWorld() {
            while(true) {
                Console.WriteLine("Enter the absolute path to your world file...");
                string inputWorldLocation = Console.ReadLine();

                if(string.IsNullOrEmpty(inputWorldLocation)) {
                    Console.WriteLine("Invalid Directory.");
                    continue;
                } else {
                    try {
                        NbtWorld worldFile = NbtWorld.Open(inputWorldLocation);
                        if(worldFile == null) {
                            Console.WriteLine("No world was found at this location.");
                            continue;
                        } else {
                            worldFile = null;
                            world = inputWorldLocation;
                            GetBlockId();
                        }
                    } catch(Exception) {
                        Console.WriteLine("We had problems opening the world. Is it corrupt or the wrong file?");
                        continue;
                    }
                }
            }
        }

        static void GetBlockId() {
            while(true) {
                Console.WriteLine("Enter the Id of the block you want to be replaced...");
                var inputBlockId = Console.ReadLine();

                if(string.IsNullOrEmpty(inputBlockId)) {
                    Console.WriteLine("you need to enter shit");
                    continue;
                } else {
                    if(Int32.TryParse(inputBlockId, out blockId)) {
                        GetReplacementId();
                    } else {
                        Console.WriteLine("Your blockId is invalid or not a real number");
                        continue;
                    }
                }
            }
        }
        
        static void GetReplacementId() {
            while(true) {
                Console.WriteLine("Enter the Id of the block you want to replace #{0} with.", blockId);
                var inputReplacementId = Console.ReadLine();
                if(string.IsNullOrEmpty(inputReplacementId)) {
                    Console.WriteLine("you need to enter shit");
                    continue;
                } else {
                    if(Int32.TryParse(inputReplacementId, out replacementId)) {
                        Confirm();
                    } else {
                        Console.WriteLine("Your blockId is invalid or not a real number");
                        continue;
                    }
                }
            }
        }

        static void Confirm() {

            Console.WriteLine("===================================");
            Console.WriteLine("You're about to replace all blocks with the ID: {0}, with {1} on the map located at {2}", blockId, replacementId, world);
            Console.WriteLine("Type \"Confirm\" to being the task. Please Note: Depending on the size of your map this could take time time... If you have changed your mind type \"Exit\"");

            while(true) {
                var userResult = Console.ReadLine();
                
                if (string.IsNullOrEmpty(userResult)) {
                    Console.WriteLine("You need to make a selection here.... \"Confirm\" or \"Exit\"");
                    continue;
                }

                if(userResult == "Confirm") {
                    Console.WriteLine("===================================");
                    Console.WriteLine("Begining Task....");
                                    
                    ReplaceBlock replaceBlock = new ReplaceBlock();
                    replaceBlock.ReplaceThisBlock(world, blockId, replacementId);
                    replaceBlock = null;
                    FinishUp();
                } else if(userResult == "Exit") {
                    Environment.Exit(0);
                } else {
                    Console.WriteLine("You need to make a selection here.... \"Confirm\" or \"Exit\"");
                    continue;
                }
            }
        }

        static void FinishUp() {
            Console.WriteLine("Thankyou for using Minecraft-BlockReplacer");
            Console.WriteLine("Close the window or type \"Exit\" to exit.");
            Console.ReadLine();
        }
    }
}
