using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DayZ_Log
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            string user_profile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string log_directory = Path.Combine(user_profile, "AppData", "Local", "DayZ");

            List<(string, DateTime)> log_files = ScanLogFiles(log_directory);

            if (log_files.Any())
            {
                string output_file_path = "output.txt"; // Default output file path
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    output_file_path = saveFileDialog.FileName;
                    ProcessLogFiles(log_files, output_file_path);
                    MessageBox.Show("Scan complete. Output saved to " + output_file_path);
                }
            }
            else
            {
                MessageBox.Show("No log files found.");
            }
        }

        private List<(string, DateTime)> ScanLogFiles(string directory)
        {
            string logFilePattern = @"^.*\.log$";
            List<(string, DateTime)> logFiles = new List<(string, DateTime)>();

            foreach (string file in Directory.EnumerateFiles(directory, "*.log", SearchOption.AllDirectories))
            {
                string fileName = Path.GetFileName(file);
                if (Regex.IsMatch(fileName, logFilePattern, RegexOptions.IgnoreCase))
                {
                    DateTime creationDate = File.GetCreationTime(file);
                    logFiles.Add((file, creationDate));
                }
            }

            logFiles.Sort((x, y) => x.Item2.CompareTo(y.Item2));

            return logFiles;
        }

        private void ProcessLogFiles(List<(string, DateTime)> logFiles, string outputFilePath)
        {
            using (StreamWriter outputFile = new StreamWriter(outputFilePath, false, Encoding.UTF8))
            {
                foreach ((string filePath, DateTime creationDate) in logFiles)
                {
                    try
                    {
                        string formattedDate = creationDate.ToString("yyyy-MM-dd HH:mm:ss");
                        outputFile.WriteLine($"File: {filePath} (Created: {formattedDate})");

                        if (Path.GetFileName(filePath).StartsWith("crash_"))
                        {
                            ProcessCrashLogFile(filePath, outputFile);
                        }
                        else
                        {
                            ProcessRegularLogFile(filePath, outputFile);
                        }

                        outputFile.WriteLine(new string('-', 80));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error processing file {filePath}: {ex.Message}");
                    }
                }
            }
        }

        private void ProcessRegularLogFile(string filePath, StreamWriter outputFile)
        {
            string errorPattern = @"\[ERROR\]";

            foreach (string line in File.ReadLines(filePath, Encoding.UTF8))
            {
                if (Regex.IsMatch(line, errorPattern))
                {
                    outputFile.WriteLine(line.Trim());
                }
            }
        }

        private void ProcessCrashLogFile(string filePath, StreamWriter outputFile)
        {
            bool captureContent = true; // Start capturing content immediately
            foreach (string line in File.ReadLines(filePath, Encoding.UTF8))
            {
                // Stop capturing content when reaching "Runtime mode"
                if (line.StartsWith("Runtime mode"))
                {
                    break;
                }

                // Write the captured content to the output file
                if (captureContent)
                {
                    outputFile.WriteLine(line.Trim());
                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Optional: Any additional initialization code for the form can go here
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        // Delete files button
        private void button1_Click(object sender, EventArgs e)
        {
            string user_profile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string log_directory = Path.Combine(user_profile, "AppData", "Local", "DayZ");

            DeleteFilesWithExtensions(log_directory, "*.log", "*.mdmp", "*.RPT");

            MessageBox.Show("Log files deleted successfully.");
        }

        private void DeleteFilesWithExtensions(string directory, params string[] extensions)
        {
            foreach (string extension in extensions)
            {
                foreach (string file in Directory.GetFiles(directory, extension))
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error deleting file {file}: {ex.Message}");
                    }
                }
            }
        }

        // Label for File Scan
        private void label2_Click(object sender, EventArgs e)
        {

        }

        // Label for Delete log files
        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
