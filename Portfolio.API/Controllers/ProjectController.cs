using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portfolio.API.Data;

namespace Portfolio.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : Controller
    {
        private readonly DataContext context;

        public ProjectController(DataContext context)
        {
            this.context = context;
        }

        // GET api/project
        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            var projects = await context.Projects.ToListAsync();
            return Ok(projects);
        }

        // GET api/project/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProject(int id)
        {
            var project = await context.Projects.FirstOrDefaultAsync(x=>x.Id == id);
            return Ok(project);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
