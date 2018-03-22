/*
 * Sermon Record
 * Copyright 2017 Andrew Wong <featherbear@navhaxs.au.eu.org>
 *
 * The following code is licensed under the MIT License
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using NAudio.CoreAudioApi;
using NAudio.Wave;

namespace SermonRecord.UTIL
{
    public class AudioDevice
    {
        static AppPreferences appPreferences = ((App) Application.Current).Options;
        Recorder myrecorder = ((App)Application.Current).Recorder;

        public void Close()
        {
            if (IsOpen)
            {
                myrecorder.Stop();

                waveIn.DataAvailable -= updatePeak;
                waveIn.StopRecording();
                waveIn.Dispose();

                IsOpen = false;
                Debug.Print("Audio device closed");
            }
        }

        public bool Open()
        {
            if (IsOpen) Close();
            if (appPreferences.RecordingDevice == "") return false;

            waveIn = new WaveIn
            {
                WaveFormat = (appPreferences.RecordingDepth == 16
                    ? new WaveFormat(appPreferences.RecordingRate, appPreferences.RecordingChannels)
                    : WaveFormat.CreateIeeeFloatWaveFormat(appPreferences.RecordingRate,
                        appPreferences.RecordingChannels))
            };

            waveIn.DataAvailable += updatePeak;

            try
            {
                waveIn.StartRecording();
                IsOpen = true;
                Debug.Print("Audio device opened");

            } catch (Exception ex)
            {
                MessageBox.Show("Error opening audio device - perhaps invalid audio settings");
            }

            return true;
        }

        public static List<string> GetAvailableDevices()
        {
            var devices = new List<string>();
            var endpoints = new List<string>();

            new MMDeviceEnumerator().EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active).ToList().ForEach(
            d =>
            {
                endpoints.Add(d.FriendlyName);
            });

            for (var i = 0; i < WaveIn.DeviceCount; i++)
                devices.Add(endpoints.First(dev => dev.StartsWith(WaveIn.GetCapabilities(i).ProductName)));

            return devices;
        }

        #region Declarations

        public static WaveIn waveIn;

        private static readonly EventHandler<WaveInEventArgs> updatePeak = (s, e) =>
        {
            float max = 0;
            switch (appPreferences.RecordingDepth)
            {
                case 16:
                    for (var index = 0; index < e.BytesRecorded; index += 2)
                    {
                        var sample = (short) ((e.Buffer[index + 1] << 8) | e.Buffer[index + 0]) / 32768f;
                        if (sample < 0) sample = -sample;
                        if (sample > max) max = sample;
                    }
                    break;

                case 32:

                    var buffer = new WaveBuffer(e.Buffer);
                    // interpret as 32 bit floating point audio
                    for (var index = 0; index < e.BytesRecorded / 4; index++)
                    {
                        var sample = buffer.FloatBuffer[index];
                        if (sample < 0) sample = -sample;
                        if (sample > max) max = sample;
                    }

                    break;
            }
            PeakValue = max;
        };

        public static bool IsOpen { get; private set; }
        public static float PeakValue { get; private set; }
        public static double PeakValueDb => Math.Log10(PeakValue) * 20;

        #endregion Declarations
    }
}