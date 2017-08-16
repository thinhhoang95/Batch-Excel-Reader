using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchExcelReader.Data
{
    class DataTask
    {
        public Task<DataTable> ReadXLSX(string directory, List<Instructions.Instruction> instructions)
        {
            return Task.Run(() =>
            {
                DataReader dr = new Data.DataReader(directory, instructions);
                return dr.DataAsTable;
            });
        }
    }


}
