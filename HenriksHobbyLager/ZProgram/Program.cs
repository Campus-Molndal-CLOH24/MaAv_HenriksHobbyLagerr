using HenriksHobbyLager.ZProgram;
using Microsoft.Extensions.Configuration;
using System;

namespace HenriksHobbyLager.ZProgram
{
    class Program
    {
        static void Main()
        {
            var programManager = new ProgramManager();  // Skapa instans av ProgramManager
            programManager.Run();  // Kör hela programmet
        }
    }
}


