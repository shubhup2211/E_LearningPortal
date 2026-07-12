using ELearningPortal.Interfaces.IAdmin;
using Microsoft.AspNetCore.Mvc;

namespace ELearningPortal.Controllers.Admin
{
    public class AttachmentController : Controller
    {
        IAttachmentService attachService;
        public AttachmentController(IAttachmentService attachService)
        {
            this.attachService = attachService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
