using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TestNinja.Mocking.HousekeeperHelper;

namespace TestNinja.UnitTests.MockingTests
{
    [TestFixture]
    public class HousekeeperHelperTests
    {
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<IStatementGenerator> _statementGenerator;
        private Mock<IEmailSender> _emailSender;
        private Mock<IXtraMessageBox> _messageBox;
        private HousekeeperHelper _housekeeperHelper;
        // As DateTime objects are already immutable, so no need to put this in SetUp method
        private DateTime _statementDate = new DateTime(2020, 1, 1);
        private Housekeeper _housekeeper;
        private readonly string _statementFileName = "fileName";

        [SetUp]
        public void SetUp()
        {
            _housekeeper = new Housekeeper { Email = "a", FullName = "b", Oid = 1, StatementEmailBody = "c" };
            _unitOfWork = new Mock<IUnitOfWork>();
            _statementGenerator = new Mock<IStatementGenerator>();
            _emailSender = new Mock<IEmailSender>();
            _messageBox = new Mock<IXtraMessageBox>();

            _unitOfWork.Setup(uow => uow.Query<Housekeeper>()).Returns(new List<Housekeeper>
            {
                _housekeeper
            }.AsQueryable());

            _housekeeperHelper = new HousekeeperHelper(
                _unitOfWork.Object,
                _statementGenerator.Object,
                _emailSender.Object,
                _messageBox.Object);
        }

        [Test]
        public void SendStatementEmails_WhenCalled_GenerateStatements()
        {
            _housekeeperHelper.SendStatementEmails(_statementDate);

            _statementGenerator.Verify(sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void SendStatementEmails_HousekeepersEmailIsNullOrEmptyOrWhitespace_ShouldNotGenerateStatement(string email)
        {
            _housekeeper.Email = email;

            _housekeeperHelper.SendStatementEmails(_statementDate);

            _statementGenerator.Verify(sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate), Times.Never);
        }

        [Test]
        public void SendStatementEmails_WhenCalled_EmailTheStatement()
        {
            _statementGenerator.Setup(sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate))
                .Returns(_statementFileName);

            _housekeeperHelper.SendStatementEmails(_statementDate);

            _emailSender.Verify(es => es.EmailFile(
                _housekeeper.Email, 
                _housekeeper.StatementEmailBody, 
                _statementFileName,
                It.IsAny<string>()));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void SendStatementEmails_StatementFileNameIsNullOrEmptyOrWhitespace_ShouldNotEmailTheStatement(string statementFileName)
        {
            _statementGenerator.Setup(sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate))
                .Returns(statementFileName);

            _housekeeperHelper.SendStatementEmails(_statementDate);

            _emailSender.Verify(es => es.EmailFile(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()),
                Times.Never);
        }

        // Note: You can also call Setup method of _statementGenerator in SetUp method to refactor our tests
        [Test]
        public void SendStatementEmails_EmailSendingFails_DisplayAMessageBox()
        {
            _statementGenerator.Setup(sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate))
                .Returns(_statementFileName);

            _emailSender.Setup(es => es.EmailFile(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()
                )).Throws<Exception>();

            _housekeeperHelper.SendStatementEmails(_statementDate);

            _messageBox.Verify(mb => mb.Show(It.IsAny<string>(), It.IsAny<string>(), MessageBoxButtons.OK));
        }
    }
}
