using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SpaceSaver
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void SelectFolderDialog_Click(object sender, EventArgs e)
        {
            if (_folderBrowserDialog.ShowDialog() != DialogResult.OK) return;
            FolderTextBox.Text = _folderBrowserDialog.SelectedPath;
        }

        private void RunButton_Click(object sender, EventArgs e)
        {
            ((Control) sender).Enabled = false;
            if (string.IsNullOrWhiteSpace(FolderTextBox.Text) || !System.IO.Directory.Exists(FolderTextBox.Text))
            {
                MessageBox.Show("Please Check Folder Location", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var toActOnThis = PopulateDuplicates(DirSearch(FolderTextBox.Text).GroupBy(f => f.FileName)
                .Where(f => f.Count() > 1)
                .ToDictionary(f => f.Key, f => f.ToArray()));

            Dictionary<string, int> dictionary = new Dictionary<string, int>();

            using (var sw = new StreamWriter(@"d:\outputdatatext.txt", true))
            {
                foreach (var item in toActOnThis)
                {
                    if (!dictionary.TryGetValue(item.Key.FileName, out var count))
                    {
                        count = 0;
                        dictionary.Add(item.Key.FileName, count);
                    }

                    count++;
                    dictionary[item.Key.FileName] = count;
                    sw.WriteLine($"{count}-{item.Key.FileName}");
                    sw.WriteLine($"\t{item.Key.FullFile}");
                    foreach (var i in item.Value)
                    {
                        sw.WriteLine($"\t{i.FullFile}");
                    }
                }
            }

            ((Control) sender).Enabled = true;
        }

        private Dictionary<FileDetails, List<FileDetails>> PopulateDuplicates(Dictionary<string, FileDetails[]> possibleDuplicates)
        {
            Dictionary<string, bool> Done = new Dictionary<string, bool>();
            var dict = new Dictionary<FileDetails, List<FileDetails>>();
            int index = 0;
            progressBar1.Maximum = possibleDuplicates.Count;
            foreach (var possible in possibleDuplicates)
            {
                progressBar1.Value = index;
                index++;

                Dictionary<int, Dictionary<int, bool>> lookup = new Dictionary<int, Dictionary<int, bool>>();
                var items = possible.Value;
                for (int i = 0; i < items.Length; i++)
                {
                    var firstItem = items[i];
                    if (Done.ContainsKey(firstItem.FullFile))
                    {
                        continue;
                    }

                    for (int j = 0; j < items.Length; j++)
                    {
                        if (i == j)
                        {
                            continue;
                        }

                        if (lookup.TryGetValue(j, out var list) && list.ContainsKey(i))
                        {
                            continue;
                        }

                        if (list == null)
                        {
                            lookup.Add(j, new Dictionary<int, bool>());
                        }

                        lookup[j].Add(i, true);
                        if (lookup.ContainsKey(i))
                        {
                            lookup[i].Add(j, true);
                        }
                        else
                        {
                            lookup.Add(i, new Dictionary<int, bool> {{j, true}});
                        }

                        var secoundItem = items[j];
                        if (Done.ContainsKey(secoundItem.FullFile))
                        {
                            continue;
                        }

                        if (secoundItem.Equals(firstItem))
                        {
                            continue;
                        }

                        try
                        {
                            if (Mkb.FileCompare.FilesAreEqual(new FileInfo(firstItem.FullFile), new FileInfo(secoundItem.FullFile)))
                            {
                                if (dict.ContainsKey(firstItem))
                                {
                                    dict[firstItem].Add(secoundItem);
                                }
                                else
                                {
                                    dict.Add(firstItem, new List<FileDetails> {secoundItem});
                                }

                                if (Done.ContainsKey(firstItem.FullFile) == false)
                                {
                                    Done.Add(firstItem.FullFile, true);
                                }

                                if (Done.ContainsKey(secoundItem.FullFile) == false)
                                {
                                    Done.Add(secoundItem.FullFile, true);
                                }

                                firstItem.Duplicates.Add(secoundItem);
                                secoundItem.Duplicates.Add(firstItem);
                            }
                        }
                        catch (Exception e)
                        {
                        }
                    }
                }
            }

            return dict;
        }

        private static IEnumerable<FileDetails> DirSearch(string sDir)
        {
            var files = new List<FileDetails>();


            foreach (var file in Directory.GetDirectories(sDir))
            {
                files.AddRange(Directory.GetFiles(file)
                    .Select(f => new FileDetails
                    {
                        Directory = file,
                        FileName = f.Split('\\').Last(),
                        FullFile = f,
                    })
                );
                files.AddRange(DirSearch(file));
            }

            return files;
        }
    }

    public class FileDetails
    {
        protected bool Equals(FileDetails other)
        {
            return Equals(Duplicates, other.Duplicates) && Directory == other.Directory && FileName == other.FileName && FullFile == other.FullFile;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((FileDetails) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Duplicates != null ? Duplicates.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Directory != null ? Directory.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (FileName != null ? FileName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (FullFile != null ? FullFile.GetHashCode() : 0);
                return hashCode;
            }
        }

        public string Directory { get; set; }
        public string FileName { get; set; }

        public string FullFile { get; set; }

        public List<FileDetails> Duplicates = new List<FileDetails>();
    }
}