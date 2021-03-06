﻿namespace ApprovalTests.Reporters
{
    using System;
    using System.Diagnostics;

    using ApprovalTests.Core;

    public class TfsReporter : IEnvironmentAwareReporter
    {
        public static readonly TfsReporter INSTANCE = new TfsReporter();

        public bool IsWorkingInThisEnvironment(string forFile)
        {
            return "TFSBuildServiceHost".Equals(GetParentProcessName());
        }

        public void Report(string approved, string received)
        {
            // does nothing
        }

        // TODO: This belongs in a utility class
        private static Process GetParentProcess(Process currentProcess)
        {
            try
            {
                var pc = new PerformanceCounter("Process", "Creating Process Id", currentProcess.ProcessName);
                return Process.GetProcessById((int)pc.RawValue);
            }
            catch (ArgumentException)
            {
                return null;
            }
        }

        private static string GetParentProcessName()
        {
            var parentProcess = GetParentProcess(Process.GetCurrentProcess());
            return parentProcess == null ? string.Empty : parentProcess.ProcessName;
        }
    }
}