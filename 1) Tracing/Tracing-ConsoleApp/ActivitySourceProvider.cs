using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracing_ConsoleApp
{
    internal static class ActivitySourceProvider
    {
        internal static ActivitySource Source = new ActivitySource(OpenTelemetryConstants.ActivitySourceName);

        internal static ActivitySource SourceFile = new ActivitySource(OpenTelemetryConstants.ActivitySourceFileName);

    }
}
