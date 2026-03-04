namespace DataTransfer_WebApp_Pulyala.Models
{
    public class Country
    {
        public string Name { get; set; }
        public string Game { get; set; } //Winter, Summer, Paralympics, Youth Olympics
        public string Sport { get; set; } 
        public string Category { get; set; } // Indoor or Outdoor
        public string FlagImage => $"{Name.Replace(" ", "").ToLower()}.png";

        public string FlagCode => Name switch
        {
            "Canada" => "ca", "Sweden" => "se", "Great Britain" => "gb", "Jamaica" => "jm", 
            "Italy" => "it", "Japan" => "jp", "Germany" => "de", "China" => "cn", 
            "Mexico" => "mx", "Brazil" => "br", "Netherlands" => "nl", "USA" => "us",
            "Thailand" => "th", "Uruguay" => "uy", "Ukraine" => "ua", "Austria" => "at",
            "Pakistan" => "pk", "Zimbabwe" => "zw", "France" => "fr", "Cyprus" => "cy",
            "Russia" => "ru", "Finland" => "fi", "Slovakia" => "sk", "Portugal" => "pt",
            _ => "un" // Placeholder
        };
    }
}
