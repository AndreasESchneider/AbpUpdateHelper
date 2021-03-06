﻿namespace AbpUpdateHelper.FileGroupActions
{
    public class FileGroupActionRemoveAbpFile : IFileGroupAction
    {
        public void Run(FileGroup fileGroup, string destinationFolder)
        {
            // fileGroup.DeleteCurrentAbpFile(destinationFolder);
        }

        public bool Match(FileGroup fileGroup)
        {
            if (fileGroup.NewAbpFile == null && fileGroup.CurrentAbpFile != null && fileGroup.ProjectFile != null)
            {
                return true;
            }

            return false;
        }
    }
}