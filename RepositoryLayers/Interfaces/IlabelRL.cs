using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IlabelRL
    {
        Task Addlabel(int userId, int Noteid, string LabelName);
    }
}
