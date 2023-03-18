using System.Text.RegularExpressions;

Dictionary<string, List<string>> db = new Dictionary<string, List<string>>(); //db

Regex regexEmail = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
Regex regexPhone = new Regex("^\\+?[1-9][0-9]{7,14}$");

bool EmailValid(string email)
{
    var matchEmail = regexEmail.Match(email);
    return matchEmail.Success;
}

bool PhoneValid(string phone)
{
    var matchEmail = regexPhone.Match(phone);
    return matchEmail.Success;
}

void ViewUser(string login, string pass, Dictionary<string, List<string>> db)
{
    //if user exist
    if (db.Keys.Contains(login))
    {
        //check pass
        if (db[login].ToList()[0] == pass)
        {
            //view info
            foreach (var item in db[login].ToList())
                Console.WriteLine(item);
        }
        else
            Console.WriteLine("pass failed");
    }
    else
        Console.WriteLine("login not exist");
}

//reg user
void Register(string fio, string birth, string address, string email, string phone, string login, string pass,
    Dictionary<string, List<string>> db)
{
    //if user not exist
    if (!db.Keys.Contains(login))
    {
        DateTime newBirth;
        //if birth valid
        if (DateTime.TryParse(birth, out newBirth))
        {
            //if email and phone valid
            if (EmailValid(email) && PhoneValid(phone))
                db[login] = new List<string> { pass, fio, birth, address, email, phone }; //add user
            else
                Console.WriteLine("email of phone not valid");
        }
        else
            Console.WriteLine("dateTime not valid");
    }
    else
        Console.WriteLine("login exist");
}

//edit info
void EditInfo(Dictionary<string, List<string>> db, string login, string pass, List<string> newInfo)
{
    //check exist user
    if (db.Keys.Contains(login))
    {
        //check pass
        if (db[login].ToList()[0] == pass)
        {
            DateTime newBirth;
            //if birth valid
            if (DateTime.TryParse(newInfo[2], out newBirth))
            {
                //if email and phone valid
                if (EmailValid(newInfo[3]) && PhoneValid(newInfo[4]))
                    db[login] = newInfo; //edit info
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

//search user
void SearchUser(Dictionary<string, List<string>> db, string fio)
{
    //search fio
    if (db.Values.ToList()[1].Contains(fio))
    {
        var user = db.Values.FirstOrDefault(u => u.ToList()[1] == fio);
        foreach (var item in user)
            Console.WriteLine(item);
    }
    else
        Console.WriteLine("fio not exist");
}

//search user version 2
void SearchUser2(Dictionary<string, List<string>> db, string fio)
{
    //search fio
    if (db.Values.ToList()[1].Contains(fio))
    {
        //later
    }
    else
        Console.WriteLine("fio not exist");
}

//remove user
void RemoveUser(Dictionary<string, List<string>> db, string login, string pass)
{
    //check exist user
    if (db.Keys.Contains(login))
    {
        //check pass
        if (db[login].ToList()[0] == pass)
            db.Remove(login);
        else
            Console.WriteLine("pass failed");
    }
    else
        Console.WriteLine("user not exist");
}

//search young users
void SearchYoungUsers(Dictionary<string, List<string>> db)
{
    var user = db.Values.FirstOrDefault(u =>
        Convert.ToDateTime(u[2]).Year == db.Values.ToList()[2].Min(m => Convert.ToDateTime(m[2]).Year));
    foreach (var item in user)
    {
        Console.WriteLine(item);
    }
}

Dictionary<string, List<string>> db1 = new Dictionary<string, List<string>>
{
    { "bvv", new List<string> { "123", "bvv", "26.07.2003", "tomsk", "bvv@g,ail.com", "89539152059" } },
    { "kns", new List<string> { "321", "kns", "02.04.2002", "tomsk", "kns@g,ail.com", "89539152059" } }
};
SearchYoungUsers(db1);

//--TEST--
Console.WriteLine("Enter fio, birth, address, email, phone, login and pass");
//register new user
Register(Console.ReadLine(), Console.ReadLine(), Console.ReadLine(), Console.ReadLine(), Console.ReadLine(),
    Console.ReadLine(), Console.ReadLine(), db);

Console.WriteLine("Enter login and pass");
//view all info
ViewUser(Console.ReadLine(), Console.ReadLine(), db);

Console.WriteLine("Enter login, pass, new: pass, fio, birth, address, email, phone");
//edit info user
EditInfo(db, Console.ReadLine(), Console.ReadLine(),
    new List<string>
    {
        Console.ReadLine(), Console.ReadLine(), Console.ReadLine(),
        Console.ReadLine(), Console.ReadLine(), Console.ReadLine()
    });

Console.WriteLine("Enter login and pass");
//view all info
ViewUser(Console.ReadLine(), Console.ReadLine(), db);

Console.WriteLine("Enter fio");
//search user
SearchUser(db, Console.ReadLine());

Console.WriteLine("Enter login and pass");
//remove user
RemoveUser(db, Console.ReadLine(), Console.ReadLine());

Console.WriteLine("Enter login and pass");
//view all info
ViewUser(Console.ReadLine(), Console.ReadLine(), db);