using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityList.Code
{
    public static class Utilities
    {
        public static string UploadFileIntoLibrary(SPDocumentLibrary docLibrary, string fileName, byte[] fileContent)
        {
            var url = "";

            try
            {
                docLibrary.ParentWeb.AllowUnsafeUpdates = true;

                var file = TryGetFile(docLibrary.RootFolder, fileName);

                if (file == null)
                {
                    file = docLibrary.RootFolder.Files.Add(fileName, fileContent);

                    file.Item["Title"] = fileName;
                    file.Item.SystemUpdate(false);

                }
                else
                {
                    file.SaveBinary(fileContent);
                }
 
                // file relative url
                url = file.Item.ParentList.ParentWeb.ServerRelativeUrl.TrimEnd('/') + "/" + file.Item.Url;

            }
            finally
            {
                docLibrary.ParentWeb.AllowUnsafeUpdates = false;
            }

            return url;

        }

        public static SPFile TryGetFile(SPFolder parentFolder, string fileUrl)
        {
            try
            {
                return parentFolder.Files[fileUrl];
            }
            catch
            {
                return null;
            }

        }


        public static byte[] GetFileContent(SPWeb web, string fileUrl)
        {
            var libItem = web.GetListItem(fileUrl);

            if (libItem == null || libItem.File == null) return null;

            return libItem.File.OpenBinary(SPOpenBinaryOptions.None);
        }

        public static int GetHashCodeSafe(this string self)
        {
            if (self == null) return 0;

            return self.GetHashCode();
        }

    }
}
