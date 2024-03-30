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
            bool skipLine = false; // Flag to skip the line starting with "CLI params:"
            foreach (string line in File.ReadLines(filePath, Encoding.UTF8))
            {
                // Skip the line starting with "CLI params:"
                if (line.StartsWith("CLI params:"))
                {
                    skipLine = true;
                    continue;
                }

                // Write the captured content to the output file
                if (!skipLine)
                {
                    outputFile.WriteLine(line.Trim());
                }
                else
                {
                    // Reset the flag after skipping the line
                    skipLine = false;
                }
            }
        }

        // To display information about logs THIS NEEDS USING!
        private void label4_Click(object sender, EventArgs e)
        {

        }

        // Main form with info about log files and directory status
        private void MainForm_Load(object sender, EventArgs e)
        {
            string user_profile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string dayz_directory = Path.Combine(user_profile, "AppData", "Local", "DayZ");

            if (Directory.Exists(dayz_directory))
            {
                ShowDayZFolderInfo(dayz_directory);
            }
            else
            {
                MessageBox.Show("DayZ folder not found.");
            }
        }

        private void ShowDayZFolderInfo(string directory)
        {
            string[] extensions = new string[] { "*.log", "*.mdmp", "*.RPT" };

            int totalFiles = 0;
            long totalSize = 0;

            foreach (string extension in extensions)
            {
                foreach (string file in Directory.GetFiles(directory, extension))
                {
                    FileInfo fileInfo = new FileInfo(file);
                    totalFiles++;
                    totalSize += fileInfo.Length;
                }
            }

            label4.Text = $"Number of log files: {totalFiles}\n";
            label4.Text += $"Total size of log files: {FormatBytes(totalSize)}";
        }

        private string FormatBytes(long bytes)
        {
            string[] suffixes = { "B", "KB", "MB", "GB", "TB" };
            int suffixIndex = 0;
            double byteCount = bytes;

            while (byteCount >= 1024 && suffixIndex < suffixes.Length - 1)
            {
                byteCount /= 1024;
                suffixIndex++;
            }

            return $"{byteCount:n1} {suffixes[suffixIndex]}";
        }

        // Info label including version
        private void label1_Click(object sender, EventArgs e)
        {

        }

        // Delete files button
        private void button1_Click(object sender, EventArgs e)
        {
            string user_profile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string log_directory = Path.Combine(user_profile, "AppData", "Local", "DayZ");

            // Delete files
            DeleteFilesWithExtensions(log_directory, "*.log", "*.mdmp", "*.RPT");

            // Show updated file information
            ShowDayZFolderInfo(log_directory);

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

        // Title for log info
        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
