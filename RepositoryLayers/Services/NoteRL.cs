using DatabaseLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Entity;
using RepositoryLayer.FundoNoteContext;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RepositoryLayer.Services
{
    public class NoteRL:INoteRL
    {
        //Initializing class
        FundoContext fundo;
        public IConfiguration Configuration { get; }

        //Creating constructor for initialization
        public NoteRL(FundoContext Fundo, IConfiguration configuration)
        {
            this.fundo = Fundo;
            this.Configuration = configuration;
        }

        //Creating method to add note 
        public async Task<Note> AddNote(NotePostModel notePostModel, int userId)
        {
            try
            {
                var user = fundo.Users.FirstOrDefault(u => u.userID == userId);
                Note note = new Note
                {
                    User = user
                };
                note.Title = notePostModel.Title;
                note.Description = notePostModel.Description;
                note.BGColor = notePostModel.BGColor;
                note.IsArchive = false;
                note.IsReminder = false;
                note.IsPin = false;
                note.IsTrash = false;
                note.CreatedAt = DateTime.Now;
                fundo.Add(note);
                await fundo.SaveChangesAsync();
                return note;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Note> GetNote(int noteId, int userId)
        {
            try
            {
                return await fundo.Notes.Where(u => u.NoteId == noteId )
                .Include(u => u.User).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    
}
