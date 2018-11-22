using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GitHubUserSearchMain.Models
{
    public class SearchUser
    {
        public string Name { get; set; }
        public IReadOnlyList<Repository> GitHubRepositories { get; set; }
        public SearchUsersResult Result { get; set; }

        public SearchUser(string user, IReadOnlyList<Repository> gitHubRepo, SearchUsersResult result)
        {
            this.GitHubRepositories = gitHubRepo;
            this.Name = user;
            this.Result = result;
        }
    }
}