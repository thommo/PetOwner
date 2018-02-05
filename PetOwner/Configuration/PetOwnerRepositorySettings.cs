namespace PetOwner.Configuration
{
    public class PetOwnerRepositorySettings
    {
        public string PetOwnerUrl { get; set; }
        public bool UseProxy { get; set; }
        public string ProxyAddress { get; set; }
        public int ProxyPort { get; set; }
        public string ProxyUsername { get; set; }
        public string ProxyPassword { get; set; }
    }
}
