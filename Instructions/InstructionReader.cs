using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BatchExcelReader.Instructions
{
    class InstructionReader
    {
        private List<Instruction> instructionList = new List<Instruction>();
        public List<Instruction> Instructions { get { return instructionList;  } }
        /*private BindingSource instructionBindingSource = new BindingSource();
        public BindingSource InstructionBinder { get {
                instructionBindingSource.DataSource = InstructionList;
                return instructionBindingSource;
        }}*/

        public InstructionReader(string instructionFilePath)
        {
            try
            {
                // Read the instruction file and populate the InstructionList
                string[] lines = System.IO.File.ReadAllLines(@instructionFilePath);
                int lineCount = 0;
                foreach (string s in lines)
                {
                    string[] t = s.Split(',');
                    instructionList.Add(new Instruction(t[0], t[1]));
                    lineCount++;
                }
                Console.WriteLine("InstructionReader: " + lineCount + " instructions read.");
            } catch (Exception e)
            {
                Console.WriteLine("InstructionReader: an error occurred while reading instruction file.");
                throw e;
            }
        }
    }
}
