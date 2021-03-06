using TodoLocalized.Database;
using System.IO;
using TodoLocalized;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace TodoLocalized
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);

        }
    }
}