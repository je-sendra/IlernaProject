using IlernaProject.ClassLib.Models;
using YoutubeExplode;
using YoutubeExplode.Converter;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams;

namespace IlernaProject.ClassLib.Utils;

/// <summary>
/// This class is responsible for downloading videos from YouTube.
/// It uses the YoutubeExplode library to handle the downloading process.
/// Repository:
/// https://github.com/Tyrrrz/YoutubeExplode
/// </summary>
public class YoutubeVideoDownloader
{
    /// <summary>
    /// Downloads a video from YouTube using the specified URL and saves it to the specified output path.
    /// </summary>
    /// <param name="videoUrl"></param>
    /// <param name="outputPath"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<OperationResult> DownloadVideoAsync(string videoUrl, string outputPath, CancellationToken cancellationToken)
    {
        try
        {
            // Check if the output file already exists
            if (File.Exists(outputPath))
            {
                string message = $"Output file already exists: {outputPath}";
                return new OperationResult
                {
                    HasFailed = true,
                    InternalMessage = message,
                    UserMessage = message
                };
            }

            await DownloadVideo(videoUrl, outputPath, cancellationToken);

            return new OperationResult
            {
                HasFailed = false,
                InternalMessage = "Download completed successfully.",
                UserMessage = "Download completed successfully."
            };
        }
        catch (OperationCanceledException)
        {
            string message = $"Download was canceled: {outputPath}";
            return new OperationResult
            {
                HasFailed = true,
                InternalMessage = message,
                UserMessage = message
            };
        }
        catch (Exception ex)
        {
            return new OperationResult
            {
                HasFailed = true,
                InternalMessage = ex.Message,
                UserMessage = "An error occurred during the download."
            };
        }
    }

    private static async Task DownloadVideo(string videoUrl, string outputPath, CancellationToken cancellationToken)
    {
        YoutubeClient youtube = new();
        await youtube.Videos.DownloadAsync(videoUrl, outputPath, cancellationToken: cancellationToken);
    }
}