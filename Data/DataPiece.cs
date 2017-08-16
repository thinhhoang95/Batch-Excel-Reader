using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchExcelReader.Data
{
    class DataPiece
    {
        public Instructions.Instruction Instruction { get; set; }
        public string Data { get; set; }
        public DataPiece(Instructions.Instruction mInstruction, string mData)
        {
            Data = mData;
            Instruction = mInstruction;
        }
    }
}
