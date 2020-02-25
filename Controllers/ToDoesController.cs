using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoRest.Models;

namespace ToDoRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoesController : ControllerBase
    {
        private readonly DataToDoRest _context;
        public ToDoesController(DataToDoRest context)
        {
            _context = context;
        }

        // GET: api/ToDoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDo>>> GetToDo()
        {
            return await _context.ToDo.ToListAsync();
        }

        [HttpGet]
        [Route("GetToDoCount")]
        public async Task<int> GetToDoCount()
        {

            return await _context.ToDo.CountAsync();
        }


        [HttpGet]
        [Route("GetToDoCountDel")]
        public async Task<int> GetToDoCountDel()
        {

            return await _context.ToDo.Where(e => e.IsDeleted == true).CountAsync();
        }





        // [HttpGet("{test1}/{test2}")]
        //[Route("api/ToDoes/GetToDo")]
        //public async Task<ActionResult<IEnumerable<ToDo>>> GetToDo(string test1,string test2)
        //{
        //  if(test1== "isdeleted" && test2 == "true")
        //    return await _context.ToDo.Where(e => e.IsDeleted == true).ToListAsync();
        //else if (test1 == "isdeleted" && test2 == "false")
        //  return await _context.ToDo.Where(e => e.IsDeleted == false).ToListAsync();


        //else if (test1 == "isvisible" && test2 == "true")
        //  return await _context.ToDo.Where(e => e.IsVisible == true).ToListAsync();
        //else if (test1 == "isvisible" && test2 == "false")
        //  return await _context.ToDo.Where(e => e.IsVisible == false).ToListAsync();


        //return Content("Not found");

        //}

        [HttpGet]
        [Route("api/ToDoes/GetToDoDelet")]
        public async Task<ActionResult<IEnumerable<ToDo>>> GetToDoDelet()
        {
          
            return await _context.ToDo.Where(e => e.IsDeleted == true).ToListAsync();
      
        

        }

        [HttpGet]
        [Route("api/ToDoes/GetToDoVis")]
        public async Task<ActionResult<IEnumerable<ToDo>>> GetToDoVis()
        {
            
                return await _context.ToDo.Where(e => e.IsVisible == true).ToListAsync();
            
        }




        // GET: api/ToDoes/5
        /// <summary>
        /// GET A TODO ITEM USING ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDo>> GetToDo(int id)
        {
            var toDo = await _context.ToDo.FindAsync(id);

            if (toDo == null)
            {
                return NotFound();
            }

            return toDo;
        }

        // PUT: api/ToDoes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutToDo(int id, ToDo toDo)
        {
            if (id != toDo.Id)
            {
                return BadRequest();
            }

            _context.Entry(toDo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToDoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ToDoes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<ToDo>> PostToDo(ToDo toDo)
        {
            toDo.IsDeleted = false;
            toDo.IsVisible = true;
            _context.ToDo.Add(toDo);
            toDo.URD = System.DateTime.Now;
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetToDo", new { id = toDo.Id }, toDo);
        }

        // DELETE: api/ToDoes/5
        // [HttpDelete("{id}")]
        //public async Task<ActionResult<ToDo>> DeleteToDo(int id)
        //{
        // var toDo = await _context.ToDo.FindAsync(id);
        // if (toDo == null)
        //{
        //  return NotFound();
        //}

        //_context.ToDo.Remove(toDo);
        //await _context.SaveChangesAsync();

        //   return toDo;
        //}

        [HttpDelete("{id}")]
        public async Task<ActionResult<ToDo>> DeleteToDo(int id)
        {
         var toDo = await _context.ToDo.FindAsync(id);
            if (toDo == null)
            {
                return NotFound();
            }

            else
            {
                toDo.IsDeleted = true;
                
            }

           return toDo;
        }


        private bool ToDoExists(int id)
        {
            return _context.ToDo.Any(e => e.Id == id);
        }

        private bool ToDoDeleted(int id)
        {
            return _context.ToDo.Any(e => e.Id == id && e.IsDeleted == true);
        }

    }
}
