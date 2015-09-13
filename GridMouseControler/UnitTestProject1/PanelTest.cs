using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GridMouseControler;
using System.Drawing;
using System.Windows.Forms;

namespace UnitTestProject1
{
    /// <summary>
    /// UnitTest1 の概要の説明
    /// </summary>
    [TestClass]
    public class PanelTest
    {
        public PanelTest()
        {
        }

        private TestContext testContextInstance;

        /// <summary>
        ///現在のテストの実行についての情報および機能を
        ///提供するテスト コンテキストを取得または設定します。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 追加のテスト属性
        //
        // テストを作成する際には、次の追加属性を使用できます:
        //
        // クラス内で最初のテストを実行する前に、ClassInitialize を使用してコードを実行してください
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // クラス内のテストをすべて実行したら、ClassCleanup を使用してコードを実行してください
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // 各テストを実行する前に、TestInitialize を使用してコードを実行してください
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // 各テストを実行した後に、TestCleanup を使用してコードを実行してください
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestFullMap()
        {
            var panel = new RootPanel(new Rectangle(0, 0, 100, 200));
            Assert.IsNull(panel.Parent);
            Assert.IsFalse(panel.HasParent);
            Assert.AreEqual(new Rectangle(0, 0, 100, 200), panel.Bounds);
            Assert.AreEqual(new Point(50, 100), panel.CursorPoint);
            Assert.IsNull(panel.DicChildren);
            using (var form = new Form())
            {
                panel.CreateChildren(form.CreateGraphics(), new FullMap());
                Assert.AreEqual(25, panel.DicChildren.Count);
                Assert.AreEqual(new Point(0, 0), panel.DicChildren["q"].MapPosition);
                Assert.AreEqual(new Point(4, 4), panel.DicChildren[";"].MapPosition);
            }
        }

        [TestMethod]
        public void TestHandyMap()
        {
            var panel = new RootPanel(new Rectangle(0, 0, 100, 200));
            using (var form = new Form())
            {
                panel.CreateChildren(form.CreateGraphics(), new HandyMap());
                Assert.AreEqual(9, panel.DicChildren.Count);
                Assert.AreEqual(new Point(0, 0), panel.DicChildren["w"].MapPosition);
                Assert.AreEqual(new Point(1, 1), panel.DicChildren["d"].MapPosition);
                Assert.AreEqual(new Point(2, 2), panel.DicChildren["v"].MapPosition);
            }
        }

        [TestMethod]
        public void TestGridPanel()
        {
            using (var form = new Form())
            {
                var g = form.CreateGraphics();
                var panel = new GridPanel(new RootPanel(new Rectangle(0, 0, 1000, 1600)), g, new Point(5, 4), 3, 2, "x");
                Assert.IsNotNull(panel.Parent);
                Assert.IsTrue(panel.HasParent);
                Assert.AreEqual(new Rectangle(600, 800, 200, 400), panel.Bounds);
                Assert.AreEqual(new Point(700, 1000), panel.CursorPoint);
                Assert.IsNull(panel.DicChildren);
                Assert.AreEqual(149, panel.FontSize);
                Assert.IsTrue(panel.IsOdd);
                panel.MapPosition.X = 0;
                Assert.IsFalse(panel.IsOdd);
                panel.CreateChildren(g, new FullMap());
                var child = panel.DicChildren["f"];
                Assert.AreEqual(new Rectangle(720, 880, 40, 80), child.Bounds);
            }
        }
    }
}
