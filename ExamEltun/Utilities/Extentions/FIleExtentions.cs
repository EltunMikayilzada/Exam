namespace ExamEltun.Utilities.Extentions
{
    public static class FIleExtentions
    {
        public static bool CheckFileType(this IFormFile file, string type) 
        {
            if (file.ContentType.Contains(type))
            {
                return true;
            }
            return false;
        }
        public static bool CheckFileSize(this IFormFile file, int kb)
        {
            if (file.Length<kb*1024)
            {
                return true;
            }
            return false;
        }
        public static async Task<string>  CreateFileAsync(this IFormFile file, string root,string folder)
        {
            string Customfilenam = Guid.NewGuid().ToString() + "_" + file.FileName;
            string path=Path.Combine(root,folder, Customfilenam);
            using (FileStream stream =new FileStream(path,FileMode.Create)) 
            {
                file.CopyToAsync(stream);
            }
            return Customfilenam;
        }
        public static bool DeleteFileAsync(this string filename, string root, string folder)
        {
            string path = Path.Combine(root, folder,filename);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
                return true;
            }
            return false;
        }
    }
}
