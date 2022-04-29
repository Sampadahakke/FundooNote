using BusinessLayer.Interfaces;
using DatabaseLayer;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class NoteBL : INoteBL
    {
        INoteRL noteRL;
        public NoteBL(INoteRL noteRL)
        {
            this.noteRL = noteRL;
        }
        
        //Creating asyncronous method to add note
        public async Task<Note> AddNote(NotePostModel notePostModel, int userId)
        {
            try
            {
                return await this.noteRL.AddNote(notePostModel, userId);    
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
                return await this.noteRL.GetNote(noteId, userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
