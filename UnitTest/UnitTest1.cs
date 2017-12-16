using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Main;
using System.Linq;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        MainWindowViewModel mainWindowViewModel = new MainWindowViewModel();

        [TestMethod]
        public void CreateDataTest()
        {
            
            Assert.AreEqual(mainWindowViewModel.AllBeforeNumber.Count,35);
            Assert.AreEqual(mainWindowViewModel.AllAfterNumber.Count, 12);
        }

        [TestMethod]
        public void UpsetDate()
        {
            for (int i = 0; i < 50; i++)
            {
                mainWindowViewModel.UpsetBeforeNumber();
                Console.WriteLine(string.Join(",",mainWindowViewModel.AllBeforeNumber.ToArray()));
                Assert.IsTrue(mainWindowViewModel.AllBeforeNumber.Distinct().Count() == 35);
            }
        }
    }
}
