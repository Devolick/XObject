using System;
using System.Collections.Generic;
using System.Text;

namespace XObjectSerializer.Optimization
{
    internal abstract class FileOptimizator
    {
        protected string importFile;

        internal FileOptimizator(string importFile)
        {
            this.importFile = importFile;
        }

        internal abstract string Optimization();
    }
}
