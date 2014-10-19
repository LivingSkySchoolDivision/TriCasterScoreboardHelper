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
        protected string fileName { get; set; }

        protected Game(string fileName)
        {
            this.fileName = fileName;
        }

        public abstract void Save();
    }
}
