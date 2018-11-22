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
            var client = new GitHubClient(new ProductHeaderValue("GitUserSearchTest"));

            string userRequest = searchterm;
            var request = new SearchUsersRequest(userRequest);

            var searchResult = await client.Search.SearchUsers(request);

            if (searchResult.TotalCount != 0)
            {
                //A test to retrieve respositories of a specific GitHub User
                User gitHubUser = await client.User.Get(userRequest);
                IReadOnlyList<Repository> githubRepositories = await client.Repository.GetAllForUser(gitHubUser.Login);

                var user = new SearchUser(userRequest, githubRepositories, searchResult);

                return View(user);
            }
            else
            {
                //Build the class with the search results and pass to the view

                //var user = new SearchUser(userRequest, githubRepositories, searchResult);

                return View(user);
            }


        }
    }
}