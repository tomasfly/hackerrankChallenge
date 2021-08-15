using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Solution
{
    public class NotesStore
    {
        public NotesStore() { }

        List<String> acceptedStates = new List<String> { "completed", "active", "others" };

        List<String> notes = new List<String> { };
        
        public void AddNote(String state, String name)
        {
            if (name.Length == 0)
            {
                throw new Exception("Name cannot be empty");
            }
            bool isValid = acceptedStates.Any(x => x.Equals(state));
            if (name.Length > 0 && !isValid)
            {
                throw new Exception($"Invalid state {state}");
            }
            notes.Add(state);
            notes.Add(name);
        }
        public List<String> GetNotes(String state)
        {
            List<String> returnNotes = new List<String> { };
            bool isValid = acceptedStates.Any(x => x.Equals(state));
            if (!isValid)
            {
                throw new Exception($"Invalid state {state}");
            }

            for (int i = 0; i < notes.Count; i++)
            {
                if (notes[i] == state)
                {
                    returnNotes.Add(notes[i + 1]);
                }
            }

            if (returnNotes.Count == 0)
            {
                return returnNotes;
            }
            return returnNotes;
        }
    }

    public class Solution
    {
        public static void Main()
        {
            var notesStoreObj = new NotesStore();
            var n = int.Parse(Console.ReadLine());
            for (var i = 0; i < n; i++)
            {
                var operationInfo = Console.ReadLine().Split(' ');
                try
                {
                    if (operationInfo[0] == "AddNote")
                        notesStoreObj.AddNote(operationInfo[1], operationInfo.Length == 2 ? "" : operationInfo[2]);
                    else if (operationInfo[0] == "GetNotes")
                    {
                        var result = notesStoreObj.GetNotes(operationInfo[1]);
                        if (result.Count == 0)
                            Console.WriteLine("No Notes");
                        else
                            Console.WriteLine(string.Join(",", result));
                    }
                    else
                    {
                        Console.WriteLine("Invalid Parameter");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                }
            }
        }
    }
}