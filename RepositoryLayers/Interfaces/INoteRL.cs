using DatabaseLayer;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface INoteRL
    {
        Task<Note> AddNote(NotePostModel notePostModel, int userId);
        Task<Note> GetNote(int noteId, int userId);

    }
}
