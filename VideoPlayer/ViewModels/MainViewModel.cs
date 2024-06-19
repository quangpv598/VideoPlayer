using CommunityToolkit.Mvvm.ComponentModel;
using FlyleafLib.Controls.WPF;
using FlyleafLib.MediaPlayer;
using FlyleafLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using System.Xml.Linq;

namespace VideoPlayer.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        public Player Player { get; set; }
        public Config Config { get; set; }

        private string _lastError;
        public string LastError
        {
            get => _lastError;
            set => SetProperty(ref _lastError, value);
        }

        private bool _showDebug;
        public bool ShowDebug
        {
            get => _showDebug;
            set => SetProperty(ref _showDebug, value);
        }

        public ICommand ToggleDebug { get; set; }

        public MainViewModel()
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

            Config = new Config();

            // Inform the lib to refresh stats
            Config.Player.Stats = true;

            Player = new Player(Config);

            // Keep track of error messages
            Player.OpenCompleted += (o, e) => { LastError = e.Error; };
            Player.BufferingCompleted += (o, e) => { LastError = e.Error; };
        }
    }
}
