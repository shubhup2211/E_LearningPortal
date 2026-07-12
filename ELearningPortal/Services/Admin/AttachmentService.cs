using ELearning.Data;
using ELearningPortal.Interfaces.IAdmin;

namespace ELearningPortal.Services.Admin
{
    public class AttachmentService : IAttachmentService
    {
        private readonly AppDbContext db;
        public AttachmentService(AppDbContext db)
        {
            this.db = db;
        }
    }
}
