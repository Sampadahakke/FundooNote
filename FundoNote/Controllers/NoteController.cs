using BusinessLayer.Interfaces;
using DatabaseLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;
using RepositoryLayer.FundoNoteContext;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FundoNote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        FundoContext fundo;
        INoteBL noteBL;
        public NoteController(INoteBL noteBL, FundoContext fundo)
        {
            this.noteBL = noteBL;
            this.fundo = fundo;
        }

        //HTTP method to handle registration user request
        [HttpPost("AddNote")]
        public async Task<ActionResult> AddNote(NotePostModel notePostModel)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                var result = await this.noteBL.AddNote(notePostModel, userId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Note Added Successfully!!", data = result });

                }
                return this.BadRequest(new { success = true, message = "Failed to add!!" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpGet("GetNote/{NoteId}")]
        public async Task<ActionResult> GetNote(int NoteId)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                var result = await this.noteBL.GetNote(NoteId, userId);
                if (result != null)
                {
                   return this.Ok(new { success = true, message = $"Note get successfully", data = result });
                }
                return this.BadRequest(new { success = true, message = $"Failed to get note" });
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
