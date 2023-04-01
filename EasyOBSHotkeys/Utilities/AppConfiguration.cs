using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace EasyOBSHotkeys.Utilities
{
    internal class AppConfiguration
    {
        /// <summary>
        /// Simple data structure to help parsing the JSON file
        /// </summary>
        internal class Command
        {
            public uint KeyCode { get; set; }

            public string? Scene { get; set; }

            public override string ToString() => $"0x{KeyCode:X2}: \"{Scene}\"";
        }

        /// <summary>
        /// Server to connect to, including port
        /// </summary>
        [Required]
        public string Server { get; set; } = null!;

        /// <summary>
        /// Password for the OBS websocket, can be null
        /// </summary>
        public string? Password { get; set; }

        /// <summary>
        /// Commands (bindings for Virtual KeyCodes to Scene names
        /// </summary>
        public IEnumerable<Command>? Commands { get; set; }

        /// <summary>
        /// Dictionary mapping for key codes to scene names
        /// </summary>
        public Dictionary<uint, string?> CommandsDictionary =>
            Commands?.ToDictionary(c => c.KeyCode, c => c.Scene) ?? new Dictionary<uint, string?>();

        /// <summary>
        /// Reads and parses the appsettings.json file
        /// </summary>
        /// <returns>The configuration object, or null</returns>
        public static AppConfiguration? LoadConfig()
        {
            try
            {
                if (File.Exists("appsettings.json"))
                {
                    var rawText = File.ReadAllText("appsettings.json");

                    return JsonSerializer.Deserialize<AppConfiguration>(rawText, new JsonSerializerOptions()
                    {
                        AllowTrailingCommas = true,
                        PropertyNameCaseInsensitive = true,
                        ReadCommentHandling = JsonCommentHandling.Skip
                    });
                }

                MessageBox.Show("Couldn't find the appsettings.json file!", "appsettings.json Error", 
                                MessageBoxButtons.OK, MessageBoxIcon.Error);

                return null;
            }
            catch (JsonException)
            {
                MessageBox.Show("An error occured parsing the appsettings.json file!", "appsettings.json Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);

                return null;
            }
            catch
            {
                MessageBox.Show("An unknown error occured parsing the appsettings.json file!", "appsettings.json Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);

                return null;
            }
        }
    }
}
