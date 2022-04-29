using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IlabelRL
    {
        Task Addlabel(int userId, int Noteid, string LabelName);
        Task<List<Label>> Getlabel(int userId);
        Task<List<Label>> GetlabelByNoteId(int NoteId);
        Task<Label> UpdateLabel(Label label,int LabelId);
        Task DeleteLabel(int LabelId);


    }
}
