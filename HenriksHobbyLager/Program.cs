using System;

namespace HenriksHobbyLager
{
    class Program
    {
        static void Main(string[] args)
        {
            ProgramManager programManager = new ProgramManager();  // Skapar en instans av ProgramManager
            programManager.Run();  // Kör logiken från ProgramManager
        }
    }
}

