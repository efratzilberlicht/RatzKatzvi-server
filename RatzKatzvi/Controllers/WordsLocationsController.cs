using BL;
using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RatzKatzvi.Controllers
{
    [RoutePrefix("api/wordsLocations")]
    public class WordsLocationsController : ApiController
    {

        //GetAllWordsLocations
        public IHttpActionResult GetAllWordsLocations()
        {
            return Ok(WordsLocationsBL.GetAllWordsLocations());
        }
        //GetWordsLocationById
        [Route("GetWordsLocationById/{id}")]
        public IHttpActionResult GetWordsLocationById(int id)
        {
            try
            {
                return Ok(WordsLocationsBL.GetWordsLocationById(id));
            }
            catch
            {
                return NotFound();
            }
        }
        //GetByWordId
        [Route("GetAllByName/{keyWord}")]
        public IHttpActionResult GetWordsLocationByWordId(int wordId)
        {
            try
            {
                //List<WordsLocations1> wordLocations = WordsLocationsBL.GetWordsLocationByWordId(wordId);
                //if (wordLocations.Count == 0)
                //    throw new Exception();
                //return Ok(wordLocations);
                //Racheli change- WordsLocationsBL.GetWordsLocationByWordId error
                return NotFound();

            }
            catch
            {
                return NotFound();
            }
        }
        // POST: api/WordsLocations
        public IHttpActionResult Post([FromBody] WordsLocations1 wordLocation)
        {
            try
            {
                WordsLocationsBL.AddWordsLocation(wordLocation);
                return Ok();
            }
            catch
            {
                return Conflict();
            }
        }

        // PUT: api/WordsLocations/5
        //UPDATE
        [HttpPut]
        [Route("Put")]
        public IHttpActionResult Put([FromBody] WordsLocations1 WordsLocation)
        {
            try
            {
                WordsLocationsBL.UpdateWordsLocation(WordsLocation);
                return Ok();
            }
            catch
            {
                return NotFound();
            }
        }

        // DELETE: api/WordsLocations/5
        public IHttpActionResult Delete(WordsLocations1 wordsLocation)
        {
            try
            {
                WordsLocationsBL.DeleteWordsLocation(wordsLocation);
                return Ok();
            }
            catch
            {
                return Conflict();
            }
        }
    }
}
