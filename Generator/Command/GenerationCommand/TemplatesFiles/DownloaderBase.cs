/**
 * Licensed to the Apache Software Foundation (ASF) under one
 * or more contributor license agreements.  See the NOTICE file
 * distributed with this work for additional information
 * regarding copyright ownership.  The ASF licenses this file
 * to you under the Apache License, Version 2.0 (the
 * "License"); you may not use this file except in compliance
 * with the License.  You may obtain a copy of the License at
 * 
 *   http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 * */

using System;
using LibGit2Sharp;
using System.IO;
using HackPleasanterApi.Generator.SimpleCommand.TemplatesFiles.Settings;
using NLog;
using Cysharp.Diagnostics;
using Zx;

namespace HackPleasanterApi.Generator.GenerationCommand.TemplatesFiles
{

    public interface IDownloaderBase
    {
        public string repoName { get; }


        public string DownLoad();
        public Task PostProcessing(string path);

        public SettingsBase settingsBase { get; set; }

    }


    public abstract class DownloaderBase<SettingsBaseType> : IDownloaderBase
        where SettingsBaseType : SettingsBase
    {
        /// <summary>
        /// ロガー
        /// </summary>
        protected static Logger logger = LogManager.GetCurrentClassLogger();


        protected string repoUrl;

        public string repoName { get; }

        public string? localPath;

        string refLocalPath;
        protected SettingsBaseType settingsBase_ { get; set; }


        SettingsBase IDownloaderBase.settingsBase { get => settingsBase_; set => settingsBase_ = (SettingsBaseType)value; }

        public DownloaderBase(string repoUrl, string refLocalPath, SettingsBaseType settingsBase)
        {
            this.repoUrl = repoUrl;
            this.repoName = repoUrl.Split("/").Last();

            this.refLocalPath = refLocalPath;
            this.settingsBase_ = settingsBase;

        }

        public string DownLoad()
        {

            localPath = Path.Combine(refLocalPath, "TemplatesFiles", repoName);
            Directory.CreateDirectory(localPath);


            var repoPath = localPath;

            var co = new CloneOptions();

            co.BranchName = $"release/{this.settingsBase_.TemplateVersion}";

            Repository.Clone(repoUrl, repoPath, co);

            return localPath;

        }

        public virtual async Task PostProcessing(string path)
        {

            logger.Debug("-- 後処理開始 --");

            try
            {
                await PostProcessingImp(path);
            }
            catch (ProcessErrorException ex)
            {
                // int .ExitCode
                // string[] .ErrorOutput
                logger.Error("後処理実行エラー : " + ex.ToString());
            }

            logger.Debug("-- 後処理終了 --");

        }

        protected abstract Task PostProcessingImp(string path);

    }
}

