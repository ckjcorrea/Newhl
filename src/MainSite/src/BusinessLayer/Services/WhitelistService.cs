using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlwaysMoveForward.OAuth.Common.Configuration;

namespace AlwaysMoveForward.OAuth.BusinessLayer.Services
{
    public class WhiteListService : IWhiteListService
    {
        public bool AllowUnauthorizedAccess(Uri queryString, WhiteListConfiguration configuration)
        {
            bool retVal = false;

            if (queryString != null && configuration != null)
            {
                string[] folderList = configuration.FolderWhitelist.Split(';');
                retVal = this.AllowUnauthorizedAccessToFolders(queryString, folderList);

                if (retVal == false)
                {
                    string[] fileTypeWhiteList = configuration.FileTypeWhitelist.Split(';');
                    retVal = this.AllowUnauthorizedAccessToFileTypes(queryString, fileTypeWhiteList);
                }
            }
            else
            {
                retVal = true;
            }

            return retVal;
        }

        public bool AllowUnauthorizedAccessToFolders(Uri queryString, string[] folderList)
        {
            bool retVal = false;

            if (queryString != null && folderList != null)
            {
                for (int i = 0; i < folderList.Length; i++)
                {
                    if(this.AllowUnauthorizedAccessToFolder(queryString, folderList[i]))
                    {                    
                        retVal = true;
                        break;
                    }
                }
            }
            else
            {
                retVal = true;
            }

            return retVal;
        }

        public bool AllowUnauthorizedAccessToFolder(Uri queryString, string folder)
        {
            bool retVal = false;

            if (queryString != null && folder != null)
            {
                if (!folder.EndsWith("/"))
                {
                    folder += "/";
                }
                
                if (queryString.Segments.Contains(folder))
                {
                    retVal = true;
                }
            }
            else
            {
                retVal = true;
            }

            return retVal;
        }

        public bool AllowUnauthorizedAccessToFileTypes(Uri queryString, string[] fileTypeList)
        {
            bool retVal = false;

            if (queryString != null && fileTypeList != null)
            {
                for (int i = 0; i < fileTypeList.Length; i++)
                {
                    if (queryString.OriginalString.EndsWith(fileTypeList[i]))
                    {
                        retVal = true;
                        break;
                    }
                }
            }
            else
            {
                retVal = true;
            }

            return retVal;
        }
    }
}
