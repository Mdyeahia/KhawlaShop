using Khawla.Data;
using Khawla.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khawla.Service
{
    public class SharedService
    {
        #region Singleton
        public static SharedService Instance
        {
            get
            {
                if (instance == null) instance = new SharedService();

                return instance;
            }
        }
        private static SharedService instance { get; set; }

        private SharedService()
        {
        }

        #endregion
        KhawlaDbContext context = new KhawlaDbContext();
        public int SavePicture(Picture picture)
        {
            context.Pictures.Add(picture);
            context.SaveChanges();

            return picture.Id;
        }
    }
}
