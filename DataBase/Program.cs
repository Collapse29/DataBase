using DataBase.Models;
using DataBase.ViewModels;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Xml.Linq;

namespace DataBase
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<User> users = new List<User>();
            List<Work> works = new List<Work>();
            List<UserViewModel> usersViewModel = new List<UserViewModel>();
            List<WorkViewModel> workViewModel = new List<WorkViewModel>();
            List<UsersWorks> usersWorks = new List<UsersWorks>();
            List<UsersWorksViewModel> usersWorksViewModels = new List<UsersWorksViewModel>();
            string str = "Data Source = DESKTOP-IUQD35B\\SQLEXPRESS01;Initial Catalog=Users; Integrated Security=SSPI";
            using (SqlConnection connection = new SqlConnection(str))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM dbo.Users", connection);
                //command.ExecuteNonQuery();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        User user = new User();
                        Int32.TryParse(reader[0].ToString(), out int userID);
                        Int32.TryParse(reader[2].ToString(), out int userAge);
                        Int32.TryParse(reader[3].ToString(), out int userGender);
                        user.ID = userID;
                        user.Name = reader[1].ToString();
                        user.Age = userAge;
                        user.Gender = userGender;
                        users.Add(user);
                    }
                }
                usersViewModel = users.Select(MapUserToUserViewModel).ToList();
                foreach (var i in usersViewModel)
                {
                    Console.WriteLine(i.ToString());
                }

                //foreach (var i in users)
                //{
                //    usersViewModel.Add(MapUserToUserViewModel(i));
                //    Console.WriteLine(usersViewModel.Last().ToString());
                //}

                command = new SqlCommand("SELECT * FROM dbo.Work", connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Work work = new Work();
                        Int32.TryParse(reader[0].ToString(),out int workID);
                        work.ID = workID;
                        work.WorkName = reader[1].ToString();
                        works.Add(work);
                    }
                }
                workViewModel = works.Select(MapWorkToWorkViewModel).ToList();
                foreach (var i in workViewModel)
                {
                    Console.WriteLine(i.ToString());
                }
                command = new SqlCommand("SELECT i.*, u.Name, u.Age, u.Gender, y.WorkName FROM\r\nUsersWorks as i Join Users as u on i.UserId=u.ID \r\njoin Work as y on i.WorkId=y.ID;", connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        
                        UsersWorksViewModel usersWorksViewModel = new UsersWorksViewModel();
                        usersWorksViewModel.ID = (int)reader[0];
                        usersWorksViewModel.UserID = (int)reader[1];
                        usersWorksViewModel.WorkID = (int)reader[2];
                        usersWorksViewModel.Name = reader[3].ToString();
                        usersWorksViewModel.Age = (int)reader[4];
                        usersWorksViewModel.Gender = GetGender((int)reader[5]);
                        usersWorksViewModel.NameWork = reader[6].ToString();
                        usersWorksViewModels.Add(usersWorksViewModel);
                    }
                }
                foreach (var i in usersWorksViewModels)
                {
                    Console.WriteLine(i.ToString());
                }
                //command = new SqlCommand("INSERT INTO Users (Name, Age, Gender) VALUES ('Аркадий', 20, 0);", connection);
                //command.ExecuteNonQuery();
                //command = new SqlCommand("UPDATE Users SET Age = 50 WHERE ID = 3;", connection);
                //command.ExecuteNonQuery();
                //command = new SqlCommand("DELETE FROM Users WHERE ID = 3", connection);
                //int resultDelete = command.ExecuteNonQuery();
                //ExecuteCommand("INSERT INTO Users(Name, Age, Gender) VALUES('Аркадий', 20, 0))", connection);
                //ExecuteCommand("UPDATE Users SET Age = 50 WHERE ID = 3;", connection);
                //ExecuteCommand("DELETE FROM Users WHERE ID = 3", connection);
            }
        }
        static int ExecuteCommand(string commands, SqlConnection conn)
        {
            SqlCommand command = new SqlCommand(commands, conn);
            int result = command.ExecuteNonQuery();
            return result;
            //или
            //return command.ExecuteNonQuery();
        }

        static UserViewModel MapUserToUserViewModel(User user)
        {
            UserViewModel userViewModel = new UserViewModel();
            userViewModel.ID = user.ID;
            userViewModel.Name = user.Name;
            userViewModel.Age = user.Age;
            userViewModel.Gender = GetGender(user.Gender);
            return userViewModel;
        }

        static WorkViewModel MapWorkToWorkViewModel(Work work)
        {
            WorkViewModel workViewModel = new WorkViewModel();
            workViewModel.ID = work.ID;
            workViewModel.WorkName = work.WorkName;
            return workViewModel;
        }

        static string GetGender(int gender)
        {
            if (Int32.TryParse(gender.ToString(), out int genderIndex))
            {
                if (genderIndex == 0)
                {
                    return "Мужчина";
                }
                else
                {
                    return  "Женщина";
                }
            }
            else
            {
                return "Неизвестный пол";
            }
            //тернарный оператор ?:
            //string gender = Int32.TryParse(reader[5].ToString(), out int genderIndex)  ?
            //    genderIndex == 0 ? 
            //        "Мужчина" 
            //        : "Женщина"
            //    : "Неизвестный пол";
        }
    }
}