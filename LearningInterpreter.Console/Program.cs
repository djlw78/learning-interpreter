﻿namespace LearningInterpreter.Console
{
    using System.Reflection;
    using LearningInterpreter.IO;
    using LearningInterpreter.Basic.Parsing;
    using LearningInterpreter.RunTime;

    class Program
    {
        private static void Main(string[] args)
        {
            var inputOutput = new ConsoleInputOutput();
            var scannerFactory = new ScannerFactory();
            var parser = new Parser(scannerFactory);
            var programRepository = new FileProgramRepository(parser);

            using (var rte = new RunTimeEnvironment(inputOutput, programRepository))
            {
                var readEvaluatePrintLoop = new ReadEvaluatePrintLoop(rte, parser);

                PrintSalute(inputOutput);
                Run(readEvaluatePrintLoop);
            }
        }

        private static void PrintSalute(IInputOutput inputOutput)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var assemblyName = assembly.GetName();
            inputOutput.WriteLine("{0} {1}", assemblyName.Name, assemblyName.Version);
        }

        private static void Run(ReadEvaluatePrintLoop readEvaluatePrintLoop)
        {
            do
            {
                readEvaluatePrintLoop.TakeStep();
            }
            while (!readEvaluatePrintLoop.IsOver);
        }
    }
}
