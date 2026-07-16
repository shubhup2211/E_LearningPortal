using ELearning.Data;
using ELearning.Enums;
using ELearning.Models.Purchases;
using ELearningPortal.Configuration;
using ELearningPortal.Interfaces.IUser;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Razorpay.Api;
using PaymentModel = ELearning.Models.Purchases.Payment;

namespace ELearningPortal.Services.User
{
    public class PaymentService : IPaymentService
    {
        private readonly AppDbContext db;
        private readonly RazorpaySettings settings;

        public PaymentService(
            AppDbContext db,
            IOptions<RazorpaySettings> options)
        {
            this.db = db;
            settings = options.Value;
        }

        public async Task<Order> CreateOrderAsync(int courseId, int userId)
        {
            var course = await db.Courses
                .Include(c => c.SubCourses)
                .FirstOrDefaultAsync(c => c.CourseId == courseId);

            if (course == null)
                throw new Exception("Course not found.");

            decimal amount = course.SubCourses.Min(x => x.Price);

            RazorpayClient client = new RazorpayClient(
                settings.KeyId,
                settings.KeySecret);

            Dictionary<string, object> options = new();

            options.Add("amount", amount * 100);

            options.Add("currency", "INR");

            options.Add("receipt", Guid.NewGuid().ToString());

            options.Add("payment_capture", 1);

            return client.Order.Create(options);
        }


        public async Task<bool> SavePaymentAsync(
    int userId,
    int courseId,
    string paymentId,
    string paymentMethod)
        {
            using var transaction =
                await db.Database.BeginTransactionAsync();

            try
            {

                // Check if course is already purchased
                bool alreadyPurchased = await db.UserCourses.AnyAsync(x =>
                    x.UserId == userId &&
                    x.CourseId == courseId &&
                    x.Status == PurchaseStatus.Active);

                if (alreadyPurchased)
                {
                    return false;
                }




                var course = await db.Courses
    .Include(x => x.SubCourses)
    .FirstOrDefaultAsync(x => x.CourseId == courseId);

                if (course == null)
                    return false;

                decimal amount = course.SubCourses.Min(x => x.Price);







                PaymentModel payment = new PaymentModel();

                payment.UserId = userId;

                payment.TransactionId = paymentId;

                payment.InvoiceNumber =
                    "INV" + DateTime.Now.ToString("yyyyMMddHHmmss");

                payment.Amount = amount;

                payment.PaymentMethod = paymentMethod;

                payment.PaymentStatus = PaymentStatus.Success;

                payment.PaymentDate = DateTime.Now;

                db.Payments.Add(payment);

                await db.SaveChangesAsync();

                UserCourse userCourse = new UserCourse();

                userCourse.UserId = userId;

                userCourse.CourseId = courseId;

                userCourse.PaymentId = payment.PaymentId;

                userCourse.PurchaseDate = DateTime.Now;

                userCourse.ExpiryDate =
                    DateTime.Now.AddYears(1);

                userCourse.Status = PurchaseStatus.Active;

                db.UserCourses.Add(userCourse);

                await db.SaveChangesAsync();

                var subCourses =
                    await db.SubCourses
                        .Where(x => x.CourseId == courseId)
                        .ToListAsync();

                foreach (var item in subCourses)
                {
                    UserSubCourse usc = new UserSubCourse();

                    usc.UserId = userId;

                    usc.SubCourseId = item.SubCourseId;

                    usc.PaymentId = payment.PaymentId;

                    usc.PurchaseDate = DateTime.Now;

                    // Added
                    usc.ExpiryDate = DateTime.Now.AddYears(1);

                    usc.Status = PurchaseStatus.Active;

                    db.UserSubCourses.Add(usc);
                }

                await db.SaveChangesAsync();

                await transaction.CommitAsync();

                return true;
            }
            catch
            {
                await transaction.RollbackAsync();

                return false;
            }
        }
    }

}