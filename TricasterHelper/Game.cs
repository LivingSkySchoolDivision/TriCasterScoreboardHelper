using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TricasterHelper
{
    abstract class Game
    {
        FileStream VariableFile { get; set; }

        protected Game(string fileName)
        {

        }

        public abstract void Save();
    }
}
