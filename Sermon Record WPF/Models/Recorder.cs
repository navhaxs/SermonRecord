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

        //private readonly EventHandler<WaveInEventArgs> WriteEvent;

        //private void HandleWriteEvent (s, e) =>
        //{
        //   
        //};

    public delegate void RecordingStateChangedEvent (RecorderState recordingState);
        public static event RecordingStateChangedEvent RecordingStateChanged;

        private static WaveFileWriter _writer;

        public enum RecorderState
        {
            Stopped = 0,
            Recording = 1,
            Paused = 2
        }

        private RecorderState _IsRecording = RecorderState.Stopped;

        public bool IsRecording
        {
            get
            {
                if (_IsRecording == RecorderState.Stopped) return false;
                return true;
            }
        }

        public RecorderState MyRecordingState { get {
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
        public string FileName;
        public string FilePath => _writer?.Filename;
        public long FileSize => _writer.Position;
        public string FileSizeF()
        {
            if (_writer == null) return "0MB";

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
            Stop();
            File.Delete(FilePath);
        }

        public void Reset()
        {
            ElapsedTime = 0;
            OnPropertyChanged("GetElapsedTimeFormatted");
        }

        public bool Record()
        {
            if (!AudioDevice.IsOpen) return false;
            StartTime = DateTime.Now;

            FileName = "sermon_" + StartTime.ToString("yyyyMMdd");
            _writer = new WaveFileWriter(Path.Combine(appPreferences.TempLocation, FileName + ".wav"),
                AudioDevice.waveIn.WaveFormat);

            AudioDevice.waveIn.DataAvailable += WriteEvent;

            ElapsedTime = 0;
            OnPropertyChanged("GetElapsedTimeFormatted");

            ElapsedTimer.Start();

            MyRecordingState = RecorderState.Recording;
            Debug.Print("Recording started");
            return true;
        }

        private void WriteEvent(object sender, WaveInEventArgs e)
        {
            if (MyRecordingState == RecorderState.Recording)
                _writer.Write(e.Buffer, 0, e.BytesRecorded);
        }

        public void Pause()
        {
            switch (MyRecordingState)
            {
                case RecorderState.Recording:
                    MyRecordingState = RecorderState.Paused;
                    break;
                default:
                    break;
            };
        }

        public void Stop()
        {
            ElapsedTimer.Stop();

            if (IsRecording)
            {
                AudioDevice.waveIn.DataAvailable -= WriteEvent;

                _writer.Close();
                _writer.Dispose();
                MyRecordingState = RecorderState.Stopped;
            }

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