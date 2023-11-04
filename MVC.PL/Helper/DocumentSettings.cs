namespace MVC.PL.Helper
{
    public class DocumentSettings
    {
        public static string UploadFile(IFormFile file,string folderName)
        {
            //1.Get Located Folder Path
            var folderpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/files", folderName);

            //2.Get File Nmae And Make It Unique
            var filename = $"{Guid.NewGuid()}{Path.GetFileName(file.FileName)}";

            //3.Get File Path
            var filepath=Path.Combine(folderpath, filename);

            //4.Save File As Steams
            using var filestream = new FileStream(filepath,FileMode.Create);

            file.CopyTo(filestream);
            return filename;
        }
    }
}
