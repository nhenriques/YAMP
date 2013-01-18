﻿using System;
using System.IO;

namespace YAMP
{
    [Kind(PopularKinds.System)]
    [Description("Changes the current working directory.")]
    class CdFunction : SystemFunction
    {
        [Description("The change directory method changes the currently set working directory. This enables you to access all files in the new directory with their file name only.")]
        [Example("cd(\"..\")", "Navigates to the upper directory of the current working directory.")]
        [Example("cd(\"C:/\")", "Navigates to the root directory of the drive C.")]
        [Example("cd(\"../../../\")", "Navigates three directories up.")]
        public StringValue Function(StringValue path)
        {
            var p = path.Value;

            if (!Path.IsPathRooted(path.Value))
                p = Path.Combine(Environment.CurrentDirectory, path.Value);

            if (!Directory.Exists(path.Value))
            {
                Parser.RaiseNotification("cd", new NotificationEventArgs(NotificationType.Failure, "The directory " + p + " could not be found."));
                return null;
            }

            Environment.CurrentDirectory = path.Value;
            return null;
        }
    }
}
