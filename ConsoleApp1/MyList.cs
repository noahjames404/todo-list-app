﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleApp1
{
    class MyList
    {

        const string TASK_CREATE    = "1";
        const string TASK_VIEW_ALL  = "2";
        const string TASK_UPDATE    = "3";
        const string TASK_DELETE    = "4";

        const string LIST_SAVE_PATH = "./todo_list.txt";

        static List<string> todo_list = new List<string>();


        public static void Main(string[] args)
        {
            todo_list = getList();

            Console.WriteLine("==== My To do list program ===== ");

            while (true)
            {
                showMenu();
            }
      
        }

        public static string userInput(string question)
        {
            Console.Write(question + " : ");
            return Console.ReadLine();
        }

        public static void showMenu()
        {
            saveList(todo_list);

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("MAIN MENU");
            Console.WriteLine("1: Create Task");
            Console.WriteLine("2: View All Task");
            Console.WriteLine("3: Update Task");
            Console.WriteLine("4: Delete Task");
            Console.WriteLine();
            Console.WriteLine();

            string option = userInput("Select an option");
            
            switch (option)
            {
                case TASK_CREATE:
                    createTask();
                    break;
                case TASK_VIEW_ALL:
                    viewAllTask();
                    break;
                case TASK_UPDATE:
                    updateTask();
                    break;
                case TASK_DELETE:
                    deleteTask();
                    break;
                default:
                    Console.WriteLine("Invalid option selected");
                    showMenu();
                    break;
            }
            


        }


        public static void createTask()
        {

            string task = "";

            do
            {
                task = userInput("input task; type 'stop' to main menu");
                if (!task.Equals("stop"))
                {
                    todo_list.Add(task);
                }
                
            } while (!task.Equals("stop"));
        }

        public static void viewAllTask()
        {
            int counter = 1;
            Console.WriteLine("Showing all todo list");
            foreach (string task in todo_list)
            {
                Console.WriteLine(counter + ":" + task);
                counter++;
            }
        }

        public static void updateTask()
        {
            viewAllTask();
            int line_number = -1;
            string line_number_raw = userInput("input line number to update");
            
            try
            {
                line_number = Convert.ToInt32(line_number_raw);
                string new_task = userInput("update task name");

                todo_list[line_number - 1] = new_task;
            }
            catch(Exception ex)
            {
                Console.WriteLine("invalid line number, please try again");
            }

            showMenu();
        }

        public static void deleteTask()
        {
            viewAllTask();
            int line_number = -1;
            string line_number_raw = userInput("input line number to update");

            try
            {
                line_number = Convert.ToInt32(line_number_raw);
                todo_list.RemoveAt(line_number - 1);
            }
            catch (Exception ex)
            {
                Console.WriteLine("invalid line number, please try again");
            }

            showMenu();
        }



        public static void saveList(List<string> todo_list)
        {
            StreamWriter sw = File.CreateText(LIST_SAVE_PATH);
            foreach (string task in todo_list)
            {
                sw.WriteLine(task);
            }

            sw.Close();
        }

        public static List<string> getList()
        {
            if (!File.Exists(LIST_SAVE_PATH))
            {
                File.Create(LIST_SAVE_PATH).Close();
            }

            List<string> todo_list = new List<string>();
            StreamReader sr = File.OpenText(LIST_SAVE_PATH);
            string task;
            while ((task = sr.ReadLine()) != null)
            {
                todo_list.Add(task);
            }

            sr.Close();

            return todo_list;
        }


    }
}
