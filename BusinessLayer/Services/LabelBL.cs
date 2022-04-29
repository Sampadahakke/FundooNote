using BusinessLayer.Interfaces;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class LabelBL : ILabelBL
    {
        IlabelRL labelRL;
        public LabelBL(IlabelRL ilabelRL)
        {
           this.labelRL = ilabelRL;
        }
        public async Task Addlabel(int userId, int Noteid, string LabelName)
        {
            try
            {
                await this.labelRL.Addlabel(userId, Noteid, LabelName);  
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
