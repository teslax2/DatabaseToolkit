using System;
using System.Data.SqlClient;
using DatabaseToolkit.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DatabaseToolkitTest
{
    [TestClass]
    public class UnitTestDatabaseToolkit
    {
        [TestMethod]
        public void DataBaseActions_Seed_ShouldCreateDatabase()
        {
            var dbAction = new Database();
            var affectedRows = dbAction.Seed();
            Assert.IsTrue(affectedRows > 0);
        }

        [TestMethod]
        public void DataBaseActions_Seed_ArgumentsInConstructor_ShouldCreateDatabase()
        {
            var dbAction = new Database("localhost", "sa", "Strong@Password1", "AssecoDB");
            var affectedRows = dbAction.Seed();
            Assert.IsTrue(affectedRows > 0);
        }

        [TestMethod]
        public void DataBaseActions_Seed_ThrowException()
        {
            try
            {
                var dbAction = new Database("localhost", "sasdsd", "Strong@Password1", "AssecoDB");
                var affectedRows = dbAction.Seed();

                Assert.Fail();
            }
            catch (SqlException)
            {
                ;
            }
        }

        [TestMethod]
        public void DataBaseActions_Read_ShouldReturnList()
        {
            var dbAction = new Database();
            var items = dbAction.Read();
            Assert.IsTrue(items.Count > 0);
        }

        [TestMethod]
        public void DataBaseActions_Update_ShouldCopy()
        {
            var dbFrom = new Database();
            var items = dbFrom.Read();

            var dbTo = new Database("localhost", "sa", "Strong@Password1", "AssecoDB2");

            var affected = dbTo.Update(items);
            Assert.IsTrue(affected > 0);
        }
    }
}
