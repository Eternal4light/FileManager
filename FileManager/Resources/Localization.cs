using FileSystemHelper;
using System;
using System.Collections.Generic;
using System.IO;

namespace FileManager.Resources
{
    public static class Localization
    {
        public static string AvailableFreeSpace = "Available Free Space:   ";
        public static string DriveFormat        = "Drive Format:   ";
        public static string DriveType          = "Drive Type:   ";
        public static string TotalFreeSpace     = "Total Free Space:   ";
        public static string TotalSize          = "Total Size:   ";
        public static string VolumeLabel        = "Volume Label:   ";

        public static string ElementsContained = "Elements Contained:   ";
        public static string LastWriteTime     = "Last Write Time:   ";
        public static string LastAccessTime    = "Last Access Time:   ";
        public static string CreationTime      = "Creation Time:   ";

        public static string Length         = "Length:   ";

        public static string ToDrives = "To Drives:   ";
        public static string InChildNodes = "in Child Nodes";
        public static string Search = "Search ";
        public static string FoldersChecked = "Folders Checked: ";


        public static void LocalizeDriveInfo(NodeEntity drive)
        {
            for (int i = 0; i < drive.Info.Length; i++)
            {
                drive.Info[i] = drive.Info[i].Replace("@AvailableFreeSpace", AvailableFreeSpace);
                drive.Info[i] = drive.Info[i].Replace("@DriveFormat", DriveFormat);
                drive.Info[i] = drive.Info[i].Replace("@DriveType", DriveType);
                drive.Info[i] = drive.Info[i].Replace("@TotalFreeSpace", TotalFreeSpace);
                drive.Info[i] = drive.Info[i].Replace("@TotalSize", TotalSize);
                drive.Info[i] = drive.Info[i].Replace("@VolumeLabel", VolumeLabel);
            }
        }
        public static void LocalizeDriveInfos(List<NodeEntity> drives)
        {
            foreach (var drive in drives)
            {
                LocalizeDriveInfo(drive);
            }
        }

        public static void LocalizeDirectoryInfo(NodeEntity folder)
        {
            for (int i = 0; i < folder.Info.Length; i++)
            {
                folder.Info[i] = folder.Info[i].Replace("@ElementsContained", ElementsContained);
                folder.Info[i] = folder.Info[i].Replace("@LastWriteTime", LastWriteTime);
                folder.Info[i] = folder.Info[i].Replace("@LastAccessTime", LastAccessTime);
                folder.Info[i] = folder.Info[i].Replace("@CreationTime", CreationTime);
            }
        }

        public static void LocalizeDirectoryInfos(List<NodeEntity> folders)
        {
            foreach (var folder in folders)
            {
                LocalizeDirectoryInfo(folder);
            }
        }

        public static void LocalizeFileInfo(NodeEntity file)
        {
            for (int i = 0; i < file.Info.Length; i++)
            {
                file.Info[i] = file.Info[i].Replace("@Length", Length);
                file.Info[i] = file.Info[i].Replace("@CreationTime", CreationTime);
                file.Info[i] = file.Info[i].Replace("@LastAccessTime", LastAccessTime);
            }
        }

        public static void LocalizeFileInfos(List<NodeEntity> files)
        {
            foreach (var file in files)
            {
                LocalizeFileInfo(file);
            }
        }

        public static void ChangeLanguage(string path)
        {
            if (String.IsNullOrEmpty(path))
            {
                AvailableFreeSpace = "Available Free Space:   ";
                DriveFormat = "Drive Format:   ";
                DriveType = "Drive Type:   ";
                TotalFreeSpace = "Total Free Space:   ";
                TotalSize = "Total Size:   ";
                VolumeLabel = "Volume Label:   ";

                ElementsContained = "Elements Contained:   ";
                LastWriteTime = "Last Write Time:   ";
                LastAccessTime = "Last Access Time:   ";
                CreationTime = "Creation Time:   ";

                Length = "Length:   ";

                ToDrives = "To Drives:   ";
                InChildNodes = "in Child Nodes";
                Search = "Search ";
                FoldersChecked = "Folders Checked: ";
            }
            else
            {
                var fileText = File.ReadAllLines(path);

                foreach (var line in fileText)
                {
                    if (line.Contains("@AvailableFreeSpace")) { AvailableFreeSpace = line.Split('#')[1]; continue; }
                    else if (line.Contains("@DriveFormat")) { DriveFormat = line.Split('#')[1]; continue; }
                    else if (line.Contains("@DriveType")) { DriveType = line.Split('#')[1]; continue; }
                    else if (line.Contains("@TotalFreeSpace")) { TotalFreeSpace = line.Split('#')[1]; continue; }
                    else if (line.Contains("@TotalSize")) { TotalSize = line.Split('#')[1]; continue; }
                    else if (line.Contains("@VolumeLabel")) { VolumeLabel = line.Split('#')[1]; continue; }
                    else if (line.Contains("@ElementsContained")) { ElementsContained = line.Split('#')[1]; continue; }
                    else if (line.Contains("@LastWriteTime")) { LastWriteTime = line.Split('#')[1]; continue; }
                    else if (line.Contains("@LastAccessTime")) { LastAccessTime = line.Split('#')[1]; continue; }
                    else if (line.Contains("@CreationTime")) { CreationTime = line.Split('#')[1]; continue; }
                    else if (line.Contains("@Length")) { Length = line.Split('#')[1]; continue; }
                    else if (line.Contains("@ToDrives")) { ToDrives = line.Split('#')[1]; continue; }
                    else if (line.Contains("@InChildNodes")) { InChildNodes = line.Split('#')[1]; continue; }
                    else if (line.Contains("@Search")) { Search = line.Split('#')[1]; continue; }
                    else if (line.Contains("@FoldersChecked")) { FoldersChecked = line.Split('#')[1]; continue; }

                }
            }
        }
    }
}
