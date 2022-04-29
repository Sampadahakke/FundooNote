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
    public class CollabRL:ICollabRL
    {
        FundoContext fundo;
        public IConfiguration Configuration { get; }

        //Creating constructor for initialization
        public CollabRL(FundoContext fundo, IConfiguration configuration)
        {
            this.fundo = fundo;
            this.Configuration = configuration;
        }

        public async Task<Collaborator> AddCollaborator(int userId,int NoteId,CollaboratorValidation collab)
        {
            try
            {
                var user = fundo.Users.FirstOrDefault(u => u.userID == userId);
                var note = fundo.Notes.FirstOrDefault(b => b.NoteId == NoteId);
                Collaborator collaborator = new Collaborator
                {
                    User = user,
                    Note = note
                };
                collaborator.CollabEmail = collab.email;
                fundo.Collaborators.Add(collaborator);
                await fundo.SaveChangesAsync();
                return collaborator;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> RemoveCollaborator(int userId, int NoteId, int collaboratorId)
        {
            try
            {
                var result = fundo.Collaborators.FirstOrDefault(u => u.userId == userId && u.NoteId == NoteId && u.collaboratorId == collaboratorId);
                if (result != null)
                {
                    fundo.Collaborators.Remove(result);
                    await fundo.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        public async Task<List<Collaborator>> GetCollaboratorByUserId(int userId)
        {
            try
            {
                List<Collaborator> result = await fundo.Collaborators.Where(u => u.userId == userId).Include(u=>u.User).Include(U=>U.Note).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<List<Collaborator>> GetCollaboratorByNoteId(int userId,int NoteId)
        {
            try
            {
                List<Collaborator> result = await fundo.Collaborators.Where(u => u.userId == userId&&u.NoteId==NoteId).Include(u => u.User).Include(U => U.Note).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
