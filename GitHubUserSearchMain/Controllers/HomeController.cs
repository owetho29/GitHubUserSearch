using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GitHubUserSearchMain.Models;

namespace GitHubUserSearchMain.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> Search(string searchterm)
        {
            //Initiate the OctoKit API
            var client = new GitHubClient(new ProductHeaderValue("GitUserSearchTest"));

            //Create a list for all found users
            List<SearchUser> resultList = new List<SearchUser>();

            //Store the term given by the user
            string userRequest = searchterm;

            //OctoKit request to search for that user
            var request = new SearchUsersRequest(userRequest);

            //Octokit results of the search
            var searchResult = await client.Search.SearchUsers(request);

            //IF: users found
            if (searchResult.TotalCount != 0)
            {
                //FOREACH: user found in the search results
                foreach (var r in searchResult.Items)
                {
                    //Store the repositories of each user found
                    User gitHubUser = await client.User.Get(r.Login);
                    IReadOnlyList<Repository> githubRepositories = await client.Repository.GetAllForUser(gitHubUser.Login);

                    //Create a user internally based upon the search results
                    var user = new SearchUser(gitHubUser.Login, githubRepositories, searchResult, gitHubUser.HtmlUrl);

                    //Add this user to the result list
                    resultList.Add(user);
                }
                
                //Return the view with the attached list
                return View(resultList);
            }
            //ELSE: No users found
            else
            {
                //Build the class with the search results and pass to the view
                var user = new SearchUser(userRequest, searchResult);

                //Return the view with no userlist found.
                return View(resultList);
            }


        }
    }
}