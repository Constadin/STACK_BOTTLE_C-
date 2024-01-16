using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace bottle_gAME_ST
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("\n\n\t\t|---------------------|\n");
            Console.Write("\t\t   Color Bottle Game\n");
            Console.Write("\t\t|---------------------|\n\n\n");

            for (int i = 0; i < 3; i++)
            {
                Console.Write(" |__|\t{0}\t", "green");
                Console.Write(" |__|\t{0}\t", "red");
                Console.Write(" |__|\t{0}\t", "yellow");
                Console.Write(" |__|\t{0}\t", "empty");
                Console.Write(" |__|\t{0}\t", "empty");
                Console.WriteLine();
            }
            Console.WriteLine("\n bot 0\t\t bot 1\t\t bot 2\t\t bot 3\t\t bot 4");

            // Wait for user input
            Console.WriteLine("\n\nPress to play the game.");
            Console.ReadKey();
            Console.Clear();


            //create a list of stacks
            List<Stack<Colors>> bigList = new List<Stack<Colors>>();

            // Create 5 stacks Bottles and add them to bigList
            for (int i = 0; i < 5; i++)
            {
                Stack<Colors> Bottles = new Stack<Colors>();
                bigList.Add(Bottles);
            }

            FillStacksWithColors(bigList);

            while (true)
            {
                Console.Write("\n\n\t\t|---------------------|\n");
                Console.Write("\t\t   Color Bottle Game\n");
                Console.Write("\t\t|---------------------|\n\n\n");
                // Display 
                PrintMove(bigList);
                // Prompt the user for source and destination stack indices
                Console.WriteLine("\nEnter the source stack index (0-4):");
                int sourceIndex = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter the destination stack index (0-4):");
                int destinationIndex = int.Parse(Console.ReadLine());

                // Move the first element from the source stack to the destination stack
                if (MoveElement(bigList, sourceIndex, destinationIndex))
                {

                    Console.WriteLine("Move successful! \n");

                }
                else
                {
                    Console.WriteLine("\nMove failed. The destination stack is full or the source stack is empty.\n");
                }

                // Display 
                PrintMove(bigList);
                // Wait for user input
                Console.WriteLine("\n\nPress any key to continue.");
                Console.ReadKey();
                Console.Clear();
            }

        }
        static void PrintMove(List<Stack<Colors>> bigList)
        {
            //μέθοδο LINQ για να βρει τη στοίβα με τον μεγαλύτερο αριθμό στοιχείων.
            //Η έκφραση λάμδα stack => stack.Count μεταβιβάζεται για Max size stoibas
            //να ορίσουμε ότι θέλουμε να συgγκρίνουμε τις μετρήσεις κάθε στοίβας. 

            int maxStackSize = bigList.Max(stack => stack.Count);

            for (int i = maxStackSize - 1; i >= 0; i--)
            {
                foreach (var stack in bigList)
                {
                    if (stack.Count > i)
                    {
                        Colors color = stack.Reverse().ElementAt(i);
                        Console.Write(" |__|\t{0}\t", color.GetColor());

                    }
                    else
                    {
                        Console.Write(" |__|\t\t");
                    }
                }
                Console.WriteLine();

            }

            Console.WriteLine("\n bot 0\t\t bot 1\t\t bot 2\t\t bot 3\t\t bot 4");


        }

        static void FillStacksWithColors(List<Stack<Colors>> bigList)
        {
            List<string> availableColors = new List<string>()
            {
              "red", "red", "red",
              "yellow", "yellow", "yellow",
              "green", "green", "green"
            };

            Random random = new Random();

            for (int i = 0; i < bigList.Count; i++)
            {
                Stack<Colors> stack = bigList[i];

                for (int j = 0; j < 3; j++)
                {
                    if (availableColors.Count == 0)
                    {
                        break; // No more colors available
                    }

                    int index = random.Next(availableColors.Count);
                    string color = availableColors[index];
                    Colors colorsObj = new Colors(color);
                    colorsObj.SetColor(color);

                    if (stack.Count(c => c.GetColor() == color) >= 2)
                    {
                        // Color already repeated more than 2 times in the stack
                        // Remove the color from availableColors to prevent further repetitions
                        availableColors.Remove(color);
                    }
                    else
                    {
                        // Color can be added to the stack
                        colorsObj.SetColor(color);
                        stack.Push(colorsObj);
                        availableColors.RemoveAt(index);
                    }
                }
            }
        }

        static bool MoveElement(List<Stack<Colors>> bigList, int sourceIndex, int destinationIndex)
        {
            if (sourceIndex >= 0 && sourceIndex < bigList.Count && destinationIndex >= 0 && destinationIndex < bigList.Count)
            {
                Stack<Colors> sourceStack = bigList[sourceIndex];
                Stack<Colors> destinationStack = bigList[destinationIndex];

                if (sourceStack.Count > 0 && destinationStack.Count < 3)
                {
                    Colors color = sourceStack.Pop();
                    destinationStack.Push(color);
                    Console.Write("\nColor : ");
                    color.PrintColor();
                    return true;
                }
            }

            return false;
        }        
    }
}



