using BusinessLayer.Interfaces;
using DatabaseLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RepositoryLayer.Entity;
using RepositoryLayer.FundoNoteContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundoNote.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        FundoContext fundo;
        INoteBL noteBL;
        public readonly IDistributedCache distributedCache;
        private string keyName = "Sampada";
        public NoteController(INoteBL noteBL, FundoContext fundo, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.noteBL = noteBL;
            this.fundo = fundo;
            this.distributedCache = distributedCache;
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

        //HTTP method to handle get request
        [Authorize]
        [HttpGet("GetNote/{NoteId}")]
        public async Task<ActionResult> GetNote(int NoteId)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                var res = fundo.Notes.FirstOrDefault(u=>u.NoteId == NoteId);
                if(res == null)
                {
                    return this.BadRequest(new { success = false, message = $"Note doesn't exists..." });
                }
                var result = await this.noteBL.GetNote(NoteId, userId);
                if (result != null)
                {
                   return this.Ok(new { success = true, message = $"Note get successfully", data = result });
                }
                return this.BadRequest(new { success = false, message = $"Failed to get note" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //HTTP method to handle get all notes
        [Authorize]
        [HttpGet("GetAllNotes")]
        public async Task<ActionResult> GetAllNotes()
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                var res = fundo.Notes.FirstOrDefault(u => u.UserId == userId);
                if (res == null)
                {
                    return this.BadRequest(new { success = false, message = $"User doesn't exists..." });
                }
                List<Note> result = new List<Note>();
                result = await this.noteBL.GetAllNote(userId);
                if(result != null)
                {
                    return this.Ok(new { success = true, message = $"Notes get successfully", data = result });
                }
                return this.BadRequest(new { success = true, message = $"Failed to get notes", data = result });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //HTTP method to delete note
        [Authorize]
        [HttpDelete("DeleteNote/{noteId}")]
        public async Task<ActionResult> DeleteNote(int noteId)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                var re = fundo.Notes.Where(x => x.UserId == userId && x.NoteId == noteId).FirstOrDefault();
                if (re == null)
                {
                    return this.BadRequest(new { success = false, message = $"Note doesn't exists" });
                }
                await this.noteBL.DeleteNote(noteId, userId);
                return this.Ok(new { success = true, message = "Note deleted successfully!!!" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //HTTP method to update the note
        [Authorize]
        [HttpPut("UpdateNote/{NoteId}")]
        public async Task<IActionResult> UpdateNote(NotePostModel notePostModel, int NoteId)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                var re = fundo.Notes.Where(x => x.UserId == userId && x.NoteId == NoteId).FirstOrDefault();
                if (re == null)
                {
                   return this.BadRequest(new { success = false, message = $"Note doesn't exists" });
                }
                var result = await this.noteBL.UpdateNote(notePostModel, NoteId, userId);
                return this.Ok(new { success = true, message = $"Note updated successfully!!!", data = result });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //HTTP method to hadle put request
        [Authorize]
        [HttpPut("ArchieveNote/{noteId}")]
        public async Task<ActionResult> IsArchieveNote(int noteId)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                var re = fundo.Notes.Where(x => x.UserId == userId && x.NoteId == noteId).FirstOrDefault();
                if (re == null)
                {
                    return this.BadRequest(new { success = false, message = $"Note doesn't exists" });
                }
                var res = await this.noteBL.ArchieveNote(noteId, userId);
                if (res != null)
                    return this.Ok(new { success = true, message = "Note Archieved successfully!!!" });
                else
                    return this.BadRequest(new { success = false, message = "Failed to archieve note or Id does not exists" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //HTTP method to hadle put request
        [Authorize]
        [HttpPut("IsPinned/{noteId}")]
        public async Task<ActionResult> IsPinned(int noteId)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                var re = fundo.Notes.Where(x => x.UserId == userId && x.NoteId == noteId).FirstOrDefault();
                if (re == null)
                {
                    return this.BadRequest(new { success = false, message = $"Note doesn't exists" });
                }
                var res = await this.noteBL.PinNote(noteId, userId);
                if (res != null)
                    return this.Ok(new { success = true, message = "Note pinned successfully!!!" });
                else
                    return this.BadRequest(new { success = false, message = "Failed to pin note or Id does not exists" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //HTTP method to hadle put request
        [Authorize]
        [HttpPut("IsTrash{noteId}")]
        public async Task<ActionResult> IsTrash(int noteId)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                var re = fundo.Notes.Where(x => x.UserId == userId && x.NoteId == noteId).FirstOrDefault();
                if (re == null)
                {
                    return this.BadRequest(new { success = false, message = $"Note doesn't exists" });
                }
                var res = await this.noteBL.TrashNote(noteId, userId);
                if (res != null)
                    return this.Ok(new { success = true, message = "Note trashed successfully!!!" });
                else
                    return this.BadRequest(new { success = false, message = "Failed to trash note or Id does not exists" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //HTTP method to hadle put request
        [Authorize]
        [HttpPut("ChangeColorNote/{noteId}")]
        public async Task<ActionResult> ChangeColorNote(int noteId, string color)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                var re = fundo.Notes.Where(x => x.UserId == userId && x.NoteId == noteId).FirstOrDefault();
                if (re == null)
                {
                    return this.BadRequest(new { success = false, message = $"Note doesn't exists" });
                }
                var res = await this.noteBL.ChangeColor(noteId, userId, color);
                if (res != null)
                    return this.Ok(new { success = true, message = "Note color changed successfully!!!" });
                else
                    return this.BadRequest(new { success = false, message = "Failed to change color note or Id does not exists" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //GetAllNote api using RedisCache
        
        [HttpGet("GetAllNotesRedis")]
        public async Task<ActionResult> GetAllNotes_ByRadisCache()
        {
            try
            {
                
                string serializeNoteList=string.Empty;
                var noteList = new List<Note>();
                var redisNoteList = await distributedCache.GetAsync(keyName);
                if (redisNoteList != null)
                {
                    serializeNoteList = Encoding.UTF8.GetString(redisNoteList);
                    noteList = JsonConvert.DeserializeObject<List<Note>>(serializeNoteList);
                }
                else
                {

                    noteList = await this.noteBL.GetAllNotes_ByRadisCache();
                    serializeNoteList=JsonConvert.SerializeObject(noteList);
                    redisNoteList=Encoding.UTF8.GetBytes(serializeNoteList);
                    var option = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(20)).SetAbsoluteExpiration(TimeSpan.FromHours(6));
                    await distributedCache.SetAsync(keyName,redisNoteList,option);
                }
                return this.Ok(new { success = true, message = "Get note successful!!!", data=noteList});


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
