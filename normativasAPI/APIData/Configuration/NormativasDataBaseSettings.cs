using System;
namespace normativasAPI.APIData.Configuration
{
    public class NormativasDataBaseSettings : INormativasDataBaseSettings
    {
        public string NormativasCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface INormativasDataBaseSettings
    {
        string NormativasCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
