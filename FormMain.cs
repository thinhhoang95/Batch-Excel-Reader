using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BatchExcelReader.Instructions;

namespace BatchExcelReader
{
    public partial class FormMain : Form
    {
        private string XLSXFolder = "";
        private string instructionFile = "";
        private InstructionReader instructionReader;
        private BindingSource bindingSource = new BindingSource();
        private DataTable dataTable = new DataTable();

        public FormMain()
        {
            InitializeComponent();
        }

        private void browseXLSXButton_Click(object sender, EventArgs e)
        {
            DialogResult result = folderSelector.ShowDialog();
            if (result==DialogResult.OK && !string.IsNullOrWhiteSpace(folderSelector.SelectedPath))
            {
                XLSXFolder = folderSelector.SelectedPath;
                txtXLSXDirectory.Text = XLSXFolder;
            }
        }

        private void browseInstructionButton_Click(object sender, EventArgs e)
        {
            DialogResult result = fileSelector.ShowDialog();
            if (result==DialogResult.OK && !string.IsNullOrEmpty(fileSelector.FileName))
            {
                instructionFile = fileSelector.FileName;
                txtInstructionFile.Text = instructionFile;
            }
        }

        /// <summary>
        /// This void will read instruction file, prepare the dataset to process batch files
        /// </summary>
        private void readInstruction()
        {
            if (string.IsNullOrEmpty(instructionFile)) return;
            instructionReader = new InstructionReader(instructionFile);
        }

        private async void startProcessButton_Click_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(XLSXFolder) && !string.IsNullOrEmpty(instructionFile))
            {
                // Read instruction file for structure
                readInstruction();
                // Read excel files
                Data.DataTask readerTask = new Data.DataTask();
                dataTable = await readerTask.ReadXLSX(XLSXFolder, instructionReader.Instructions);
                // Update the GridView
                bindingSource.DataSource = dataTable;
                dataGridView.DataSource = bindingSource;
                dataGridView.Refresh();
            }
            else
            {
                MessageBox.Show("Please specify both XLSX directory and instruction file.", "Error occurred", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
