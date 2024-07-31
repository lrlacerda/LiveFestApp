﻿using LiveFest.Context;
using LiveFest.Domains;
using LiveFest.Interface;
using LiveFest.Repository;
using LiveFest.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LiveFest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaveEventsController : ControllerBase
    {
        private ISaveEventsRepository saveEventsRepository;

        public SaveEventsController()
        {
            saveEventsRepository = new SaveEventsRepository();
        }

        [HttpGet("All")]
        public IActionResult GetAll(Guid userID)
        {
            try
            {
                List<SaveEvents> eventsById = saveEventsRepository.GetAll(userID);

                return Ok(eventsById);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost("Create")]
        public IActionResult SaveEvent([FromBody] SaveEventsViewModel saveEventsViewModel)
        {
            if (saveEventsViewModel == null)
            {
                return BadRequest("Request body cannot be null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userID = saveEventsViewModel.UserID;
            var eventID = saveEventsViewModel.EventID;

            bool eventSaved = saveEventsRepository.SaveEvent(userID, eventID);

            if (eventSaved)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Failed to save event.");
            }
        }
        [HttpDelete("Delete")]
        public IActionResult DeleteSaveEvent(Guid userID, Guid eventID)
        {
            try
            {
                saveEventsRepository.DeleteSaveEvent(userID, eventID);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
