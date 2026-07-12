/* ==================================================================
   ELearning - Sample Seed Data
   ------------------------------------------------------------------
   Prerequisites (already inserted by you):
     Roles     : 1=SuperAdmin, 2=Admin, 3=User
     Branches  : 1=Mumbai, 2=Pune, 3=Delhi, 4=Bangalore, 5=Hyderabad
     Users     : 1=SuperAdmin, 2=Rahul(MumbaiAdmin), 3=Priya(PuneAdmin),
                 4=Shubham(User-Mumbai), 5=Rohit(User-Pune)

   Enum reference:
     Status         : 0=Inactive, 1=Active, 2=Deleted
     ApprovalStatus : 0=Pending, 1=Approved, 2=Rejected
     PurchaseStatus : 0=Pending, 1=Active, 2=Expired, 3=Cancelled, 4=Failed
     PaymentStatus  : 0=Pending, 1=Success, 2=Failed, 3=Refunded, 4=Cancelled
     Gender         : 1=Male, 2=Female, 3=Other
   ================================================================== */

USE ELearningDB;
GO

SET NOCOUNT ON;
GO

/* ==================================================================
   COURSES  (CreatedBy = 1 SuperAdmin)
   ================================================================== */
INSERT INTO Courses (CourseName, Description, Price, Image, DisplayOrder, ApprovalStatus, CreatedBy, Status, CreatedAt, ModifiedAt) VALUES
('Full Stack Web Development','Learn HTML, CSS, JS, React, Node.js, MongoDB end-to-end',4999.00,'fullstack.jpg',1,1,1,1,GETDATE(),GETDATE()),
('ASP.NET Core MVC Mastery','Build enterprise apps using .NET 8 and EF Core',5999.00,'aspnet.jpg',2,1,1,1,GETDATE(),GETDATE()),
('Data Science with Python','Numpy, Pandas, ML with Scikit-Learn',6499.00,'datascience.jpg',3,1,1,1,GETDATE(),GETDATE()),
('Mobile App Dev with Flutter','Build cross-platform mobile apps',4499.00,'flutter.jpg',4,1,1,1,GETDATE(),GETDATE()),
('DevOps Essentials','Docker, Kubernetes, CI/CD, Azure',5499.00,'devops.jpg',5,0,1,1,GETDATE(),GETDATE());
GO

/* Courses assumed: 1..5 */

/* ==================================================================
   SUBCOURSES  (Course 1..5, BranchId nullable)
   ================================================================== */
INSERT INTO SubCourses (CourseId, BranchId, SubCourseName, Description, Price, Image, Status, CreatedAt, ModifiedAt) VALUES
(1,1,'Frontend Fundamentals','HTML, CSS, JavaScript basics',1499.00,'frontend.jpg',1,GETDATE(),GETDATE()),
(1,1,'React Deep Dive','Hooks, Redux, Routing',1799.00,'react.jpg',1,GETDATE(),GETDATE()),
(1,2,'Node.js & Express','Backend APIs with Node',1699.00,'node.jpg',1,GETDATE(),GETDATE()),

(2,1,'C# Fundamentals','OOP, LINQ, Async',1599.00,'csharp.jpg',1,GETDATE(),GETDATE()),
(2,3,'ASP.NET Core MVC','Controllers, Views, Razor',1999.00,'mvc.jpg',1,GETDATE(),GETDATE()),
(2,3,'Entity Framework Core','Code First, Migrations',1899.00,'efcore.jpg',1,GETDATE(),GETDATE()),

(3,4,'Python for Data Science','Numpy, Pandas',1899.00,'python.jpg',1,GETDATE(),GETDATE()),
(3,4,'Machine Learning Basics','Scikit-Learn, Regression',2299.00,'ml.jpg',1,GETDATE(),GETDATE()),

(4,5,'Dart Language','Dart syntax, OOP',1299.00,'dart.jpg',1,GETDATE(),GETDATE()),
(4,5,'Flutter Widgets','UI building with Flutter',1799.00,'flutterui.jpg',1,GETDATE(),GETDATE()),

(5,NULL,'Docker Essentials','Containers & images',1499.00,'docker.jpg',1,GETDATE(),GETDATE()),
(5,NULL,'Kubernetes Intro','Pods, Services, Deployments',1899.00,'k8s.jpg',1,GETDATE(),GETDATE());
GO

/* SubCourses assumed: 1..12 */

/* ==================================================================
   LESSONS
   ================================================================== */
INSERT INTO Lessons (SubCourseId, LessonTitle, VideoURL, Duration, DisplayOrder, Status, CreatedAt, ModifiedAt) VALUES
-- SubCourse 1 (Frontend Fundamentals)
(1,'Introduction to HTML','https://cdn.masstech.com/videos/html-intro.mp4',15,1,1,GETDATE(),GETDATE()),
(1,'CSS Selectors & Box Model','https://cdn.masstech.com/videos/css-basics.mp4',20,2,1,GETDATE(),GETDATE()),
(1,'JavaScript Basics','https://cdn.masstech.com/videos/js-basics.mp4',25,3,1,GETDATE(),GETDATE()),

-- SubCourse 2 (React Deep Dive)
(2,'React Components','https://cdn.masstech.com/videos/react-components.mp4',22,1,1,GETDATE(),GETDATE()),
(2,'Hooks in Depth','https://cdn.masstech.com/videos/react-hooks.mp4',28,2,1,GETDATE(),GETDATE()),

-- SubCourse 3 (Node.js & Express)
(3,'Node.js Runtime','https://cdn.masstech.com/videos/node-runtime.mp4',18,1,1,GETDATE(),GETDATE()),
(3,'Express Routing','https://cdn.masstech.com/videos/express-routing.mp4',24,2,1,GETDATE(),GETDATE()),

-- SubCourse 4 (C# Fundamentals)
(4,'C# Basics','https://cdn.masstech.com/videos/csharp-basics.mp4',20,1,1,GETDATE(),GETDATE()),
(4,'OOP in C#','https://cdn.masstech.com/videos/csharp-oop.mp4',26,2,1,GETDATE(),GETDATE()),

-- SubCourse 5 (ASP.NET Core MVC)
(5,'MVC Pattern','https://cdn.masstech.com/videos/mvc-pattern.mp4',22,1,1,GETDATE(),GETDATE()),
(5,'Razor Views','https://cdn.masstech.com/videos/razor-views.mp4',20,2,1,GETDATE(),GETDATE()),

-- SubCourse 6 (EF Core)
(6,'Code First Approach','https://cdn.masstech.com/videos/efcore-codefirst.mp4',24,1,1,GETDATE(),GETDATE()),
(6,'Migrations Guide','https://cdn.masstech.com/videos/efcore-migrations.mp4',22,2,1,GETDATE(),GETDATE()),

-- SubCourse 7 (Python for DS)
(7,'Numpy Arrays','https://cdn.masstech.com/videos/numpy.mp4',25,1,1,GETDATE(),GETDATE()),
(7,'Pandas DataFrames','https://cdn.masstech.com/videos/pandas.mp4',28,2,1,GETDATE(),GETDATE()),

-- SubCourse 8 (ML Basics)
(8,'Linear Regression','https://cdn.masstech.com/videos/linear-reg.mp4',30,1,1,GETDATE(),GETDATE()),

-- SubCourse 9 (Dart)
(9,'Dart Syntax','https://cdn.masstech.com/videos/dart-syntax.mp4',18,1,1,GETDATE(),GETDATE()),

-- SubCourse 10 (Flutter Widgets)
(10,'Stateless Widgets','https://cdn.masstech.com/videos/flutter-stateless.mp4',22,1,1,GETDATE(),GETDATE()),

-- SubCourse 11 (Docker)
(11,'Docker Containers','https://cdn.masstech.com/videos/docker-containers.mp4',24,1,1,GETDATE(),GETDATE()),

-- SubCourse 12 (Kubernetes)
(12,'K8s Pods & Services','https://cdn.masstech.com/videos/k8s-pods.mp4',26,1,1,GETDATE(),GETDATE());
GO

/* Lessons assumed: 1..20 */

/* ==================================================================
   ATTACHMENTS
   ================================================================== */
INSERT INTO Attachments (LessonId, Title, FilePath, CreatedAt, ModifiedAt) VALUES
(1,'HTML Cheatsheet','/uploads/attachments/html-cheatsheet.pdf',GETDATE(),GETDATE()),
(2,'CSS Reference Guide','/uploads/attachments/css-guide.pdf',GETDATE(),GETDATE()),
(3,'JS Basics Notes','/uploads/attachments/js-notes.pdf',GETDATE(),GETDATE()),
(4,'React Components Slides','/uploads/attachments/react-components.pptx',GETDATE(),GETDATE()),
(8,'C# Basics eBook','/uploads/attachments/csharp-basics.pdf',GETDATE(),GETDATE()),
(10,'MVC Pattern Diagram','/uploads/attachments/mvc-diagram.png',GETDATE(),GETDATE()),
(12,'EF Core Code First Guide','/uploads/attachments/efcore-guide.pdf',GETDATE(),GETDATE()),
(14,'Numpy Practice Set','/uploads/attachments/numpy-practice.zip',GETDATE(),GETDATE()),
(18,'Flutter Widgets Poster','/uploads/attachments/flutter-widgets.png',GETDATE(),GETDATE());
GO

/* ==================================================================
   ASSIGNMENTS
   ================================================================== */
INSERT INTO Assignments (LessonId, Title, Description, FilePath, CreatedAt, ModifiedAt) VALUES
(1,'Build Static HTML Page','Create a personal profile page with HTML5','/uploads/assignments/html-assign.pdf',GETDATE(),GETDATE()),
(3,'JS Calculator','Build a simple calculator with vanilla JS','/uploads/assignments/js-calc.pdf',GETDATE(),GETDATE()),
(4,'React Todo App','Build a Todo app using React components','/uploads/assignments/react-todo.pdf',GETDATE(),GETDATE()),
(8,'C# Console Bank App','Simple banking console app in C#','/uploads/assignments/csharp-bank.pdf',GETDATE(),GETDATE()),
(10,'MVC Blog Project','Build a small blog using MVC','/uploads/assignments/mvc-blog.pdf',GETDATE(),GETDATE()),
(14,'Numpy Data Analysis','Analyse a dataset using Numpy','/uploads/assignments/numpy-task.pdf',GETDATE(),GETDATE());
GO

/* Assignments assumed: 1..6 */

/* ==================================================================
   QUESTIONS  (MCQ)
   ================================================================== */
INSERT INTO Questions (LessonId, QuestionText, CreatedAt, ModifiedAt) VALUES
(1,'Which tag is used to define a hyperlink in HTML?',GETDATE(),GETDATE()),
(1,'Which HTML tag is used for the largest heading?',GETDATE(),GETDATE()),
(3,'Which keyword is used to declare a constant in JavaScript?',GETDATE(),GETDATE()),
(3,'Which method converts JSON to a JavaScript object?',GETDATE(),GETDATE()),
(4,'What is the correct way to create a functional component in React?',GETDATE(),GETDATE()),
(8,'Which access modifier makes a member accessible within the same assembly?',GETDATE(),GETDATE()),
(10,'What does MVC stand for?',GETDATE(),GETDATE()),
(14,'Which Numpy function creates an array of zeros?',GETDATE(),GETDATE());
GO

/* Questions assumed: 1..8 */

/* ==================================================================
   QUESTION OPTIONS   (4 options per question)
   ================================================================== */
INSERT INTO QuestionOptions (QuestionId, OptionText, IsCorrect, Status) VALUES
-- Q1: <a> tag
(1,'<a>',1,1),(1,'<link>',0,1),(1,'<href>',0,1),(1,'<url>',0,1),
-- Q2: <h1>
(2,'<h6>',0,1),(2,'<h1>',1,1),(2,'<heading>',0,1),(2,'<head>',0,1),
-- Q3: const
(3,'var',0,1),(3,'let',0,1),(3,'const',1,1),(3,'define',0,1),
-- Q4: JSON.parse
(4,'JSON.stringify()',0,1),(4,'JSON.parse()',1,1),(4,'JSON.toObject()',0,1),(4,'parseJSON()',0,1),
-- Q5: function Component
(5,'class MyComp {}',0,1),(5,'function MyComp() { return <div/>; }',1,1),(5,'component MyComp()',0,1),(5,'render MyComp',0,1),
-- Q6: internal
(6,'private',0,1),(6,'public',0,1),(6,'internal',1,1),(6,'protected',0,1),
-- Q7: Model View Controller
(7,'Model View Controller',1,1),(7,'Multiple View Component',0,1),(7,'Managed Visual Class',0,1),(7,'Model View Class',0,1),
-- Q8: np.zeros
(8,'np.empty()',0,1),(8,'np.array()',0,1),(8,'np.zeros()',1,1),(8,'np.ones()',0,1);
GO

/* ==================================================================
   ASSIGNMENT SUBMISSIONS
     ReviewStatus (ApprovalStatus enum): 0=Pending, 1=Approved, 2=Rejected
   ================================================================== */
INSERT INTO AssignmentSubmissions (AssignmentId, UserId, UploadedFile, SubmittedDate, ReviewStatus, ReviewedBy, ReviewedDate, Remarks) VALUES
(1,4,'/uploads/submissions/shubham-html.zip',GETDATE(),1,2,GETDATE(),'Well structured page. Good work.'),
(2,4,'/uploads/submissions/shubham-js-calc.zip',GETDATE(),0,NULL,NULL,NULL),
(3,4,'/uploads/submissions/shubham-react-todo.zip',GETDATE(),2,2,GETDATE(),'Missing edit feature. Please resubmit.'),

(1,5,'/uploads/submissions/rohit-html.zip',GETDATE(),1,3,GETDATE(),'Nice work overall.'),
(4,5,'/uploads/submissions/rohit-csharp-bank.zip',GETDATE(),1,3,GETDATE(),'Good OOP design.'),
(5,5,'/uploads/submissions/rohit-mvc-blog.zip',GETDATE(),0,NULL,NULL,NULL);
GO

/* ==================================================================
   SUBSCRIPTIONS
   ================================================================== */
INSERT INTO Subscriptions (PlanName, Description, Price, ValidityDays, ApprovalStatus, Status, CreatedAt, ModifiedAt) VALUES
('Starter Monthly','Access to selected beginner courses for 30 days',999.00,30,1,1,GETDATE(),GETDATE()),
('Pro Quarterly','Access to all pro courses for 90 days',2499.00,90,1,1,GETDATE(),GETDATE()),
('Ultimate Annual','Full access to every course for 365 days',7999.00,365,1,1,GETDATE(),GETDATE());
GO

/* Subscriptions assumed: 1..3 */

/* ==================================================================
   SUBSCRIPTION - COURSE MAPPING
   ================================================================== */
INSERT INTO SubscriptionCourses (SubscriptionId, CourseId) VALUES
-- Starter: Full Stack, Flutter
(1,1),(1,4),

-- Pro: Full Stack, ASP.NET, Data Science
(2,1),(2,2),(2,3),

-- Ultimate: All courses
(3,1),(3,2),(3,3),(3,4),(3,5);
GO

/* ==================================================================
   PAYMENTS
   ================================================================== */
INSERT INTO Payments (UserId, TransactionId, GatewayName, InvoiceNumber, Amount, PaymentMethod, PaymentStatus, PaymentDate) VALUES
(4,'TXN20260101001','Razorpay','INV-2026-0001',4999.00,'UPI',1,GETDATE()),         -- Shubham buys Full Stack course
(4,'TXN20260101002','Razorpay','INV-2026-0002',1499.00,'Card',1,GETDATE()),        -- Shubham buys Frontend Fundamentals subcourse
(5,'TXN20260101003','Stripe','INV-2026-0003',5999.00,'Card',1,GETDATE()),          -- Rohit buys ASP.NET course
(5,'TXN20260101004','Razorpay','INV-2026-0004',2499.00,'UPI',1,GETDATE()),         -- Rohit buys Pro Quarterly subscription
(4,'TXN20260101005','Razorpay','INV-2026-0005',999.00,'UPI',1,GETDATE());          -- Shubham buys Starter Monthly subscription
GO

/* Payments assumed: 1..5 */

/* ==================================================================
   USER COURSES  (Course-level purchases)
   ================================================================== */
INSERT INTO UserCourses (UserId, CourseId, PaymentId, PurchaseDate, ExpiryDate, Status) VALUES
(4,1,1,GETDATE(),DATEADD(YEAR,1,GETDATE()),1),   -- Shubham -> Full Stack
(5,2,3,GETDATE(),DATEADD(YEAR,1,GETDATE()),1);   -- Rohit -> ASP.NET
GO

/* ==================================================================
   USER SUB-COURSES  (SubCourse-level purchases)
   ================================================================== */
INSERT INTO UserSubCourses (UserId, SubCourseId, PaymentId, PurchaseDate, ExpiryDate, Status) VALUES
(4,1,2,GETDATE(),DATEADD(MONTH,6,GETDATE()),1),  -- Shubham -> Frontend Fundamentals
(5,5,NULL,GETDATE(),DATEADD(MONTH,6,GETDATE()),1); -- Rohit -> ASP.NET Core MVC subcourse (via course access)
GO

/* ==================================================================
   USER SUBSCRIPTIONS
   ================================================================== */
INSERT INTO UserSubscriptions (UserId, SubscriptionId, PaymentId, PurchaseDate, ExpiryDate, Status) VALUES
(4,1,5,GETDATE(),DATEADD(DAY,30,GETDATE()),1),   -- Shubham -> Starter Monthly
(5,2,4,GETDATE(),DATEADD(DAY,90,GETDATE()),1);   -- Rohit -> Pro Quarterly
GO

/* ==================================================================
   LESSON PROGRESS
   ================================================================== */
INSERT INTO LessonProgresses (UserId, LessonId, VideoCompleted, MCQCompleted, AssignmentSubmitted, CompletedDate) VALUES
-- Shubham progress in Full Stack course
(4,1,1,1,1,GETDATE()),
(4,2,1,0,0,NULL),
(4,3,1,1,1,GETDATE()),
(4,4,0,0,0,NULL),

-- Rohit progress in ASP.NET course
(5,8,1,1,1,GETDATE()),
(5,9,1,0,0,NULL),
(5,10,1,1,1,GETDATE()),
(5,11,0,0,0,NULL);
GO

/* ==================================================================
   MCQ RESULTS
   ================================================================== */
INSERT INTO MCQResults (UserId, LessonId, Score, TotalMarks, IsPassed) VALUES
(4,1,2,2,1),   -- Shubham HTML lesson
(4,3,2,2,1),   -- Shubham JS lesson
(4,4,0,1,0),   -- Shubham React lesson (failed)

(5,8,1,1,1),   -- Rohit C# lesson
(5,10,1,1,1); -- Rohit MVC lesson
GO

/* ==================================================================
   RATINGS
   ================================================================== */
INSERT INTO Ratings (CourseId, UserId, Stars, Review, Status) VALUES
(1,4,5,'Excellent course, learned a lot!',1),
(1,5,4,'Very good but could have more real-world projects.',1),
(2,5,5,'Best ASP.NET course. Highly recommended.',1),
(3,4,4,'Great intro to data science.',1);
GO

/* ==================================================================
   CERTIFICATES
   ================================================================== */
INSERT INTO Certificates (UserId, CourseId, CertificateNo, GeneratedDate, PdfPath) VALUES
(4,1,'CERT-FS-2026-0001',GETDATE(),'/uploads/certificates/cert-fs-0001.pdf'),
(5,2,'CERT-NET-2026-0002',GETDATE(),'/uploads/certificates/cert-net-0002.pdf');
GO

/* ==================================================================
   VERIFY
   ================================================================== */
SELECT 'Courses'                AS TableName, COUNT(*) AS Rows FROM Courses UNION ALL
SELECT 'SubCourses',              COUNT(*) FROM SubCourses UNION ALL
SELECT 'Lessons',                 COUNT(*) FROM Lessons UNION ALL
SELECT 'Attachments',             COUNT(*) FROM Attachments UNION ALL
SELECT 'Assignments',             COUNT(*) FROM Assignments UNION ALL
SELECT 'AssignmentSubmissions',   COUNT(*) FROM AssignmentSubmissions UNION ALL
SELECT 'Questions',               COUNT(*) FROM Questions UNION ALL
SELECT 'QuestionOptions',         COUNT(*) FROM QuestionOptions UNION ALL
SELECT 'MCQResults',              COUNT(*) FROM MCQResults UNION ALL
SELECT 'Subscriptions',           COUNT(*) FROM Subscriptions UNION ALL
SELECT 'SubscriptionCourses',     COUNT(*) FROM SubscriptionCourses UNION ALL
SELECT 'Payments',                COUNT(*) FROM Payments UNION ALL
SELECT 'UserCourses',             COUNT(*) FROM UserCourses UNION ALL
SELECT 'UserSubCourses',          COUNT(*) FROM UserSubCourses UNION ALL
SELECT 'UserSubscriptions',       COUNT(*) FROM UserSubscriptions UNION ALL
SELECT 'LessonProgresses',        COUNT(*) FROM LessonProgresses UNION ALL
SELECT 'Ratings',                 COUNT(*) FROM Ratings UNION ALL
SELECT 'Certificates',            COUNT(*) FROM Certificates;
GO
