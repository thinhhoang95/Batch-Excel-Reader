using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchExcelReader.Instructions
{
    class Instruction
    {
        public string Cell { get; set; }
        public string Alias { get; set; }

        public Instruction (string mCell, string mAlias)
        {
            Cell = mCell;
            Alias = mAlias;
        }
    }
}
