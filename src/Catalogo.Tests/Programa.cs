using System;

namespace Catalogo.Tests
{
    public class Programa
    {
        public static void Main(string[] args)
        {
            var tests = new CatalogoServiceTests();
            bool allTestsPassed = true;

            allTestsPassed &= RunTestAndPrintResult(() => tests.TestGetAllBooks(), "TestGetAllBooks");
            allTestsPassed &= RunTestAndPrintResult(() => tests.TestSearchBooks(), "TestSearchBooks");
            allTestsPassed &= RunTestAndPrintResult(() => tests.TestSortBooks(), "TestSortBooks");
            allTestsPassed &= RunTestAndPrintResult(() => tests.TestCalculateShippingCost(), "TestCalculateShippingCost");

            if (allTestsPassed)
            {
                Console.WriteLine("All tests passed successfully.");
            }
            else
            {
                Console.WriteLine("Some tests failed.");
            }
        }

        private static bool RunTestAndPrintResult(Action testAction, string testName)
        {
            try
            {
                testAction();
                Console.WriteLine($"{testName} passed successfully.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Test {testName} failed: {ex.Message}");
                return false;
            }
        }
    }
}