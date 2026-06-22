using System;
using System.Media;
using System.IO;

namespace CybersecurityChatbot
{
    public static class AudioService
    {
        private const string AudioFilePath = "greeting.wav";

        public static void PlayGreeting()
        {
            try
            {
                if (File.Exists(AudioFilePath))
                {
                    using (SoundPlayer player = new SoundPlayer(AudioFilePath))
                    {
                        player.Play();
                    }
                }
                else
                {
                    // Fallback: Play system beep pattern if WAV not found
                    Console.Beep(440, 500);
                    Console.Beep(880, 500);
                    Console.Beep(440, 1000);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Audio error: {ex.Message}");
            }
        }
    }
}