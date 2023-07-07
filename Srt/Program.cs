// See https://aka.ms/new-console-template for more information
using Srt;
string fileName = @".\..\..\napisy do filmu.srt";
Console.Write("Podaj nazwę pliku [" +  fileName + "]: ");
string newName = Console.ReadLine();
if (newName.Length > 0)
  fileName = newName;
Processing.Loop(fileName);
Console.ReadLine();


