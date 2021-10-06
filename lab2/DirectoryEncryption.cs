using System.IO;

namespace lab2
{
    public static class DirectoryEncryption
    {
        private const string EXTENSION = ".enc";

        private const string SYSTEM_FILE = "System";
        
        public static void Encrypt(string rootDirectoryPath, bool toEncrypt)
        {
            var rootDirectory = new DirectoryInfo(rootDirectoryPath);
            
            var directories = rootDirectory.GetDirectories();
            
            foreach (var directory in directories)
            {
                Encrypt(directory.FullName, toEncrypt);
            }
            
            var files = rootDirectory.GetFiles();
            
            foreach (var file in files)
            {
                if(file.FullName.Contains(SYSTEM_FILE)) continue;
                
                if (file.Extension.Equals(EXTENSION))
                {
                    if(toEncrypt){ continue;}
                    
                    EncryptFile(file, false);
                }
                else
                {
                    if(!toEncrypt) { continue;}
                    
                    EncryptFile(file, true);
                }
            }
        }

        private static async void EncryptFile(FileInfo fileInfo, bool toEncrypt)
        {
            byte[] array;
            
            using (var fileReadStream = fileInfo.OpenRead())
            {
                array = new byte[fileInfo.Length];

                await fileReadStream.ReadAsync(array, 0, array.Length);
            }

            var decryptedText = toEncrypt ? Cryptography.Encrypt(array) : Cryptography.Decrypt(array);

            using (var fileWriteStream = new FileStream(fileInfo.FullName, FileMode.Create))
            {
                await fileWriteStream.WriteAsync(decryptedText, 0, decryptedText.Length);
            }

            if (toEncrypt)
            {
                File.Move(fileInfo.FullName, fileInfo.FullName + EXTENSION);
            }
            else
            {
                File.Move(fileInfo.FullName, fileInfo.FullName.Replace(EXTENSION, string.Empty));
            }
        }
    }
}