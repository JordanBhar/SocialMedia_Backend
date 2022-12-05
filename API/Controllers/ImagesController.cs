using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models.DTOs;
using API.Models.Entities;
using API.Models.Persistence;
using API.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImagesController : ControllerBase
    {
        //Dependency Injection
        private DataContext _database;
        public ImagesController(DataContext database)
        {
            _database = database;
        }

        [HttpGet]
         public async Task<IActionResult> GetImagesAsync([FromQuery] int pagenumber)
         {
            //var response = new PageResponse<Image>();

                var selectedImage = _database.Images
                                        .Include(x => x.User);
                                                    
                var response = new PageResponse<ImageDto>();
                response.Data = (IEnumerable<ImageDto>)selectedImage
                                            .Skip((pagenumber - 1) * 10)
                                            .Take(10);                             

            var totalRecords = await _database.Images.CountAsync();
            var totalPages = Math.Ceiling((Decimal)totalRecords / 10);
            
            response.Meta.Add("PageCount", totalPages.ToString());

            response.Links.Add("first" , $"/api/people?pagenumber=1&pagesize={10}");

            return Ok(response);
            
         }

        [HttpGet("{id}")] //Obtaining a User from API database
        public async Task<IActionResult> GetImageIdAsync(string id)
        {
            try
            {
                var selectedImage = await _database.Images
                                                    .Include(x => x.User)
                                                    .SingleOrDefaultAsync(x => x.Id == new Guid(id));
                                                
                if (selectedImage == null)
                    return NotFound();

                var imageResponse = new ImageDto
                {
                    Id = selectedImage.Id,
                    Url = selectedImage.Url,
                    UserId = selectedImage.User.UserId,
                    UserName = selectedImage.User.Name,
                    PostingDate = selectedImage.PostingDate,
                };
                
                return Ok(imageResponse);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        

    }
}