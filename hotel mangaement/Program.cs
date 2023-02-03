/* 
 * Project Name: Hotel Management Application System 
 * Author Name: Yijin Wu
 * Date:2022/10/5
 * Application Purpose:
 * 
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Assessment2Task2
{
    // Custom Class - Room
    public class Room
    {
        public int RoomNo { get; set; }
        public bool IsAllocated { get; set; }
    }
    // Custom Class - Customer
    public class Customer
    {
        public int CustomerNo { get; set; }
        public string CustomerName { get; set; }
    }
    // Custom Class - RoomAllocation
    public class RoomlAllocaltion
    {
        public int AllocatedRoomNo { get; set; }
        public Customer AllocatedCustomer { get; set; }
        public string DateStamp { get; set; }
    }

    // Custom Main Class - Program
    internal class Program
    {
        // Variables declaration and initialization
        public static Room[] listofRooms = new Room[100];
        public static int roomCount = 0;
        public static List<RoomlAllocaltion> listOfRoomlAllocaltions = new List<RoomlAllocaltion>();
        public static string filePath;


        //filePath = Path.Combine(folderPath, "HotelManagement.txt");

        // Main function
        static void Addrooms()
        {
            Room room = new Room();
            room.RoomNo = roomCount;
            room.IsAllocated = false;
            listofRooms[roomCount] = room;
            Console.WriteLine("Add Room:" + roomCount + " success!");
            roomCount++;
        }
        static void Displayrooms()
        {
            for (int i = 0; i < roomCount; i++)
            {
                Console.WriteLine("RoomNo:" + listofRooms[i].RoomNo + ", IsAllocated:" + listofRooms[i].IsAllocated);
            }

        }
        static void Allocaterooms()
        {
            try
            {
                Console.WriteLine("please input CustomerNo and CustomerName:");
                Console.Write("CustomerNo:");
                int CustomerNo = Convert.ToInt32(Console.ReadLine());
                Console.Write("CustomerName:");
                string CustomerName = Console.ReadLine();
                Console.Write("please input allocate RoomNo:");
                int RoomNo = Convert.ToInt32(Console.ReadLine());
                if (RoomNo >= roomCount)
                {
                    throw new InvalidOperationException();
                }
                if (listofRooms[RoomNo].IsAllocated)
                {
                    throw new InvalidTimeZoneException();
                }
                Customer customer = new Customer();
                customer.CustomerNo = CustomerNo;
                customer.CustomerName = CustomerName;
                RoomlAllocaltion allocaltion = new RoomlAllocaltion();
                // dateStamp
                allocaltion.DateStamp = DateTime.UtcNow.ToString();
                allocaltion.AllocatedRoomNo = RoomNo;
                allocaltion.AllocatedCustomer = customer;
                listOfRoomlAllocaltions.Add(allocaltion);
                listofRooms[RoomNo].IsAllocated = true;
                Console.WriteLine("Allocation Room success!");
            }
            catch (FormatException fe)
            {
                Console.Write("Unhandled Exception: System.FormatException: Input string was not in a correct format.");
            }
            catch (InvalidOperationException ie)
            {
                Console.Write("Unhandled Exception: System.InvalidOperationException: Sequence contains no matching element.");
            }
            finally
            {
            }

        }

        static void Deallocaterooms()
        {
            try
            {
                Console.Write("please input De-Allocate RoomNo:");
                int RoomNo = Convert.ToInt32(Console.ReadLine());
                bool isFind = false;
                foreach (RoomlAllocaltion allocaltion1 in listOfRoomlAllocaltions)
                {
                    if (allocaltion1.AllocatedRoomNo == RoomNo)
                    {
                        listOfRoomlAllocaltions.Remove(allocaltion1);
                        listofRooms[RoomNo].IsAllocated = false;
                        isFind = true;
                        break;
                    }
                    Console.Write("De-Allocate RoomNo success !");
                }
                if (!isFind)
                {
                    throw new InvalidOperationException();
                }
            }
            catch (FormatException fe)
            {
                Console.Write("Unhandled Exception: System.FormatException: Input string was not in a correct format.");
            }
            catch (InvalidOperationException ie)
            {
                Console.Write("Unhandled Exception: System.InvalidOperationException: Sequence contains no matching element.");
            }
            finally
            {

            }


        }

        static void Displayroomsallocationdetails()
        {
            foreach (RoomlAllocaltion allocaltion1 in listOfRoomlAllocaltions)
            {
                Console.WriteLine("RoomNo:" + allocaltion1.AllocatedRoomNo + ", CustomerNo:" + allocaltion1.AllocatedCustomer.CustomerNo
                + ", CustomerName:" + allocaltion1.AllocatedCustomer.CustomerName + " DateStamp:" + allocaltion1.DateStamp);
            }

        }


        static void SavetheRoomAllocationsToaFile()
        {
            try
            {
                DateTime dt = new DateTime();
                dt = System.DateTime.Now;
                string admin = dt.ToString("dd-MM-yyyy HH:mm:ss");
                StreamWriter yj = new StreamWriter(filePath);
                {
                    foreach (RoomlAllocaltion roomallocation in listOfRoomlAllocaltions)
                    {
                        Console.WriteLine(roomallocation.AllocatedRoomNo + "\t" + roomallocation.AllocatedCustomer.CustomerNo + "\t" + roomallocation.AllocatedCustomer.CustomerName + "\t" + admin);
                        yj.WriteLine(roomallocation.AllocatedRoomNo + "\t" + roomallocation.AllocatedCustomer.CustomerNo + "\t" + roomallocation.AllocatedCustomer.CustomerName + "\t" + admin);
                    }
                }



            }
            catch (Exception x)
            {
                Console.WriteLine(x.Message);
            }
        }
        static void ShowRoomAllocationsFromFile()
        {
            try
            {
                Console.WriteLine("The Show Room Allocation From the File have been called from Function Switch");
                FileStream f = new FileStream(path: filePath, FileMode.Open, FileAccess.Read);
                StreamReader streamReader = new StreamReader(f);
                string line = streamReader.ReadLine();
                while (line != null)
                {
                    Console.WriteLine(line);
                    line = streamReader.ReadLine();
                }
                streamReader.Close();
            }
            catch (Exception b)
            {
                Console.WriteLine(b);

            }
        }


        static void Main(string[] args)
        {
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            filePath = Path.Combine(folderPath, "HotelManagement.txt");
            string saveDataFolderPath = System.Environment.CurrentDirectory;
            string saveDataFilePath = Path.Combine(saveDataFolderPath, "lhms_studentid.txt");

            char ans;
            do
            {
                Console.Clear();
                Console.WriteLine("***********************************************************************************");
                Console.WriteLine("                 LANGHAM HOTEL MANAGEMENT SYSTEM                  ");
                Console.WriteLine("                            MENU                                 ");
                Console.WriteLine("***********************************************************************************");
                Console.WriteLine("1. Add Rooms");
                Console.WriteLine("2. Display Rooms");
                Console.WriteLine("3. Allocate Rooms");
                Console.WriteLine("4. De-Allocate Rooms");
                Console.WriteLine("5. Display Room Allocation Details");
                Console.WriteLine("6. Billing");
                Console.WriteLine("7. Save the Room Allocations To a File");
                Console.WriteLine("8. Show the Room Allocations From a File");
                Console.WriteLine("9. Exit");
                // Add new option 0 for Backup 
                Console.WriteLine("***********************************************************************************");
                Console.Write("Enter Your Choice Number Here:");
                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        // Option 1 - adding Rooms function
                        Console.Clear();
                        Addrooms();
                        break;
                    case 2:
                        // display Rooms function
                        Console.Clear();
                        Displayrooms();
                        break;
                    case 3:
                        // allocate Room To Customer function
                        Console.Clear();
                        Allocaterooms();
                        break;
                    case 4:
                        // De-Allocate Room From Customer function
                        Console.Clear();
                        Deallocaterooms();
                        break;
                    case 5:
                        // display Room Alocations function;
                        Console.Clear();
                        Displayroomsallocationdetails();
                        break;
                    case 6:
                        Console.Write("Billing Feature is Under Construction and will be added soon…!!!");
                        break;
                    case 7:
                        // SaveRoomAllocationsToFile
                        Console.Clear();
                        SavetheRoomAllocationsToaFile();
                        break;
                    case 8:
                        //Show Room Allocations From File
                        Console.Clear();
                        ShowRoomAllocationsFromFile();

                        break;
                    case 9:
                        // Exit Application
                        Environment.Exit(0);
                        break;
                    default:
                        break;
                }

                Console.Write("\nWould You Like To Continue(Y/N):");
                ans = Convert.ToChar(Console.ReadLine());
            } while (ans == 'y' || ans == 'Y');
        }
    }
}
