namespace WWsystemLib
{
    public class LoggerSettings
    {
        public string Prefix { get; set; }
        
        public string DefaultPrefix { get; set; }
        public string Path { get; set; }
        
        public bool ToConsole { get; set; }
        
        public bool ToFile { get; set; }
    }
}