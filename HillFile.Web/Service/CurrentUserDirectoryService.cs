using System;

namespace HillFile.Web.Service
{
    public class CurrentUserDirectoryService : ICurrentUserDirectoryService
    {
        public string GetCurrentUserDirectory()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        }
    }
}