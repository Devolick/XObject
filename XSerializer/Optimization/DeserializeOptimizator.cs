using System;
using System.Collections.Generic;
using System.Text;

namespace XObjectSerializer.Optimization
{
    internal class DeserializeOptimizator : FileOptimizator
    {
        internal DeserializeOptimizator(string importFile)
            : base(importFile) { }

        internal override string Optimization()
        {
            throw new NotImplementedException();
        }
    }
}
