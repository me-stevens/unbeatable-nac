using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NoughtsAndCrosses;

namespace Tests {

	[TestClass]
	public class TestPlayer {

		Player p = new Player();
		Random r = new Random();

		/**********************************************
		 * LASTPOS - Placed and FirstPos are updated?
		 *********************************************/
		[TestMethod]
		public void TestLastPos() {
			// Arrange
			int i     = r.Next(2);
			int j     = r.Next(2);
			int[] pos = new int[] { i, j };

			// Act
			p.LastPos = pos;

			// Assert placed
			int expected  = 1;
			Assert.AreEqual(expected, p.GetPlaced(), " \n--- Expected: " + expected + ". Actual p.GetPlaced(): " + p.GetPlaced());

			// Assert FirstPos
			Assert.AreEqual(p.LastPos, p.FirstPos, " \n--- Expected: " + p.LastPos[0] + "," + p.LastPos[1] + ". Actual p.FirstPos: " + p.FirstPos[0] + "," + p.FirstPos[1]);
		}

		/**********************************************
		 * COPY - Copies the contents of the player?
		 *********************************************/
		[TestMethod]
		public void TestCopy() {
			// Arrange
			p = new Player();
			p.Name    = "Jane";
			p.IsFirst = true;
			p.IsHuman = true;
			p.LastPos = new[] { 1, 4 };
			p.LastPos = new[] { 2, 3 };
			p.LastPos = new[] { 3, 2 };
			p.LastPos = new[] { 4, 1 };

			// Act
			Player copy = p.Copy();

			// Assert properties
			Assert.AreEqual(p.Name, copy.Name, " \n--- Expected: " + p.Name + ". Actual copy.Name: " + copy.Name);
			Assert.AreEqual(p.IsFirst, copy.IsFirst, " \n--- Expected: " + p.IsFirst + ". Actual copy.IsFirst: " + copy.IsFirst);
			Assert.AreEqual(p.IsHuman, copy.IsHuman, " \n--- Expected: " + p.IsHuman + ". Actual copy.IsHuman: " + copy.IsHuman);
			Assert.AreEqual(p.GetPlaced(), copy.GetPlaced(), " \n--- Expected: " + p.GetPlaced() + ". Actual copy.GetPlaced(): " + copy.GetPlaced());
			CollectionAssert.AreEqual(p.FirstPos, copy.FirstPos);
			CollectionAssert.AreEqual(p.LastPos, copy.LastPos);
		}
	}
}
