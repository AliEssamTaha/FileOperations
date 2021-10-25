using System;

namespace FileOperations.Model
{
    public class File
    {
        public int Id { get; set; }
        public byte[] Content { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
