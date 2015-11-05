﻿namespace LearningBasic.Parsing.Ast.Statements
{
    using System;

    public class Randomize : IStatement
    {
        public IExpression Seed { get; private set; }

        public Randomize(IExpression seed)
        {
            if (seed == null)
                throw new ArgumentNullException("seed");

            Seed = seed;
        }

        public EvaluateResult Execute(IRunTimeEnvironment rte)
        {
            var seedException = Seed.GetExpression(rte.Variables);
            var seed = seedException.CalculateValue();
            rte.Randomize((int)seed);

            return new EvaluateResult(Messages.RandomizeSeedAccepted, seed);
        }

        public override string ToString()
        {
            return "RANDOMIZE " + Seed.ToPrintable();
        }
    }
}