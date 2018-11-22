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

            List<SearchUser> resultList = new List<SearchUser>();

            string userRequest = searchterm;
            var request = new SearchUsersRequest(userRequest);

            var searchResult = await client.Search.SearchUsers(request);

            if (searchResult.TotalCount != 0)
            {
                foreach (var r in searchResult.Items)
                {
                    User gitHubUser = await client.User.Get(r.Login);
                    IReadOnlyList<Repository> githubRepositories = await client.Repository.GetAllForUser(gitHubUser.Login);
                    var user = new SearchUser(gitHubUser.Login, githubRepositories, searchResult);
                    resultList.Add(user);
                }
                
                
                return View(resultList);
            }
            else
            {
                //Build the class with the search results and pass to the view
                var user = new SearchUser(userRequest, searchResult);

                return View(user);
            }


        }
    }
}