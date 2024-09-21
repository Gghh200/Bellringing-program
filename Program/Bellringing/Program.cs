using System.Collections.Concurrent;
using System.ComponentModel.Design;

public class Program{
    public static int[] ?Bells;
    public static int[] ?Rounds;
    public static string[] ?InputArray;
    public static Char[] ?FollowBell;
    public static string ?type;
    public static int ?Errors = 0;
    public static void Main(){
        Console.WriteLine("Welcome To The Bellringing software:");
        Start();
    }
    static void Start(){
        Console.WriteLine("Enter Number of Bells:");
        int BellNumber = Int32.Parse(Console.ReadLine()!);
        Bells = new int[BellNumber];
        Rounds = new int[BellNumber];
        for (int bell = 0; bell < Bells.Length; bell++){
            Bells[bell] = bell + 1;
            Rounds[bell] = bell + 1;
        }

        Console.WriteLine("Enter Number of Place Notation:");
        String PlaceNotation = Console.ReadLine()!;
        PlaceNotation = PlaceNotation.Replace("."," . ");
        PlaceNotation = PlaceNotation.Replace("-"," - ");
        PlaceNotation = PlaceNotation.Replace(","," , ");
        InputArray = PlaceNotation.Split(" ");

        Menu();
    }

    static void Exit(){
        Console.WriteLine("Would you like to clear the console? (Y/N)");
        string Answer2 = Console.ReadLine()!.ToUpper();
        if (Answer2 == "Y"){
            Console.Clear();
        }
    }
    static void Menu(){
        Console.WriteLine("What would you like to do?");
        Console.WriteLine(@"
        1. Write
        2. Practice 
        3. Restart 
        4. Exit @");
        int Answer = Int32.Parse(Console.ReadLine()!);
        if (Answer == 1){
            Write();
            Console.WriteLine("What would you like to go back to the menu? (Y/N)");
            string Answer2 = Console.ReadLine()!.ToUpper();
            if (Answer2 == "Y"){
                Menu();
            }else{
                Exit();
            }
        }else if(Answer == 2){
            Practice();
            Console.WriteLine("What would you like to go back to the menu? (Y/N)");
            string Answer2 = Console.ReadLine()!.ToUpper();
            if (Answer2 == "Y"){
                Menu();
            }else{
                Exit();
            }
        }else if(Answer == 3){
            Console.Clear();
            Start();
        }else{
            Exit();
        }
    }
    static void Practice(){
        type = "P";
        Console.WriteLine("Use A,S,D to change the place of the bell");
        Console.WriteLine("Enter Bell to Follow:");
        FollowBell = Console.ReadLine()!.ToCharArray();
        string temp = $"{string.Join("",Bells!)}";
        ChangeColour(temp, FollowBell[0]);
        int x = 0;
        while(!Enumerable.SequenceEqual(Bells!, Rounds!) || x != 1){
            for (int Input = 0; Input < InputArray!.Length; Input++){
                Bells = WhatToDo(InputArray[Input], Bells!, InputArray, Input);
            }
            Console.WriteLine("");
            x = 1;
        }
        Console.WriteLine("You had " + Errors + " number of errors");
        Errors = 0;
        type = "";
    }

    static void Write(){
        Console.WriteLine("Enter Bell to Follow:");
        FollowBell = Console.ReadLine()!.ToCharArray();
        string temp = $"{string.Join("",Bells!)}";
        ChangeColour(temp, FollowBell[0]);
        int x = 0;
        while(!Enumerable.SequenceEqual(Bells!, Rounds!) || x != 1){
            for (int Input = 0; Input < InputArray!.Length; Input++){
                Bells = WhatToDo(InputArray[Input], Bells!, InputArray, Input);
            }
            Console.WriteLine("");
            x = 1;
        }
    }
    static int[] WhatToDo(string value, int[] Bells, string[] InputArray, int position){
        if(value.Equals("-")){
            Bells = SwapAll(Bells);
        }
        else if(value.Equals(",")){
            for (int i = position - 2; i >= 0; i--){
                Bells = WhatToDo(InputArray[i], Bells, InputArray, i);
            }
        }else if(value.Equals("") || value.Equals(".")){
        }
        else{
            int[] InputBells = new int[value.Length];
            for(int Input = 0; Input < InputBells.Length; Input++){
                InputBells[Input] = Int32.Parse(value[Input].ToString());
            }
            Bells = Hold(Bells, InputBells);
        } 
        return Bells;
    }

    static int[] SwapAll(int[] OldBells){
        int[] OldBellsClone = (int[])OldBells.Clone();
        int[] NewBells = (int[])OldBells.Clone();
        for (int bell=0; bell < OldBells.Length - 1; bell++){
            OldBells[bell] = NewBells[bell + 1];
            OldBells[bell + 1] = NewBells[bell];
            bell++;
        } 
        string temp = $"{string.Join("",Bells!)}";
        if(type == "P"){
            PracticeInput(OldBellsClone, temp);
        }
        ChangeColour(temp, FollowBell![0]);
        return OldBells;
    }

    static int[] Hold(int[] OldBells, int[] HoldBells){
        int[] OldBellsClone = (int[])OldBells.Clone();
        int[] NewBells = (int[])OldBells.Clone();
        for (int bell = 0; bell < OldBells.Length - 1; bell++){
            if(HoldBells.Contains(bell + 1) || HoldBells.Contains(bell + 2)){
            }else{
                OldBells[bell] = NewBells[bell + 1];
                OldBells[bell + 1] = NewBells[bell];
                bell++;
            }
        } 
        string temp = $"{string.Join("",Bells!)}";
        if(type == "P"){
            PracticeInput(OldBellsClone, temp);
        }
        ChangeColour(temp, FollowBell![0]);
        return OldBells;
    }
    
    static void ChangeColour(string bells, char bell) {
        var IndexFollow = bells.IndexOf(bell);
        var IndexOne = bells.IndexOf('1');
        if(IndexFollow >= IndexOne){
            Console.Write(bells.Substring(0, IndexOne));
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(bells[IndexOne]);
            Console.ResetColor();
            if((IndexFollow == IndexOne + 4 && IndexOne == 1) || (IndexFollow == IndexOne + 3 && IndexOne == 1)){
                Console.Write(bells.Substring(IndexOne + 1, IndexFollow - 2));
            }else if(IndexOne + 1 == IndexFollow - 1){
                Console.Write(bells[IndexOne + 1]);
            }else if(IndexFollow == IndexOne + 3 && IndexOne == 2){
                Console.Write(bells[IndexOne + 1]+""+bells[IndexOne + 2]);
            }else if(IndexOne + 1 < IndexFollow - 1){
                Console.Write(bells.Substring(IndexOne + 1, IndexFollow - 1));
            }
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(bells[IndexFollow]);
            Console.ResetColor();
            if(IndexFollow != bells.Length){
                Console.WriteLine(bells.Substring(IndexFollow + 1));
            }
        }else{
            Console.Write(bells.Substring(0, IndexFollow));
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(bells[IndexFollow]);
            Console.ResetColor();
            if((IndexFollow == 1 && IndexOne == IndexFollow + 4) || (IndexOne == IndexFollow + 3 && IndexFollow == 1)){
                Console.Write(bells.Substring(IndexFollow + 1, IndexOne - 2));
            }else if(IndexFollow + 1 == IndexOne - 1){
                Console.Write(bells[IndexFollow + 1]);
            }else if(IndexFollow == 2 && IndexOne == IndexFollow + 3){
                Console.Write(bells[IndexFollow + 1]+""+bells[IndexFollow + 2]);
            }else if(IndexFollow + 1 < IndexOne - 1){
                Console.Write(bells.Substring(IndexFollow + 1, IndexOne - 1));
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(bells[IndexOne]);
            Console.ResetColor();
            if(IndexOne != bells.Length){
                Console.WriteLine(bells.Substring(IndexOne + 1));
            }
        }
    }

    static void PracticeInput(int[] BellsBefore, string BellsAfter){
        int Correct = 0;
        int FollowBellInt = FollowBell![0] - '0';
        int Before = Array.FindIndex(BellsBefore!, n=> n == FollowBellInt);
        int After = BellsAfter.IndexOf(FollowBell[0]);
        Console.WriteLine("Enter A,S,D");
        while(Correct == 0){
            string Input = Console.ReadLine()!.ToUpper();
            if(Input == "A" && Input == "S" && Input == "D"){
                Console.WriteLine("Enter Either A,S,D");
            }else if(!((Before + 1 == After && Input == "D") || (Before - 1 == After && Input == "A") || (Before == After && Input == "S"))){
                Console.WriteLine("Incorrect");
                Errors = Errors + 1;
            }else{
                Correct = 1;
            }
        }
    }
}