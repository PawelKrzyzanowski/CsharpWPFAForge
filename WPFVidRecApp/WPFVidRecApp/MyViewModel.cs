﻿using System; /* String, bool, DateTime */
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows; /*Message box*/
using System.Windows.Media.Imaging; /*BitmapImage*/
using System.Collections.ObjectModel; /* ObservableCollection */
using System.Drawing; /* Rectangle */
using System.Drawing.Imaging;
using System.Windows.Input; /* ICommand */
using System.IO; /* MemoryStream in  */
using System.Windows.Threading; /* Dispatcher */

using GalaSoft.MvvmLight; /* ObservableObject*/
using GalaSoft.MvvmLight.CommandWpf; 

using AForge.Video.DirectShow; /*FilterInfo */
using AForge.Video; /*IVideoSource*/

using Accord.Video.FFMPEG; /*VideoFileWriter*/

namespace WPFVidRecApp
{
    internal class MyViewModel : GalaSoft.MvvmLight.ObservableObject, IDisposable
    {      
        #region Private fields
        private AForge.Video.DirectShow.FilterInfo _inputVideoDevice;
        private System.Windows.Media.Imaging.BitmapImage _bitmap;
        private string _ipCameraUrl;
        private bool _isDesktopSource;
        private bool _isIpCameraSource;
        private bool _isWebcamSource;
        private AForge.Video.IVideoSource _videoSourceInterface;
        private Accord.Video.FFMPEG.VideoFileWriter _videoFileWriter;
        private bool _isRecording;
        private DateTime _firstFrameTime;
        #endregion

        #region Properties
        public AForge.Video.DirectShow.FilterInfo InputVideoDevice
        {
            get { return _inputVideoDevice; }
            set { Set(ref _inputVideoDevice, value); }
        }
        public BitmapImage Bitmap
        {
            get { return _bitmap; }
            set { Set(ref _bitmap, value); }
        }
        public bool IsDesktopSource
        {
            get { return _isDesktopSource; }
            set { Set(ref _isDesktopSource, value); }
        }

        public bool IsWebcamSource
        {
            get { return _isWebcamSource; }
            set { Set(ref _isWebcamSource, value); }
        }

        public bool IsIpCameraSource
        {
            get { return _isIpCameraSource; }
            set { Set(ref _isIpCameraSource, value); }
        }

        public string IpCameraUrl
        {
            get { return _ipCameraUrl; }
            set { Set(ref _ipCameraUrl, value); }
        }

        public ObservableCollection<FilterInfo> InputVideoDevicesCollection { get; set; }
        public ICommand StartAquisitionCommand { get; private set; }
        public ICommand StopAquisitionCommand { get; private set; }
        public ICommand StartRecordingCommand { get; private set; }
        public ICommand StopRecordingCommand { get; private set; }
        public ICommand SaveSnapshotCommand { get; private set; }
        #endregion

        #region Constructor
        public MyViewModel()
        {
            InputVideoDevicesCollection = new ObservableCollection<FilterInfo>();
            initInputVideoDevices();
            IsDesktopSource = true;
            StartAquisitionCommand = new RelayCommand(startAquisition);/*
            StopAquisitionCommand = new RelayCommand(StopCamera);
            StartRecordingCommand = new RelayCommand(StartRecording);
            StopRecordingCommand = new RelayCommand(StopRecording);
            SaveSnapshotCommand = new RelayCommand(SaveSnapshot);*/
            /* free ip camera */
            IpCameraUrl = "http://88.53.197.250/axis-cgi/mjpg/video.cgi?resolution=320×240";
        }
        #endregion

        #region Other Methods
        private void initInputVideoDevices()
        {
            var videoDevices = new AForge.Video.DirectShow.FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (AForge.Video.DirectShow.FilterInfo device in videoDevices)
            {
                InputVideoDevicesCollection.Add(device);
            }
            if (InputVideoDevicesCollection.Any())
            {
                InputVideoDevice = InputVideoDevicesCollection[0];
            }
            else
            {
                MessageBox.Show("No webcam found");
            }
        }

        private void myNewFrameEventHandler(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            try
            {
                /*
                if (_isRecording)
                {
                    using (var bitmap = (Bitmap)eventArgs.Frame.Clone())
                    {
                        if (_firstFrameTime != null)
                        {
                            _videoFileWriter.WriteVideoFrame(bitmap, DateTime.Now - _firstFrameTime.Value);
                        }
                        else
                        {
                            _videoFileWriter.WriteVideoFrame(bitmap);
                            _firstFrameTime = DateTime.Now;
                        }
                    }
                }
                */
                /* Clone() returns image from the AForge.Video.NewFrameEventArgs eventArgs and it's mapped to Bitmap type */
                using (var bmp = (Bitmap)eventArgs.Frame.Clone())
                {
                    var bmpImg = bmp.convertBitmapToBitmapImage(); /* had to be defined in referenced STATIC class */
                    bmpImg.Freeze(); /* Freeze make the bmpImg unmodifable and sets it's Frozen property to true */
                    /* Dispatcher provides services formanaging the queue of task for a thread */
                    /* CurrentDispatcher is Property of Dipsatcher, associated with the Dispatcher for a task  */
                    /* Ivoke() executes the task described in argument */
                    /* () => Bitmap = bmpImg   ---   that's the task , the syntax is () = > function */
                    /* So my task is to save frozen bitmapImage to _bitmap field */
                    Dispatcher.CurrentDispatcher.Invoke( () => Bitmap = bmpImg );
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error on _videoSource_NewFrame:\n" + exc.Message, "Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                //StopAquisition();
            }
        }

        private void startAquisition()
        {
            if (IsDesktopSource)
            {
                var rectangle = new Rectangle();
                /* ADD System.Windows.Forms dll */
                /* Screen represents display device(s) on the System */
                /* AllScreens returns an array of all displays on the System*/
                foreach (var screen in System.Windows.Forms.Screen.AllScreens)
                {
                    /* Union sums up rectagnles for all the displays */
                    rectangle = Rectangle.Union(rectangle, screen.Bounds);
                }
                /* The ScreenCaptureSystem heritages after AForge.Video.IVideoSource */
                /* The ScreenCaptureSystem provides public event NewFrameEventHandler, Frames Received, BytesReceived, FrameInterval,
                IsRunning, VideoSourceErrorEventHandler, Start, Stop etc. ... */
                AForge.Video.IVideoSource _videoSourceInterface = new ScreenCaptureStream(rectangle);
                /* I use C# delegate here associating my method with AForge newFrameEventHandler */
                _videoSourceInterface.NewFrame += myNewFrameEventHandler;
                _videoSourceInterface.Start();
            }
            /*
            else if (IsWebcamSource)
            {
                if (CurrentDevice != null)
                {
                    _videoSource = new VideoCaptureDevice(CurrentDevice.MonikerString);
                    _videoSource.NewFrame += video_NewFrame;
                    _videoSource.Start();
                }
                else
                {
                    MessageBox.Show("Current device can't be null");
                }
            }
            else if (IsIpCameraSource)
            {
                _videoSource = new MJPEGStream(IpCameraUrl);
                _videoSource.NewFrame += video_NewFrame;
                _videoSource.Start();
            }
            */
        }
        #endregion
        public void Dispose()
        {
            throw new NotImplementedException();
        }

    }
}