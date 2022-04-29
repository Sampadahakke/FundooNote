using Microsoft.Extensions.Configuration;
using RepositoryLayer.FundoNoteContext;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class LabelRL:IlabelRL
    {
        FundoContext fundo;
        public IConfiguration Configuration { get; }

        //Creating constructor for initialization
        public LabelRL(FundoContext fundo, IConfiguration configuration)
        {
            this.fundo = fundo;
            this.Configuration = configuration;
        }

        public async Task Addlabel(int userId, int Noteid, string LabelName)
        {
            try
            {
                var user = fundo.Users.FirstOrDefault(u=>u.userID == userId);
                var note = fundo.Notes.FirstOrDefault(b=>b.NoteId == Noteid);
                Entity.Label label = new Entity.Label
                {
                   User = user,
                   Note = note
                };
                label.LabelName=LabelName;
                fundo.Labels.Add(label);
                await fundo.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
