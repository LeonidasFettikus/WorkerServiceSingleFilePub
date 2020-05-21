using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace ConfigurationTest
{
    public class JsonFileConfigurationProvider : ConfigurationProvider
    {         
        public override void Load()
        {
            var path = Environment.CurrentDirectory;

            if (Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") == Environments.Development)
                path = Path.Combine(path, "settings.Development.json");
            else
                path = Path.Combine(path, "settings.json");

            using (var stream = File.Open(path, FileMode.Open)) 
            {
                Data = JsonConfigurationFileParser.Parse(stream);
            }
        }
    }
}
