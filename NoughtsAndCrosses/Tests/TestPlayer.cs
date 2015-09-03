using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NoughtsAndCrosses;

namespace Tests {

	[TestClass]
	public class TestPlayer {

		Player player = new Player();
		Random random = new Random();

		[TestMethod]
		public void TestCopy() {
			player = new Player();

			player.Name    = "Jane";
			player.IsHuman = true;
			player.IsFirst = true;

			Player copy = player.Copy();

			Assert.AreEqual(player.Name,    copy.Name,    " \n--- Expected: " + player.Name    + ". Actual copy.Name: "    + copy.Name);
			Assert.AreEqual(player.IsHuman, copy.IsHuman, " \n--- Expected: " + player.IsHuman + ". Actual copy.IsHuman: " + copy.IsHuman);
			Assert.AreEqual(player.IsFirst, copy.IsFirst, " \n--- Expected: " + player.IsFirst + ". Actual copy.IsFirst: " + copy.IsFirst);
		}
	}
}
