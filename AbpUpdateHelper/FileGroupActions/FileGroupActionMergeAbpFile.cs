﻿using System.Collections.Generic;
using System.Linq;
using AbpUpdateHelper.Services;

namespace AbpUpdateHelper.FileGroupActions
{
    public class FileGroupActionMergeAbpFile : IFileGroupAction
    {
        private readonly IEnumerable<IMergeAction> _mergeActions;

        public FileGroupActionMergeAbpFile(IEnumerable<IMergeAction> mergeActions)
        {
            _mergeActions = mergeActions;
        }

        public void Run(FileGroup fileGroup, string destinationFolder)
        {
            var mergeAction = _mergeActions.First(pr => pr.Match(fileGroup));

            mergeAction.Run(fileGroup, destinationFolder);
        }

        public bool Match(FileGroup fileGroup)
        {
            if (fileGroup.NewAbpFile != null && fileGroup.CurrentAbpFile != null && fileGroup.ProjectFile != null)
            {
                //
                // the new and the current abp file must not match (an merge would not give sense in this case)
                //
                if (AbpFileHelper.FilesAreEqual(fileGroup.NewAbpFile.File, fileGroup.CurrentAbpFile.File))
                {
                    return false;
                }

                //
                // if the current abp and the project file do not match matches the project meeds to be updated with the content of the new abp file
                //
                if (!AbpFileHelper.FilesAreEqual(fileGroup.CurrentAbpFile.File, fileGroup.ProjectFile.File))
                {
                    return true;
                }
            }

            return false;
        }
    }
}