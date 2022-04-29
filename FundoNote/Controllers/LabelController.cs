using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.FundoNoteContext;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FundoNote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        FundoContext fundo;
        ILabelBL labelBL;
        public LabelController(ILabelBL labelBL, FundoContext fundo)
        {
            this.labelBL = labelBL;
            this.fundo = fundo;
        }

        //HTTP method to handle add label request
        [Authorize]
        [HttpPost("Addlabel/{NoteId}/{LabelName}")]
        public async Task<ActionResult> AddLabel(int NoteId, string LabelName)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                await this.labelBL.Addlabel(userId,NoteId,LabelName);
                return this.Ok(new { success = true, message = $"Label added successfully" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
