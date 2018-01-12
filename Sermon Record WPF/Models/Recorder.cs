/*
 * Sermon Record
 * Copyright 2017 Andrew Wong <featherbear@navhaxs.au.eu.org>
 *
 * The following code is licensed under the MIT License
 */

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Timers;
using System.Windows;
using NAudio.Wave;

namespace Sermon_Record.UTIL
{
    public class Recorder : INotifyPropertyChanged
    {
        static AppPreferences appPreferences = ((App)Application.Current).Options;

        public Recorder()
        {
            ElapsedTimer.Elapsed += timer_Elapsed;

        }

        #region Recorder

        public static DateTime StartTime;

        private static readonly EventHandler<WaveInEventArgs> WriteEvent = (s, e) =>
        {
            _writer.Write(e.Buffer, 0, e.BytesRecorded);
        };

        public delegate void RecordingStateChangedEvent (bool isRecording);
        public static event RecordingStateChangedEvent RecordingStateChanged;

        private static WaveFileWriter _writer;

        private bool _IsRecording;
        public bool IsRecording { get {
                return _IsRecording;
            }
            set
            {
                _IsRecording = value;

                OnPropertyChanged("IsRecording");

                if (RecordingStateChanged == null) return;
                RecordingStateChanged(_IsRecording);
            }
        }
        public static string FilePath => _writer.Filename;
        public static long FileSize => _writer.Position;
        public static string FileSizeF()
        {
            string[] suf = {"B", "KB", "MB", "GB", "TB", "PB", "EB"};
            if (FileSize == 0)
                return "0" + suf[0];
            long bytes = Math.Abs(FileSize);
            var place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            var num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return Math.Sign(FileSize) * num + suf[place];
        }

        #endregion Recorder

        #region Elapsed Time

        private Timer ElapsedTimer = new Timer(1000);
        private static readonly ElapsedEventHandler ElapsedEvent = (s, e) => {
  
        };
        public static int ElapsedTime { get; private set; }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            ElapsedTime += 1;
            
            OnPropertyChanged("GetElapsedTimeFormatted");
        }


        public string GetElapsedTimeFormatted
        {
            get {
                var hours = ElapsedTime / 60 / 60;
                var minutes = ElapsedTime / 60;
                var seconds = ElapsedTime % 60;
                return $"{hours:#00}:{minutes:#00}:{seconds:#00}";
            }

        }

        #endregion Elapsed Time

        #region Recording Functions

        public void Cancel()
        {
            if (IsRecording && Stop())
                File.Delete(FilePath);
        }

        public bool Record()
        {
            if (!AudioDevice.IsOpen) return false;
            StartTime = DateTime.UtcNow;

            _writer = new WaveFileWriter(Path.Combine(appPreferences.TempLocation, "sermonRecord_" +
                                                                                  (StartTime - new DateTime(1970, 1,
                                                                                       1, 0, 0, 0, DateTimeKind.Utc))
                                                                                  .TotalSeconds.ToString()
                                                                                  .Split('.')[0] +
                                                                                  ".wav"),
                AudioDevice.waveIn.WaveFormat);

            AudioDevice.waveIn.DataAvailable += WriteEvent;

            ElapsedTime = 0;
            OnPropertyChanged("GetElapsedTimeFormatted");

            ElapsedTimer.Start();

            IsRecording = true;
            Debug.Print("Recording started");
            return true;
        }

        public bool Stop()
        {
            ElapsedTimer.Stop();

            if (IsRecording)
            {


                AudioDevice.waveIn.DataAvailable -= WriteEvent;
                Debug.Print("DISPOSE WRITER");

                _writer.Dispose();

                IsRecording = false;
                Debug.Print("Recording stopped");
                return true;
            }
            return false;
        }

        #endregion Recording Functions

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}