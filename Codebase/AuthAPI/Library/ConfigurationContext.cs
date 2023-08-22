namespace cliph.Library;

public static class ConfigurationContext
{
    public static T RetrieveSafeConfigurationValue<T>(IConfiguration configuration, string key)
    {
        if (configuration == null)
            throw new ConfigurationException($"Unable to retrieve configuration object;\nkey={key}");
        
        T? value = configuration.GetValue<T>(key);

        if (value == null)
            throw new ConfigurationException($"The safely retrieved configuration value is null;\nkey={key}");

        if (string.IsNullOrWhiteSpace(value.ToString()))
            throw new ConfigurationException($"The safely retrieved configuration string is null or whitespace;\nkey={key}");

        return value;
    }
}