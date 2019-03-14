using System;

namespace set
{
	internal class Program
	{
		public static void Main(string[] args)
		{
			Console.WriteLine(
				new Calculation(3).CountSolutions()
			);
		}
	}
}