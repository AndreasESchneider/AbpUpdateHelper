﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using AbpUpdateHelper.Services;

namespace AbpUpdateHelper
{
    public class WinMergeAction : ExternalMergeActionBase
    {
        protected override void RunComparer(FileGroup fileGroup, string destinationPath)
        {
            var merge = new Process
            {
                StartInfo =
                {
                    FileName = GetWinMergeFile().Item2,
                    Arguments = $" /e \"{fileGroup.NewAbpFile.File.FullName}\" \"{destinationPath}\""
                }
            };

            merge.Start();

            merge.WaitForExit();
        }

        private Tuple<bool, string> GetWinMergeFile()
        {
            var pathList = new List<string>
            {
                $"{AbpFileHelper.GetProgramFilesPath()}\\WinMerge\\WinMergeU.exe",
                $"{AbpFileHelper.GetProgramFilesX86Path()}\\WinMerge\\WinMergeU.exe"
            };

            foreach (var path in pathList)
            {
                if (File.Exists(path))
                {
                    return new Tuple<bool, string>(true, path);
                }
                
            }
            return new Tuple<bool, string>(false,null);
        }

        public override bool Match(FileGroup fileGroup)
        {
            if (!GetWinMergeFile().Item1)
            {
                return false;
            }

            return base.Match(fileGroup);
        }
    }
}