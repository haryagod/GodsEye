namespace document_service.Models
{
    public class DocumentDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string VediosCollectionName { get; set; } = null!;
        public string StandardsCollectionName { get; set; } = null!;
    }

    public enum UploadStatus { 
    INTIATED=0,
    INPROGRESS=1
    }
}

