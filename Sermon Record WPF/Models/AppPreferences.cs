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

namespace Sermon_Record
{

    public class AppPreferences
    {
        public int RecordingChannels = 1;
        public int RecordingDepth = 32;
        public string RecordingDevice = "";
        public string SaveLocation = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public int RecordingRate = 48000;
        public string TempLocation = Path.GetTempPath();

        public List<Service> Services = new List<Service> { new Service { Name = "Kingsgrove 9am" },
                                                            new Service { Name = "Kingsgrove 11am" }};
    }

    public class Service
    {
        public string Name = "(Any)";
        public string DefaultCloudSaveLocation = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public string DefaultMyPCSaveLocation = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
    }
}
