using System.Text.RegularExpressions;
using ConsoleApp1.Models;

internal class Program
{
    private static readonly Gr692BvvContext db = new Gr692BvvContext(); //db

    //for check validation
    private static readonly Regex regexEmail = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
    private static readonly Regex regexPhone = new Regex("^\\+?[1-9][0-9]{7,14}$");

    //func check email
    private static bool EmailValid(string email)
    {
        return regexEmail.Match(email).Success;
    }

    //func check phone
    private static bool PhoneValid(string phone)
    {
        return regexPhone.Match(phone).Success;
    }

    //func for return user
    private static void ViewUser(string login, string pass)
    {
        //if user exist
        if (db.Users.Select(u => u.Login).Contains(login))
        {
            //check pass
            if (db.Users.FirstOrDefault(u => u.Login == login).Password == pass)
            {
                var user = db.Users.FirstOrDefault(u => u.Login == login);
                //view info
                Console.WriteLine(
                    $"fio: {user?.Fio}, birth: {user?.Birth}, address: {user?.Address}, email: {user?.Email}, phone: {user?.Phone}");
            }
            else
                Console.WriteLine("pass failed");
        }
        else
            Console.WriteLine("login not exist");
    }

    //func for reg user
    private static void Register(string fio, string birth, string address, string email, string phone, string login,
        string pass)
    {
        //if user not exist
        if (!db.Users.Select(u => u.Login).Contains(login))
        {
            //if birth valid
            if (DateTime.TryParse(birth, out _))
            {
                //if email and phone valid
                if (EmailValid(email) && PhoneValid(phone))
                {
                    db.Users.Add(new User
                    {
                        Login = login,
                        Password = pass,
                        Fio = fio,
                        Birth = birth,
                        Address = address,
                        Email = email,
                        Phone = phone
                    }); //add user

                    db.SaveChanges();
                }
                else
                    Console.WriteLine("email of phone not valid");
            }
            else
                Console.WriteLine("dateTime not valid");
        }
        else
            Console.WriteLine("login exist");
    }

    //func for edit info
    private static void EditInfo(string login, string pass, User newInfo)
    {
        //check exist user
        if (db.Users.Select(u => u.Login).Contains(login))
        {
            //check pass
            if (db.Users.FirstOrDefault(u => u.Login == login).Login == pass)
            {
                //if birth valid
                if (DateTime.TryParse(newInfo.Birth, out _))
                {
                    //if email and phone valid
                    if (EmailValid(newInfo.Email) && PhoneValid(newInfo.Phone))
                    {
                        //edit info
                        db.Users.FirstOrDefault(u => u.Login == login).Password = newInfo.Password;
                        db.Users.FirstOrDefault(u => u.Login == login).Fio = newInfo.Fio;
                        db.Users.FirstOrDefault(u => u.Login == login).Birth = newInfo.Birth;
                        db.Users.FirstOrDefault(u => u.Login == login).Address = newInfo.Address;
                        db.Users.FirstOrDefault(u => u.Login == login).Email = newInfo.Email;
                        db.Users.FirstOrDefault(u => u.Login == login).Phone = newInfo.Phone;

                        db.SaveChanges();
                    }
                    else
                        Console.WriteLine("email of phone not valid");
                }
                else
                    Console.WriteLine("dateTime not valid");
            }
            else
                Console.WriteLine("pass failed");
        }
        else
            Console.WriteLine("user not exist");
    }

    //func for search user
    private static List<User> SearchUser(string fio)
    {
        //search fio
        if (db.Users.Select(u => u.Fio).Contains(fio))
        {
            return db.Users.Where(u => u.Fio == fio).ToList();
        }

        Console.WriteLine("fio not exist");
        return null;
    }

    //func for search user version 2
    private static List<User> SearchUser2(string fio)
    {
        //search fio
        if (true)
        {
            return db.Users.Where(u => u.Fio == fio).ToList();
            //later
        }
        else
            Console.WriteLine("fio not exist");
    }

    //func for remove user
    private static void RemoveUser(string login, string pass)
    {
        //check exist user
        if (db.Users.Select(u => u.Login).Contains(login))
        {
            //check pass
            if (db.Users.FirstOrDefault(u => u.Login == login).Login == pass)
            {
                db.Users.Remove(db.Users.FirstOrDefault(u => u.Login == login));
                db.SaveChanges();
            }
            else
                Console.WriteLine("pass failed");
        }
        else
            Console.WriteLine("user not exist");
    }

    //func for search young users
    private static List<User> SearchYoungUsers()
    {
        return db.Users
            .Where(u => Convert.ToDateTime(u.Birth).Year == db.Users.Min(o => Convert.ToDateTime(o.Birth).Year))
            .ToList();
    }

    private static void Main(string[] args)
    {
        Register("bvv", "26.07.2003", "tomsk", "bvv@gmail.com", "89539152059", "bvv", "123");
        
        ViewUser("bvv", "123");
        
        
    }
}