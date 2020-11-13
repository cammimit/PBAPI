using Microsoft.VisualStudio.TestTools.UnitTesting;
using PBAPI.Models;
using PBAPI.Controllers;
using PBAPI.Services;
using System.Threading.Tasks;
using System;



//as a general habit you would not put ALL your tests together like this
//you would split your integration and services and unit test probably - because you 
//may switch on and of the long winded integration tests whilst always
//running the unit tests

namespace PBAPITests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
        }

        [TestMethod]
        public void TestPBEntry()
        {//all we are doing here is testing the model
            //this is  abit of a stupid test but it only shows that
            //we woudl do unit tests for the model
            //arrange

            //act
            var pbe = new PBEntry();
            //assert
            Assert.IsNotNull(pbe);
        }

        [TestClass]
        public class ServicesTest1
        {
            [TestMethod]
            public void TestTheDatabaseSettings_ShouldCreateTheSettings()
            {
                //Arrange
                var settings = new PersonStoreDatabaseSettings();
                // var svc = new PersonService(settings);
                //act


                //Assert
                Assert.IsNotNull(settings);
            }
        }

        [TestMethod]
        public void GetPerson_ShouldReturnAllPerson()
        {
            //Arrange
            var settings = new PersonStoreDatabaseSettings();
            settings.DatabaseName = "PBA";
            settings.ConnectionString = "mongodb://localhost:27017";
            settings.PersonCollectionName = "PBENTITIES";
            var svc = new PersonService(settings);
            var controller = new PersonsController(svc);

            //...

            //Act
            var result = controller.Get();


            //Assert
             Assert.IsNotNull(result);
            //Assert(Assert.)

        }
    }

    [TestClass]
    public class GetSomePeopleControllerTest
    {
        //could drive from other data source if necessary
        //this was discussed
        [DataRow("5fadceffef41935c1cdecae0")]
        [DataRow("5fadceffef41935c1cdecae1")]
        [DataRow("5fadceffef41935c1cdecae2")]
        [DataTestMethod]
        public void GetPerson_ShouldReturnOnePerson(string fakePersonId)
        {
            //Arrange
            //var fakePersonId = "5fadceffef41935c1cdecae0";
            var settings = new PersonStoreDatabaseSettings();
            settings.DatabaseName = "PBA";
            settings.ConnectionString = "mongodb://localhost:27017";
            settings.PersonCollectionName = "PBENTITIES";
            var svc = new PersonService(settings);
            var controller = new PersonsController(svc);

            //...

            //Act
            var result = controller.Get(fakePersonId);


            //Assert
            Assert.IsNotNull(settings);
            Assert.IsNotNull(result);

        }

    }

    [TestClass]
    public class ContextTests
    {
        //fire up the context through statrup.cs

        //Arrange
    }

}
