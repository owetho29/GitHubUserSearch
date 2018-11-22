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
        public string URL { get; set; }

        //Constructor for a user, storing neccesary info
        public SearchUser(string user, IReadOnlyList<Repository> gitHubRepo, SearchUsersResult result, string url)
        {
            this.GitHubRepositories = gitHubRepo;
            this.Name = user;
            this.Result = result;
            this.URL = url;
        }

        //Constructor for when no user is found
        public SearchUser(string user, SearchUsersResult result)
        {
            this.Name = user;
            this.Result = result;
        }
    }
}