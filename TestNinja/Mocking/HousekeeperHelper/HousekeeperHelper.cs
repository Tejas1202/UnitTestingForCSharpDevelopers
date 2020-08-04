using System;
using System.Collections.Generic;
using System.Linq;

namespace TestNinja.Mocking.HousekeeperHelper
{
    #region Legacy code having external resource UnitOfWork as well as SaveStatement and SendingEmail functionality as private methods
    //public static class HousekeeperHelper
    //{
    //    private static readonly UnitOfWork UnitOfWork = new UnitOfWork();

    //    public static bool SendStatementEmails(DateTime statementDate)
    //    {
    //        var housekeepers = UnitOfWork.Query<Housekeeper>();

    //        foreach (var housekeeper in housekeepers)
    //        {
    //            if (housekeeper.Email == null)
    //                continue;

    //            var statementFilename = SaveStatement(housekeeper.Oid, housekeeper.FullName, statementDate);

    //            if (string.IsNullOrWhiteSpace(statementFilename))
    //                continue;

    //            var emailAddress = housekeeper.Email;
    //            var emailBody = housekeeper.StatementEmailBody;

    //            try
    //            {
    //                EmailFile(emailAddress, emailBody, statementFilename,
    //                    string.Format("Sandpiper Statement {0:yyyy-MM} {1}", statementDate, housekeeper.FullName));
    //            }
    //            catch (Exception e)
    //            {
    //                XtraMessageBox.Show(e.Message, string.Format("Email failure: {0}", emailAddress),
    //                    MessageBoxButtons.OK);
    //            }
    //        }

    //        return true;
    //    }

    //    private static string SaveStatement(int housekeeperOid, string housekeeperName, DateTime statementDate)
    //    {
    //        var report = new HousekeeperStatementReport(housekeeperOid, statementDate);

    //        if (!report.HasData)
    //            return string.Empty;

    //        report.CreateDocument();

    //        var filename = Path.Combine(
    //            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
    //            string.Format("Sandpiper Statement {0:yyyy-MM} {1}.pdf", statementDate, housekeeperName));

    //        report.ExportToPdf(filename);

    //        return filename;
    //    }

    //    private static void EmailFile(string emailAddress, string emailBody, string filename, string subject)
    //    {
    //        var client = new SmtpClient(SystemSettingsHelper.EmailSmtpHost)
    //        {
    //            Port = SystemSettingsHelper.EmailPort,
    //            Credentials =
    //                new NetworkCredential(
    //                    SystemSettingsHelper.EmailUsername,
    //                    SystemSettingsHelper.EmailPassword)
    //        };

    //        var from = new MailAddress(SystemSettingsHelper.EmailFromEmail, SystemSettingsHelper.EmailFromName,
    //            Encoding.UTF8);
    //        var to = new MailAddress(emailAddress);

    //        var message = new MailMessage(from, to)
    //        {
    //            Subject = subject,
    //            SubjectEncoding = Encoding.UTF8,
    //            Body = emailBody,
    //            BodyEncoding = Encoding.UTF8
    //        };

    //        message.Attachments.Add(new Attachment(filename));
    //        client.Send(message);
    //        message.Dispose();

    //        File.Delete(filename);
    //    }
    //}
    #endregion

    #region Refactoring code, here we're changing class and method from static to instance assuming it won't break the flow anywhere
    // as we want to use ctor injection, if you can't change from static to instance, then just use Parameter injection in Method as we did with BookingHelper in this project
    public class HousekeeperHelper
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStatementGenerator _statementGenerator;
        private readonly IEmailSender _emailSender;
        private readonly IXtraMessageBox _messageBox;

        public HousekeeperHelper(
            IUnitOfWork unitOfWork,
            IStatementGenerator statementGenerator,
            IEmailSender emailSender,
            IXtraMessageBox messageBox)
        {
            _unitOfWork = unitOfWork;
            _statementGenerator = statementGenerator;
            _emailSender = emailSender;
            _messageBox = messageBox;
        }

        // This function is more of Command type than Query type, hence we've to do Interaction testing here more than state based testing
        public void SendStatementEmails(DateTime statementDate)
        {
            // Returns List of Housekeeper objects from database
            var housekeepers = _unitOfWork.Query<Housekeeper>();

            foreach (var housekeeper in housekeepers)
            {
                // Earlier it was housekeeper.Email == null which was breaking our test case. So we catched the bug and changed the implementation
                if (string.IsNullOrWhiteSpace(housekeeper.Email))
                    continue;

                var statementFilename = _statementGenerator.SaveStatement(housekeeper.Oid, housekeeper.FullName, statementDate);

                if (string.IsNullOrWhiteSpace(statementFilename))
                    continue;

                var emailAddress = housekeeper.Email;
                var emailBody = housekeeper.StatementEmailBody;

                try
                {
                    _emailSender.EmailFile(emailAddress, emailBody, statementFilename,
                        string.Format("Sandpiper Statement {0:yyyy-MM} {1}", statementDate, housekeeper.FullName));
                }
                catch (Exception e)
                {
                    _messageBox.Show(e.Message, string.Format("Email failure: {0}", emailAddress),
                        MessageBoxButtons.OK);
                }
            }
        }
    }

    // Not creating a repository class seperately here because we're just querying the database to get list of housekeepers directly
    public interface IUnitOfWork
    {
        IQueryable<T> Query<T>();
    }

    public interface IXtraMessageBox
    {
        void Show(string s, string housekeeperStatements, MessageBoxButtons ok);
    }
    #endregion

    public class UnitOfWork : IUnitOfWork
    {
        public IQueryable<T> Query<T>()
        {
            return new List<T>().AsQueryable();
        }
    }

    public class XtraMessageBox : IXtraMessageBox
    {
        public void Show(string s, string housekeeperStatements, MessageBoxButtons ok)
        {
        }
    }

    public enum MessageBoxButtons
    {
        OK
    }

    public class MainForm
    {
        public bool HousekeeperStatementsSending { get; set; }
    }

    public class DateForm
    {
        public DateForm(string statementDate, object endOfLastMonth)
        {
        }

        public DateTime Date { get; set; }

        public DialogResult ShowDialog()
        {
            return DialogResult.Abort;
        }
    }

    public enum DialogResult
    {
        Abort,
        OK
    }

    public class SystemSettingsHelper
    {
        public static string EmailSmtpHost { get; set; }
        public static int EmailPort { get; set; }
        public static string EmailUsername { get; set; }
        public static string EmailPassword { get; set; }
        public static string EmailFromEmail { get; set; }
        public static string EmailFromName { get; set; }
    }

    public class Housekeeper
    {
        public string Email { get; set; }
        public int Oid { get; set; }
        public string FullName { get; set; }
        public string StatementEmailBody { get; set; }
    }

    public class HousekeeperStatementReport
    {
        public HousekeeperStatementReport(int housekeeperOid, DateTime statementDate)
        {
        }

        public bool HasData { get; set; }

        public void CreateDocument()
        {
        }

        public void ExportToPdf(string filename)
        {
        }
    }
}