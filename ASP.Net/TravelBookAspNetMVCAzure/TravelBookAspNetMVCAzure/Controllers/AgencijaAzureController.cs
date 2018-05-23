﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace TravelBookAspNetMVCAzure.Models
{
    public class AgencijaAzureController : ApiController
    {
        private TravelBookModel db = new TravelBookModel();

        // GET: api/AgencijaAzure
        public IQueryable<AgencijaAzure> GetAgencijaAzures()
        {
            return db.AgencijaAzures;
        }

        // GET: api/AgencijaAzure/5
        [ResponseType(typeof(AgencijaAzure))]
        public async Task<IHttpActionResult> GetAgencijaAzure(string id)
        {
            AgencijaAzure agencijaAzure = await db.AgencijaAzures.FindAsync(id);
            if (agencijaAzure == null)
            {
                return NotFound();
            }

            return Ok(agencijaAzure);
        }

        // PUT: api/AgencijaAzure/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAgencijaAzure(string id, AgencijaAzure agencijaAzure)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != agencijaAzure.id)
            {
                return BadRequest();
            }

            db.Entry(agencijaAzure).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AgencijaAzureExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/AgencijaAzure
        [ResponseType(typeof(AgencijaAzure))]
        public async Task<IHttpActionResult> PostAgencijaAzure(AgencijaAzure agencijaAzure)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AgencijaAzures.Add(agencijaAzure);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AgencijaAzureExists(agencijaAzure.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = agencijaAzure.id }, agencijaAzure);
        }

        // DELETE: api/AgencijaAzure/5
        [ResponseType(typeof(AgencijaAzure))]
        public async Task<IHttpActionResult> DeleteAgencijaAzure(string id)
        {
            AgencijaAzure agencijaAzure = await db.AgencijaAzures.FindAsync(id);
            if (agencijaAzure == null)
            {
                return NotFound();
            }

            db.AgencijaAzures.Remove(agencijaAzure);
            await db.SaveChangesAsync();

            return Ok(agencijaAzure);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AgencijaAzureExists(string id)
        {
            return db.AgencijaAzures.Count(e => e.id == id) > 0;
        }
    }
}