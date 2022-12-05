using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models.DTOs;
using API.Models.Entities;
using API.Models.Helpers;
using API.Models.Persistence;
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

        private object GetUserWithImages(User user)
        {
            var imageList = new List<String>(); 
            foreach(var image in user.Images)
                {
                    imageList.Add(image.Url);
                }
                
            return new UserDto
            {
                UserId = user.UserId,
                Email = user.Email,
                Name = user.Name,
                ImageUrl = imageList
            };
        }

        // POST /api/users
        [HttpPost] // Adding a user to API Database
        public async Task<IActionResult> CreateUser(User user)
        {
            //need to implement not adding a user if a similar entry i.e new_email = db_email to not add
            try
            {
                await _database.Users.AddAsync(user);
                await _database.SaveChangesAsync();
                return Ok(user);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        // DELETE /api/users/{ID}
        [HttpDelete("{id}")] //Deleting a User from API database
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var selectedUser = _database.Users
                                    .Include(x => x.Images)
                                    .SingleOrDefault(x => x.UserId == id);
            if (selectedUser == null)
                return NotFound();

            foreach(var image in selectedUser.Images)
            {
                 _database.Images.Remove(image);
            }

            _database.Users.Remove(selectedUser);

            await _database.SaveChangesAsync();
            return Ok();
        }
        
        // GET /api/users/353ab842-5a82-4444-b7a1-5d8daad82da6
        [HttpGet("{id}")] //Obtaining a User from API database
        public async Task<IActionResult> GetPersonIdAsync(string id)
        {
            try
            {
                var selectedUser = await _database.Users
                                                .Include(x => x.Images)
                                                .SingleOrDefaultAsync(x => x.UserId.Equals(new Guid(id)));
                
                if (selectedUser == null)
                    return NotFound();

                return Ok(GetUserWithImages(selectedUser));
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }
        
        // POST /api/users/{ID}/image
        [HttpPost("{id}/image")]
        public async Task<IActionResult> PostImageAsync(string id, Image image)
        {   
            try
            {
                var selectedUser = await _database.Users
                                                .Include(x => x.Images)
                                                .SingleOrDefaultAsync(x => x.UserId == new Guid(id));

                selectedUser.Images.Add(image);

                await _database.Images.AddAsync(image);
                await _database.SaveChangesAsync();

                return Ok(GetUserWithImages(selectedUser)); //need to return a user object with only 10 photos
            }
            catch (System.Exception)
            {

                return BadRequest();
            }
        }
    }
}