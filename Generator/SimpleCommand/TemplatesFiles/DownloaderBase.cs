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

namespace HackPleasanterApi.Generator.SimpleCommand.TemplatesFiles
{
	public class DownloaderBase
	{
		protected string repoUrl;

        public string repoName;

        public string? localPath ;

        string refLocalPath; string ver;

        public DownloaderBase(string repoUrl, string refLocalPath, string ver)
		{
            this.repoUrl = repoUrl;
            this.repoName = repoUrl.Split("/").Last();

            this.refLocalPath = refLocalPath;
            this.ver = ver;

		}

		public string  DownLoad() {

            localPath = Path.Combine(refLocalPath, "TemplatesFiles", repoName);
            Directory.CreateDirectory(localPath);


            var repoPath = localPath;

            var co = new CloneOptions();

            co.BranchName = $"release/{ver}";

            Repository.Clone(repoUrl, repoPath, co);

            return localPath;

        }
	}
}

