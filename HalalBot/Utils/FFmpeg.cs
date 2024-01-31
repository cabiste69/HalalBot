using System.Diagnostics;

namespace HalalBot.Utils;

public class FFmpeg {
    public bool AddNasheedToVideo(string url, string nasheed, string output) {
        nasheed = Globals.GetRandomNasheed();
        string command = $"-y -i \"{url}\" -i \"{nasheed}\" -map 0:v:0 -map 1:a:0 -shortest \"{output}\"";

        // Create a new Process object.
        Process process = new Process();
        // Set the StartInfo.FileName property to the path of the CMD executable.
        process.StartInfo.FileName = "ffmpeg";
        // Set the StartInfo.Arguments property to the CMD command that you want to execute.
        process.StartInfo.Arguments = command;
        // Start the process.
        process.Start();
        // Wait for the process to finish.
        process.WaitForExit();

        return process.ExitCode == 0;
    }
}