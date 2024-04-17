using FFMpegCore;
using FFMpegCore.Enums;
using FFMpegCore.Exceptions;
using FFMpegCore.Helpers;
using FFMpegCore.Pipes;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ffmpegTest
{
    public static class Options
    {
		public static void Split(IMediaAnalysis source, string output, int startSeconds, int stopSeconds)
		{
			FFMpegArguments
				.FromFileInput(source)
				.OutputToFile(output, false, options => options
					.Seek(TimeSpan.FromSeconds((double)startSeconds))
					.WithCustomArgument($"-to { stopSeconds }")
					.ForceFormat("mp4"))
				.ProcessSynchronously();
		}
    }
}