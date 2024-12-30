using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ApplicationInsights;
using Microsoft.Extensions.Logging.ApplicationInsights;

namespace Lecture4_azure_fn
{
    public class GetUsers
    {
        //private readonly AppDbContext _context;
        //public GetUsers(AppDbContext context)
        //{
        //    _context = context;
        //}

        private List<User> _users = new List<User>
        {
            new() { Id = 1, Name = "John Doe", Email = "jd@gmail.com" },
            new() { Id = 2, Name = "Alice Smith", Email = "alice@gmail.com" },
            new() { Id = 3, Name = "Chuck Norris", Email = "chuck@gmail.com" },
            new() { Id = 4, Name = "Andrew  Pavlov", Email = "avpavlov87@gmail.com" },
            new() { Id = 5, Name = "Sasha Marfut", Email = "omarfut@gmail.com" },
        };

        [FunctionName("GetUsers")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "users/{id?}")] HttpRequest req,
            ILogger log, 
            int? id)
        {

            if (id.HasValue)
            {
                log.LogInformation($"GetUsers invoked with id = {id}");

                var user = _users.FirstOrDefault<User>(x => x.Id == id);
                if (user == null)
                    return new NotFoundResult();

                return new OkObjectResult(user);
            }
            log.LogInformation($"Function invoked to get all users. Return {_users.Count} users");
            return new OkObjectResult(_users);
        }

    }
}
