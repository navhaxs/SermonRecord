/*
 * Sermon Record
 * Copyright 2017 Andrew Wong <featherbear@navhaxs.au.eu.org>
 *
 * The following code is licensed under the MIT License
 */

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Sermon_Record
{

    public class AppPreferences
    {
        public int RecordingChannels = 1;
        public int RecordingDepth = 32;
        public string RecordingDevice = "";
        //public string SaveLocation = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public int RecordingRate = 48000;
        public string TempLocation = Path.GetTempPath();
        public bool AutoMinimise = true;

        public List<Service> Services;
    }

    public class Service
    {
        public string Name = "11am Church English";
        public string CloudSaveLocation = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), @"Google Drive\Sermon MP3\{SERVICE}\");
        public string MyPCSaveLocation = @"D:\Sermons\{YEAR}\{SERVICE}\";

        [XmlIgnore]
        public string DefaultCloudSaveLocation
        {
            get
            {
                return process_subst(CloudSaveLocation);
            }
            set
            {
                CloudSaveLocation = value;
            }
        }

        [XmlIgnore]
        public string DefaultMyPCSaveLocation
        {
            get {
                return process_subst(MyPCSaveLocation);
            }
            set
            {
                MyPCSaveLocation = value;
            }
        } // System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public string process_subst(string input)
        {
            return input.Replace("{YEAR}", DateTime.Now.Year.ToString())
                    .Replace("{SERVICE}", Name);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return Name.GetHashCode();
            }
        }

        public override bool Equals(object obj)
        {
            return obj is Service && Equals((Service)obj);
        }

        public bool Equals(Service p)
        {
            return Name == p.Name;
        }

    }
}
