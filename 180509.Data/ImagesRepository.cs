using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _180509.Data
{
    public class ImagesRepository
    {
        private string _connectionString;

        public ImagesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public IEnumerable<Image> GetImages()
        {
            using (var context = new ImagesDataContext(_connectionString))
            {
                return context.Images.ToList().OrderByDescending(i => i.Id);
            }
        }
        public void AddImage(Image image)
        {
            using (var context = new ImagesDataContext(_connectionString))
            {
                context.Images.InsertOnSubmit(image);
                context.SubmitChanges();
            }
        }
        public Image GetById(int id)
        {
            using (var context = new ImagesDataContext(_connectionString))
            {
                return context.Images.FirstOrDefault(i => i.Id == id);
            }
        }
        public void IncrementLikeCount(int imageId)
        {
            using (var context = new ImagesDataContext(_connectionString))
            {
                Image image = context.Images.FirstOrDefault(i => i.Id == imageId);
                image.LikeCount++;    
                context.SubmitChanges();
            }    
        }
    }
}
