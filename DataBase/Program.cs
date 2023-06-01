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
            List<UserViewModel> usersViewModel = new List<UserViewModel>();
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

                        string gender;
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
                        Console.WriteLine("ID:" + reader[0] + ",Наименование работы:" + reader[1]);
                    }
                }
                command = new SqlCommand("SELECT i.*, u.Name, u.Age, u.Gender, y.WorkName FROM\r\nUsersWorks as i Join Users as u on i.UserId=u.ID \r\njoin Work as y on i.WorkId=y.ID;", connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //string gender = Int32.TryParse(reader[5].ToString(), out int genderIndex)  ?
                        //    genderIndex == 0 ? 
                        //        "Мужчина" 
                        //        : "Женщина"
                        //    : "Неизвестный пол";
                        string gender;
                        if (Int32.TryParse(reader[5].ToString(), out int genderIndex))
                        {
                            if (genderIndex == 0)
                            {
                                gender = "Мужчина";
                            }
                            else
                            {
                                gender = "Женщина";
                            }
                        }
                        else
                        {
                            gender = "Неизвестный пол";
                        }
                        Console.WriteLine("ID:" + reader[0] + ",Юзер ID:" + reader[1] + ",Работа ID:" + reader[2] + ",Имя:" + reader[3] + ",Лет:" + reader[4] + ",Пол:" + gender + ",Название работы:" + reader[6]);
                    }
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
            if (Int32.TryParse(user.Gender.ToString(), out int genderIndex))
            {
                if (genderIndex == 0)
                {
                    userViewModel.Gender = "Мужчина";
                }
                else
                {
                    userViewModel.Gender = "Женщина";
                }
            }
            else
            {
                userViewModel.Gender = "Неизвестный пол";
            }
            return userViewModel;
        }
    }
}