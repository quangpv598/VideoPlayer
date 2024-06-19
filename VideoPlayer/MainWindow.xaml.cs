﻿using FlyleafLib.MediaPlayer;
using FlyleafLib;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using FlyleafLib.Controls.WPF;
using System.Windows.Media;

namespace VideoPlayer
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public Player Player { get; set; }
        public Config Config { get; set; }

        public string LastError { get => _LastError; set { if (_LastError == value) return; _LastError = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LastError))); } }
        string _LastError;

        public bool ShowDebug { get => _ShowDebug; set { if (_ShowDebug == value) return; _ShowDebug = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ShowDebug))); } }
        bool _ShowDebug;

        public ICommand ToggleDebug { get; set; }

        public MainWindow()
        {
            // Initializes Engine (Specifies FFmpeg libraries path which is required)
            Engine.Start(new EngineConfig()
            {
#if DEBUG
                LogOutput = ":debug",
                LogLevel = LogLevel.Debug,
                FFmpegLogLevel = FFmpegLogLevel.Warning,
#endif

                PluginsPath = ":Plugins",
                FFmpegPath = ":FFmpeg",

                // Use UIRefresh to update Stats/BufferDuration (and CurTime more frequently than a second)
                UIRefresh = true,
                UIRefreshInterval = 100,
                UICurTimePerSecond = false // If set to true it updates when the actual timestamps second change rather than a fixed interval
            });

            ToggleDebug = new RelayCommandSimple(new Action(() => { ShowDebug = !ShowDebug; }));

            InitializeComponent();

            Config = new Config();

            // Inform the lib to refresh stats
            Config.Player.Stats = true;

            Player = new Player(Config);

            DataContext = this;

            // Keep track of error messages
            Player.OpenCompleted += (o, e) => { LastError = e.Error; };
            Player.BufferingCompleted += (o, e) => { LastError = e.Error; };
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
