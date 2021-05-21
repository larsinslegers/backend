using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlankApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using PlankApi.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace PlankApi.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [Route("api/[controller]")] //route naar de controler
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class CuttingBoardsController : ControllerBase
    {
        private readonly IPlankRepository _plankRepository;
        private readonly ICustomerRepository _customerRepository;


        public CuttingBoardsController(IPlankRepository plankRepository, ICustomerRepository customerRepository)
        {
            _plankRepository = plankRepository;
            _customerRepository = customerRepository;
        }
        /// <summary>
        /// Get all cutting boards ordered by title
        /// </summary>
        /// <returns>array of cutting boards</returns>
        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<Plank> GetAllPlanken(string titel = null, string materiaal = null, string tag = null)
        {
            if (string.IsNullOrEmpty(titel)&&string.IsNullOrEmpty(materiaal)&&string.IsNullOrEmpty(tag))
                return _plankRepository.GetAll().OrderByDescending(r => r.aantalInStock);
            return _plankRepository.GetBy(titel, materiaal, tag).OrderBy(r => r.Titel);
        }

        /// <summary>
        /// Get the cutting board with given id
        /// </summary>
        /// <param name="id">the id of the cutting board</param>
        /// <returns>The cutting board</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<Plank> GetPlank(int id)
        {
            Plank plank = _plankRepository.GetBy(id);
            if (plank == null)
                return NotFound();
            return plank;
        }

        /// <summary>
        /// Adds a new cutting board
        /// </summary>
        /// <param name="plank">the new cutting board</param>
        [HttpPost]
        public ActionResult<Plank> Create(PlankDTO plank)
        {
            try
            {
                Plank plankToCreate = new Plank() { Titel = plank.Titel, aantalInStock = plank.AantalInStock, materiaal = plank.Materiaal, prijs = plank.Prijs, hoogte = plank.hoogte, breedte = plank.breedte, dikte = plank.dikte, beschrijving=plank.beschrijving};
                foreach (var i in plank.Tags)
                   plankToCreate.AddTag(new Tag(i.Name));
                _plankRepository.Add(plankToCreate);
                _plankRepository.SaveChanges();

                //201 - link om het nieuwe recept op te vragen
                return CreatedAtAction(nameof(GetPlank), new { id = plankToCreate.Id }, plankToCreate);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        


        /// <summary>
        /// Deletes a cutting board
        /// </summary>
        /// <param name="id">the id of the cutting board to be deleted</param>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Plank plank = _plankRepository.GetBy(id);
            if (plank == null)
            {
                return NotFound();
            }
            _plankRepository.Delete(plank);
            _plankRepository.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// Modifies a cutting board
        /// </summary>
        /// <param name="id">id of the cutting board to be modified</param>
        /// <param name="aantal">the modified cutting board </param>
        
        [HttpPatch("{id}")]
        public IActionResult VerminderAantal(int id, int aantal)
        {
            Plank plank = _plankRepository.GetBy(id);
            if (plank == null)
                return NotFound();
             plank.WeizigAantal(aantal);
            _plankRepository.Update(plank);
            _plankRepository.SaveChanges();
            return NoContent();

        }

        /// <summary>
        /// Get favorite cuttingboard of current user
        /// </summary>
        [HttpGet("Favorites")]
        public IEnumerable<Plank> GetFavorites()
        {
            Customer customer = _customerRepository.GetBy(User.Identity.Name);
            return customer.FavoritePlanken;
        }


        /*
         /// <summary>
         /// Adds an tag to a cutting board
         /// </summary>
         /// <param name="id">the id of the cutting board</param>
         /// <param name="tag">the tag to be added</param>
         [HttpPost("{id}/tags")]
         public ActionResult<Tag> PostIngredient(int id, TagDTO tag)
         {
             if (!_plankRepository.TryGetPlank(id, out var plank))
             {
                 return NotFound();
             }
             var tagToCreate = new Tag(tag.Name);
             plank.AddTag(tagToCreate);
             _plankRepository.SaveChanges();
             return CreatedAtAction("GetIngredient", new { id = plank.Id, tagId = tagToCreate.Id }, tagToCreate);
         }*/
    }
}
