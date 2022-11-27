using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models.Entities;
using API.Models.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {

        //Dependency Injection
        private DataContext _database;
        public UsersController(DataContext database)
        {
            _database = database;
        }


        [HttpPost]

        public async Task<IActionResult> CreateUser(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _database.Users.AddAsync(user);
                    await _database.SaveChangesAsync();
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (System.Exception)
            {

                return BadRequest();
            }
        }
    }
}