using ELearningPortal.Interfaces.IAdmin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ELearningPortal.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
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
