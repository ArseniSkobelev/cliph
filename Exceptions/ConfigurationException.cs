namespace cliph.Library;

public class ConfigurationException : Exception
{
    /// <summary>
    /// Used when a configuration value is inaccessible (is empty, is null, is whitespace etc.)
    /// </summary>
    /// <param name="message"></param>
    public ConfigurationException(string message) : base(message)
    {
        
    }
}