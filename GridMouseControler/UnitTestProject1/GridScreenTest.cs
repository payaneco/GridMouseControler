using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GridMouseControler;
using System.Drawing;
using System.Windows.Forms;

namespace UnitTestProject1
{
    [TestClass]
    public class GridScreenTest
    {
        [TestMethod]
        public void TestInitialize()
        {
            var screen = new GridScreen(new Rectangle(1920, 1200, 1024, 768), new HandyMap());
            Assert.AreEqual(1920, screen.Left);
            Assert.AreEqual(1200, screen.Top);
            Assert.AreEqual(new Rectangle(0, 0, 1024, 768), screen.InnerRect);
            Assert.AreEqual(new Rectangle(0, 0, 1024, 768), screen.Root.Bounds);
        }

        [TestMethod]
        public void TestCreateChildren()
        {
            var bounds = new Rectangle(1920, 0, 1024, 768);
            var screen = new GridScreen(bounds, new HandyMap());
            using (var form = new Form())
            {
                form.Size = new Size(1024, 768);
                screen.CreateChildren(form.CreateGraphics());
                var dic = screen.Root.DicChildren;
                Assert.AreEqual(dic.Count, 9);
                Assert.AreEqual(new Rectangle(0, 0, 341, 256), dic["w"].Bounds);
                Assert.AreEqual(new Rectangle(682, 256, 341, 256), dic["f"].Bounds);
                Assert.AreEqual(new Rectangle(0, 512, 341, 256), dic["x"].Bounds);
            }
        }
    }
}
