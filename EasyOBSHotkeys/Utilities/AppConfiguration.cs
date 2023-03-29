using System.Text.Json;

namespace EasyOBSHotkeys.Utilities
{
    internal class AppConfiguration
    {
        internal class Command
        {
            public uint KeyCode { get; set; }

            public string? Scene { get; set; }

            public override string ToString() => $"0x{KeyCode:X2}: \"{Scene}\"";
        }

        public string? Server { get; set; }

        public string? Password { get; set; }

        public IEnumerable<Command>? Commands { get; set; }

        public Dictionary<uint, string?> CommandsDictionary =>
            Commands?.ToDictionary(c => c.KeyCode, c => c.Scene) ?? new Dictionary<uint, string?>();

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

                return null;
            }
            catch (JsonException ex)
            {
                // TODO: Log Exception

                MessageBox.Show("An error occured loading the appsettings.json file!", "appsettings.json Error");

                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}
