using NAudio.Wave;
using NAudio.Lame;
using SermonRecord.UTIL;
using System;
using System.Collections.Generic;
using System.Windows;
using System.ComponentModel;

namespace SermonRecord.Models
{
    public class PostRecord
    {
        static AppPreferences appPreferences = ((App)Application.Current).Options;
        Recorder myrecorder = ((App)Application.Current).Recorder;

        private readonly BackgroundWorker worker = new BackgroundWorker();
        public PostRecord(Window _parent)
        {
            parent = _parent;
        }

        private Window parent;

        // TODO refactor to model?
        public string _title { get; set; }
        public string _speaker { get; set; }
        public string _series { get; set; }
        public string _passage { get; set; }
        public string _service { get; set; }

        public bool SaveMP3(List<string> saveLocationList)
        {
            string tempFilename = System.IO.Path.GetFileNameWithoutExtension(myrecorder.FilePath);

            int cnt = 0;
            foreach (var p in saveLocationList)
            {
                if (p != "" && !System.IO.Directory.Exists(p))
                {
                    var x1 = new SpecialMessageBox();
                    x1.ShowMessage(parent, "Error", "Check the save location(s)", String.Format("The save location \"{0}\" does not exist. Enter a valid location, or, leave it empty to skip.", p));
                    return false;
                }

                cnt++;
            }
            if (cnt == 0)
            {
                var x1 = new SpecialMessageBox();
                x1.ShowMessage(parent, "Error", "Check the save location(s)", String.Format("No save locations specified. Enter at least one location to save the MP3."));
                return false;
            }

            // path to the temporary location where the raw recording is saved (i.e. the WAV file),
            // not including the file extension.
            string tempRecordingPath = System.IO.Path.Combine(appPreferences.TempLocation, tempFilename);

            //if (!_usingCustomPath && File.Exists(saveLocation.Text) &&
            //    MessageBox.Show("A file with the same name exists, overwrite?", "File exists", MessageBoxButtons.YesNo,
            //        MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No) return;
            //btnSave.Enabled = false;
            foreach (var p in saveLocationList)
            {
                if (p != "") {
                    var testFile = System.IO.Path.Combine(p, tempFilename + ".mp3");
                    if (System.IO.File.Exists(testFile)) {
                        var x1 = new SpecialMessageBox();
                        x1.ShowMessage(parent, "Error", "Sermon not saved.", String.Format("The MP3 export was aborted because the file already exists at the save location:\n\n\"" + testFile + "\""));
                        return false;
                    }
                }
            }

            var tag = new ID3TagData
            {
                Title = _title.Trim(),
                Artist = _speaker.Trim(),
                Album = _series.Trim(),
                Year = $"{Recorder.StartTime.Year:#0000}{Recorder.StartTime.Month:#00}{Recorder.StartTime.Day:#00}",
                Comment = _passage,
                AlbumArtist = _service.Trim(),
                Genre = "swec.org.au"
            };

            //TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Normal);
            //TaskbarManager.Instance.SetProgressValue(0, 1);

            using (var inputWave = new WaveFileReader(myrecorder.FilePath))
            using (var compressor = new SimpleCompressorStream(inputWave)
            {
                Attack = (float)1.5,
                Enabled = true,
                Threshold = 10,
                Ratio = 4,
                MakeUpGain = -3
            })
            using (var writer = new LameMP3FileWriter(tempRecordingPath + ".compressed.mp3", inputWave.WaveFormat,
                LAMEPreset.MEDIUM,
                tag))
            {
                writer.MinProgressTime = 0;
                writer.OnProgress += (w, i, o, f) =>
                {
                    //if (f) Close(); //??? throw...
                    //else UpdateProgressBar(i, inputWave.Length);
                };

                var bytesPerMillisecond = inputWave.WaveFormat.AverageBytesPerSecond / 1000;

                var startPos = 0; // (int)waveform.TimeStart.TotalMilliseconds * bytesPerMillisecond;

                var endBytes = 0; // (int)waveform.TimeEnd.TotalMilliseconds * bytesPerMillisecond;
                endBytes = endBytes - endBytes % inputWave.WaveFormat.BlockAlign;
                var endPos = (int)inputWave.Length - endBytes;

                inputWave.Position = startPos - startPos % inputWave.WaveFormat.BlockAlign;
                var buffer = new byte[1024];
                while (inputWave.Position < endPos)
                {
                    var bytesRequired = (int)(endPos - inputWave.Position);
                    if (bytesRequired > 0)
                    {
                        var bytesToRead = Math.Min(bytesRequired, buffer.Length);
                        var bytesRead = compressor.Read(buffer, 0, bytesToRead);
                        if (bytesRead > 0)
                            writer.Write(buffer, 0, bytesRead);
                    }
                }
            }

            using (var inputWave = new WaveFileReader(myrecorder.FilePath))
            using (var writer = new LameMP3FileWriter(tempRecordingPath + ".mp3", inputWave.WaveFormat, LAMEPreset.MEDIUM, tag))
            {
                inputWave.CopyTo(writer);
            }

            foreach (var p in saveLocationList)
            {
                if (p != "") System.IO.File.Copy(tempRecordingPath + ".compressed.mp3", System.IO.Path.Combine(p, tempFilename + ".mp3"));
            }

            // Cleanup
            System.IO.File.Delete(tempRecordingPath + ".mp3");
            System.IO.File.Delete(tempRecordingPath + ".compressed.mp3");

            return true;
        }
    }
}
