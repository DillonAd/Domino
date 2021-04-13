using domino.Logging;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace domino
{
    public sealed class Commander : ICommander
    {
        private readonly string _scriptName;
        private readonly ILogger _logger;
        private readonly ProcessStartInfo _processStartInfo;

        private Process _process;
        private CancellationTokenSource _cancellationTokenSource;
        private Task _executionTask;

        public Commander(ILogger logger, CommanderOptions options)
        {
            _scriptName = options.ScriptName;
            _logger = logger;
            _cancellationTokenSource = new CancellationTokenSource();

            _processStartInfo = new ProcessStartInfo
            {
                FileName = _scriptName,
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            Execute(string.Empty);
        }

        public void Execute(string fileName)
        {
            if (fileName == _scriptName)
            {
                return;
            }

            try
            {
                if (_process != null)
                {
                    _process.Kill();
                    _cancellationTokenSource.Cancel();
                    _cancellationTokenSource.Dispose();
                    _cancellationTokenSource = new CancellationTokenSource();
                    _logger.Info($"{_scriptName} exited...");
                }

                _logger.Info($"Starting {_scriptName}");

                StartProcess();

                _executionTask = Task.Run(() =>
                {
                    _process.WaitForExit();
                    _process = null;
                }, _cancellationTokenSource.Token);
            }
            catch (Exception ex)
            {
                _logger.Error($"{ex.Message} \n {ex.StackTrace}");
            }
        }

        public void Dispose()
        {
            if (_process != null)
            {
                if (!_process.HasExited)
                {
                    _process.Kill();
                    _cancellationTokenSource.Cancel();
                    _cancellationTokenSource.Dispose();
                }
                _process.Dispose();
            }
        }

        private void StartProcess()
        {
            _process = Process.Start(_processStartInfo);
            _process.EnableRaisingEvents = true;

            _process.OutputDataReceived += (sender, args) => _logger.Output(args.Data);
            _process.BeginOutputReadLine();

            _process.ErrorDataReceived += (sender, args) => _logger.Output(args.Data);
            _process.BeginErrorReadLine();

            _process.Exited += (sender, args) =>
            {
                if (_process?.ExitCode == 0)
                {
                    _logger.Info($"Finished {_scriptName}");
                }
            };
        }
    }
}
