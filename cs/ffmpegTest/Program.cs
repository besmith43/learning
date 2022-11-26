using System;
using System.IO;
using FFMpegCore;
using FFMpegCore.Enums;
using FFMpegCore.Exceptions;
using FFMpegCore.Helpers;
using FFMpegCore.Pipes;

namespace ffmpegTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string vidFile = @".\alien_interrogation.mp4";

            if (!File.Exists(vidFile))
            {
                vidFile = Path.GetFullPath(vidFile);
            }

            var vidAnalysis = FFProbe.Analyse(vidFile);

            Console.WriteLine(vidAnalysis.Path);

            string output = $"{ Directory.GetCurrentDirectory() }\\Test_Short.mp4";

            if (CommandExists("ffmpeg.exe") && CommandExists("ffprobe.exe"))
            {
                Options.Split(vidAnalysis, output, 5, 60);
            }
            

            /*FFMpegArguments
                .FromFileInput(vidAnalysis)
                .OutputToFile(output, false, options => options
                    .WithVideoCodec(VideoCodec.LibX264)
                    .WithConstantRateFactor(21)
                    .WithAudioCodec(AudioCodec.Aac)
                    .WithVariableBitrate(4)
                    .WithFastStart()
                    .Scale(VideoSize.Hd))
                .ProcessSynchronously();*/
        }

		public static bool CommandExists(string command)
		{
			return GetFullPath(command) != null;
		}

		public static string GetFullPath(string fileName)
		{
		    if (File.Exists(fileName))
		        return Path.GetFullPath(fileName);
		
		    var values = Environment.GetEnvironmentVariable("PATH");
		    foreach (var path in values.Split(Path.PathSeparator))
		    {
		        var fullPath = Path.Combine(path, fileName);
		        if (File.Exists(fullPath))
		            return fullPath;
		    }
		    return null;
		}
    }
}
