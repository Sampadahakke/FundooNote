using DatabaseLayer;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface INoteBL
    {
        Task<Note> AddNote(NotePostModel notePostModel, int userId);
        Task<Note> GetNote(int noteId, int userId);


    }
}
