using System;
using System.Text;

namespace QUT
{
    public class Human
    {
        public string Name = "?", Uni_ID = "?", Position = "?",  Address = "?", 
        Telephone = "?", Course = "?";
        public List<string> Units = new List<string>();
    }


    class Program
    {
        static void Main(string [] args)
        {
            Welcome();
            Operation();
            End();
        }
        static void Welcome()
        {
            Console.WriteLine("--- Welcome to QUT Management System ---");
        }
        static void End()
        {
            Console.WriteLine("--- Thank you for using QUT Management System ---");
        }

        static void Keep()
        {
            Console.WriteLine(" ------- ");
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        private static void Operation()
        {
            List <Human> Teacher_List = new List<Human>();
            List <Human> Student_List = new List<Human>();
            while(true)
            {
                Console.WriteLine("Please choose your option : ");
                Console.WriteLine("1 ==> Add a new student");
                Console.WriteLine("2 ==> Add a new lecturer");
                Console.WriteLine("3 ==> List current students");
                Console.WriteLine("4 ==> List current teachers");
                Console.WriteLine("5 ==> Export CSV File");
                Console.WriteLine("6 ==> Exit");
                int option = int.Parse(Console.ReadLine());
                if(option == 1) Process(ref Student_List, 0);
                else if(option == 2) Process(ref Teacher_List, 1);
                else if(option == 3) Report(Student_List, 0);
                else if(option == 4) Report(Teacher_List, 1);
                else if(option == 5){
                    List<Human> newList = Teacher_List;
                    newList.AddRange(Student_List);
                    To_CSV(newList);
                }
                else if(option == 6) break;
                else
                {
                    Console.WriteLine("You have entered an invalid number, please try again! ");
                    break;
                }
                
            }
        }

        static void Process(ref List<Human> current_list, int option)
        {
            Human obj = new Human();
            string Input;
            if(option > 0) obj.Position = "Teacher";
            else obj.Position = "Student";

            Console.WriteLine("Please enter your name :");
            {
                Input = Console.ReadLine();
                if(Input != null) obj.Name = Input;
            }

            Console.WriteLine("Please enter your ID :");
            {
                Input = Console.ReadLine();
                if(Input != null) obj.Uni_ID = Input;
            }

            Console.WriteLine("Please enter your address :");
            {
                Input = Console.ReadLine();
                if(Input != null) obj.Address = Input;
            }

            Console.WriteLine("Please enter your telephone number :");
            {
                Input = Console.ReadLine();
                if(Input != null) obj.Telephone = Input;
            }

            Console.WriteLine("Please enter your teaching/learning course :");
            {
                Input = Console.ReadLine();
                if(Input != null) obj.Course = Input;
            }

            Console.WriteLine("Please enter the number of units you are going to enroll :");
            {
                bool can = false;
                int number = 0;
                while(!can)
                {
                    int n;
                    bool check = int.TryParse(Console.ReadLine(), out n);
                    if(check == true)
                    {
                        can = true;
                        number = n;
                    }
                    else
                    {
                        Console.WriteLine("Please try again");
                    }
                }
                
                for(int i = 0; i < number; i ++)
                {
                    Console.WriteLine("Please enter your teaching/learning unit number {0}:", i + 1);
                    Input = Console.ReadLine();
                    if(Input != null){
                        obj.Units.Add(Input);
                    }
                }  
            }

            current_list.Add(obj);
        }

        private static void Report(List<Human> current_list, int option)
        {
            string position = "?";
            if(option > 0) position = "teacher";
            else position = "student";
            int n = current_list.Count;
            if(n == 0) Console.WriteLine("There is no {0} at QUT", position);
            else if(n == 1){
                 Console.WriteLine("There is 1 {0} at QUT", position);
                 {
                    Console.WriteLine("Name : {0}", current_list[0].Name);
                    Console.WriteLine("ID : {0}", current_list[0].Uni_ID);
                    Console.WriteLine("Address : {0}", current_list[0].Address);
                    Console.WriteLine("Telephone : {0}", current_list[0].Telephone);
                    Console.WriteLine("Course enrolled : {0}", current_list[0].Course);
                    Console.WriteLine("Number of units enrolled : {0}", current_list[0].Units.Count);
                    for(int i = 0; i < current_list[0].Units.Count; i ++)
                    {
                        Console.WriteLine(" - {0}", current_list[0].Units[i]);
                    }
                 }
            }
            else
            {
                Console.WriteLine("There are {0} {1} at QUT", n, position);
                {
                    for(int i = 0; i < n; i ++)
                    {
                        Console.WriteLine("Name : {0}", current_list[i].Name);
                        Console.WriteLine("ID : {0}", current_list[i].Uni_ID);
                        Console.WriteLine("Address : {0}", current_list[i].Address);
                        Console.WriteLine("Telephone : {0}", current_list[i].Telephone);
                        Console.WriteLine("Course enrolled : {0}", current_list[i].Course);
                        Console.WriteLine("Number of units enrolled: {0}", current_list[i].Units.Count);
                        for(int j = 0; j < current_list[i].Units.Count; j ++)
                        {
                            Console.WriteLine(current_list[i].Units[j]);
                        }
                    }
                }
            }
            Keep();
        }
        
        static void To_CSV(List<Human> list)
        {
            // Create new CSV file
            string file = @"D:\Vscode\QUT\Output.csv";
            String separator = ",";

            // Create a formatted string, use separator variable to store the comma that separate each value for each column;
            StringBuilder output = new StringBuilder();

            // Create attributes
            String[] headings = { "Name", "Uni_ID", "Position", "Address", "Telephone", "Course", "Units"};
            output.AppendLine(string.Join(separator, headings));

            foreach( Human person in list)
            {
                string current_units = String.Join('-', person.Units);
                String[] newLine = { person.Name, person.Uni_ID, person.Position, person.Address, person.Telephone, person.Course, current_units};
                output.AppendLine(string.Join(separator, newLine));
            }
            // try-catch block to catch any potential problems that could happen when writing data to file -> the program will not crash
            // if the program is unable to save the file
            try
            {
                File.AppendAllText(file, output.ToString());
            }
            catch(Exception ex)
            {
                Console.WriteLine("Data could not be written to the CSV file.");
                return;
            }

            Console.WriteLine("The data has been successfully saved to the CSV file");
            Keep();
        }
    }
}
