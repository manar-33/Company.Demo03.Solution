namespace Company.Demo03.PL.Helper
{
    public static class DocumentSettings
    {
        public static string Upload (IFormFile file , string folderName)
        {
            //1.Get Location of the Folder
            //string folderPath = $"D:\\C# Task\\Company.Demo03.Solution\\Company.Demo03.PL\\wwwroot\\files\\{folderName}\\";
            //string folderPath = Directory.GetCurrentDirectory()+ $"\\wwwroot\\files\\{folderName}";
            string folderPath = Path.Combine(Directory.GetCurrentDirectory() + $"\\wwwroot\\files\\{folderName}");
            //2.Get Location of the File and make it unique
            string fileName = $"{Guid.NewGuid()}{file.FileName}";
            //3.Get file Path : folderPath + filename
            string filePath=Path.Combine(folderPath, fileName);
            //4.File Stream
            using var FileStream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(FileStream);
            return fileName;
        }
        public static void Delete(string fileName, string folderName) 
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory() + $"\\wwwroot\\files\\{folderName},{fileName}");
            if (File.Exists(filePath)) 
            File.Delete(filePath);
        }
    }
}
