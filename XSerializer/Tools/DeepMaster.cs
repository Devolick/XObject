using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using XObjectSerializer.Exceptions;
using XObjectSerializer.Helpers;

namespace XObjectSerializer.Tools
{
    internal class DeepMaster
    {
        private object target;
        private object value;
        private string dotPath;

        private DeepMaster()
        {

        }
        internal DeepMaster(object target, object value, string dotPath)
        {
            if (string.IsNullOrEmpty(dotPath)) throw new XObjectException("Path can not be null reference or empty.");

            this.target = target ?? throw new XObjectException("Target can not be null reference.");
            this.value = value;
            this.dotPath = dotPath;
        }

        internal bool Partially()
        {
            string[] path = dotPath.Split('.');
            int count = 0;
            bool nothingFound = false;
            object tg = target;
            while (!nothingFound)
            {
                if (count >= path.Length) return false;
                nothingFound = true;
                foreach (PropertyInfo pi in ReflectionHelper.EachProps(tg))
                {
                    if (pi.Name == path[count] && count < path.Length - 1)
                    {
                        nothingFound = false;
                        tg = pi.GetValue(tg);
                        break;
                    }
                    else if (pi.Name == path[count] && count == path.Length - 1)
                    {
                        pi.SetValue(tg, value);
                        return true;
                    }
                }
                ++count;
            };

            return false;
        }
        internal bool Merge()
        {
            string[] path = dotPath.Split('.');
            int count = 0;
            bool nothingFound = false;
            object tg = target;
            while (!nothingFound)
            {
                if (count >= path.Length) return false;
                nothingFound = true;
                foreach (PropertyInfo pi in ReflectionHelper.EachProps(tg))
                {
                    if (pi.Name == path[count] && count < path.Length - 1)
                    {
                        nothingFound = false;
                        tg = pi.GetValue(tg);
                        break;
                    }
                    else if (pi.Name == path[count] && count == path.Length - 1)
                    {
                        pi.SetValue(tg, MergeValue());
                        return true;
                    }
                }
                ++count;
            };

            return false;
        }
        private object MergeValue() {
            string[] path = dotPath.Split('.');
            int count = 0;
            bool nothingFound = false;
            object tg = value;
            while (!nothingFound)
            {
                if (count >= path.Length) return false;
                nothingFound = true;
                foreach (PropertyInfo pi in ReflectionHelper.EachProps(tg))
                {
                    if (pi.Name == path[count] && count < path.Length - 1)
                    {
                        nothingFound = false;
                        tg = pi.GetValue(tg);
                        break;
                    }
                    else if (pi.Name == path[count] && count == path.Length - 1)
                    {
                        return pi.GetValue(tg);
                    }
                }
                ++count;
            };

            return null;
        }

    }
}
