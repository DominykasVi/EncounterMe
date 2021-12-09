using NUnit.Framework;
using Moq;
using EncounterMe;
using EncounterMe.Interfaces;
using EncounterMe.Functions;
using System;
using System.Collections.Generic;

namespace EncounterMeTests
{
    [TestFixture]
    public class Tests
    {
        private LogInManager _lm;
        private User _us;

        [SetUp]
        public void SetUp()
        {
            var mockDB = new Mock<IDatabaseManager>();
            _us = new User("Test", "test@testmail.com", "TestTest123");
            mockDB.Setup<List<User>>(x => x.readFromFile<User>()).Returns(new List<User> { _us });
            _lm = new(mockDB.Object);
        }

        [TestCase("testtest123")]
        [TestCase("TestTest1234")]
        [TestCase("TestTest12")]
        [TestCase("TestTest!234")]
        public void Test_IncorrectPassword(string password)
        {
            Assert.AreNotEqual(_us, _lm.CheckPassword("Test", password));
            
        }

        //so far usernames are case-sensitive
        [TestCase("test")]
        [TestCase("TEST")]
        [TestCase("tEst")]
        public void Test_CorrectPasswordIncorrectUsername (string name)
        {
            Assert.AreNotEqual(_us, _lm.CheckPassword(name, "TestTest123"));
        }

        [Test]
        public void Test_CorrectPassword ()
        {
            Assert.AreEqual(_us, _lm.CheckPassword("Test", "TestTest123"));
        }

        [Test]
        public void Test_CreateNewUserWithExistingUsername ()
        {
            Assert.Throws(Is.TypeOf<Exception>().And.Message.EqualTo("User already exists!"),
                delegate { _lm.CreateUser("Test", "test@tesssst.test", "testTestas1234545"); });
        }

        [TestCase("testmail.com")]
        [TestCase("test@mail")]
        [TestCase("test@mail.")]
        [TestCase("@mail.com")]
        [TestCase("test.mail@com")]
        [TestCase("testmail.com@")]
        public void Test_CreateNewUserWithIncorrectEmail (string email)
        {
            Assert.Throws(Is.TypeOf<Exception>().And.Message.EqualTo("Invalid email."),
                delegate { _lm.CreateUser("testerrr", email, "testTestas1234545"); });
        }
        [TestCase("testtesttest")]
        [TestCase("Test123")]
        [TestCase("TESTTEST123")]
        [TestCase("testtest123")]
        [TestCase("TestTest")]
        [TestCase("123123123")]
        [TestCase("TestTest!")]
        [TestCase("TEST123!@#")]
        [TestCase("test123!@#")]
        public void Test_CreateNewUserWithIncorrectPassword (string password)
        {
            Assert.Throws(Is.TypeOf<Exception>().And.Message.EqualTo("Password should be at least 8 characters long and contain at least one number, one non-capital and one capital letter."),
                delegate { _lm.CreateUser("testerrr", "test@mail.com", password); });
        }
        [Test]
        public void Test_CreateUserCorrectly ()
        {
            Assert.DoesNotThrow(delegate { _lm.CreateUser("testerrr", "test@mail.com", "TestTest1234"); });
            Assert.That(_lm.CreateUser("testerrr", "test@mail.com", "TestTest1234") is User);
        }
    }
}