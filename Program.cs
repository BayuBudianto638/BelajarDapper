// Dapper adalah  Object Mapping. Base nya ADO .NET, yg dibungkus menjadi object.
// Result dr ADO .NET itu akan di mapping otomatis ke object class.
// sqlCommand(query, connection)
// cmd.read()
// 

using BelajarDapper;
using Dapper;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection;

class Students
{
    public int StudentId { get; set; }
    public string StudentCode { get; set; }
    public string StudentName { get; set; }
    public DateTime DoB { get; set; }
    public string Gender { get; set; }
    public DateTime LastUpdate { get; set; }
}

class StudentAddress
{
    public int StudentAddressId { get; set; }
    public string Address1 { get; set; }
    public string Address2 { get; set; }
    public int StudentId { get; set; }
    public string Email { get; set; }
    public string Mobile { get; set; }
}

class StudentAddrDto
{
    public string StudentCode { get; set; }
    public string StudentName { get; set; }
    public string Address1 { get; set; }
}

class StudentAddr2Dto
{
    public string StudentCode { get; set; }
    public string StudentName { get; set; }
    public string Address1 { get; set; }
}

public class Teacher
{
    public int TeacherId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

//class Program
//{
//    static void Main()
//    {
//        var conString = @"Server=DESKTOP-QEO3NAA\SQLEXPRESS;Database=SchoolDB;Trusted_Connection=True;";

//        using (var connection = new SqlConnection(conString))
//        {
//            var sql = "SELECT * FROM Students WHERE StudentId = @id";
//            var students = connection.Query<Students>(sql,
//                new {id = 1}).ToList();

//            foreach (Students student in students)
//            {
//                Console.WriteLine(student.StudentCode + " " + student.StudentName);
//            }
//        }
//    }
//}

//class Program
//{
//    static void Main()
//    {
//        var conString = @"Server=DESKTOP-QEO3NAA\SQLEXPRESS;Database=SchoolDB;Trusted_Connection=True;";

//        using (var connection = new SqlConnection(conString))
//        {
//            var sql = "SELECT * FROM Students WHERE StudentId = @id";
//            var student = connection.QuerySingle<Students>(sql,
//                new { id = 1 }); // Dapper digunakan untuk mengambil single row pd table

//            Console.WriteLine(student.StudentCode + " " + student.StudentName);
//        }
//    }
//}

//class Program
//{
//    static void Main()
//    {
//        var conString = @"Server=DESKTOP-QEO3NAA\SQLEXPRESS;Database=SchoolDB;Trusted_Connection=True;";

//        using (var connection = new SqlConnection(conString))
//        {
//            var sql = "SELECT * FROM Students";
//            var student = connection.QueryFirstOrDefault<Students>(sql);

//            // QuerySingle : Hanya mereturn row jika memang yg di return adalah 1 row
//            // QueryFirstOrDefault : Akan mereturn 1 row dari multiple row=>SELECT TOP 1 * FROM Table

//            Console.WriteLine(student.StudentCode + " " + student.StudentName);
//        }
//    }
//}

//class Program
//{
//    static void Main()
//    {
//        var conString = @"Server=DESKTOP-QEO3NAA\SQLEXPRESS;Database=SchoolDB;Trusted_Connection=True;";

//        using (var connection = new SqlConnection(conString))
//        {
//            var sql = "SELECT * FROM Students WHERE Id = @Id";
//            var student = connection.QuerySingleOrDefault<Students>(sql, new { Id = 1 });

//            // QuerySingle : Hanya mereturn row jika memang yg di return adalah 1 row
//            // QueryFirstOrDefault : Akan mereturn 1 row dari multiple row=>SELECT TOP 1 * FROM Table
//            //                     : akan return null jika table kosong/empty
//            // QueryFirst : Akan mereturn 1 row dari multiple row=>SELECT TOP 1 * FROM Table
//            //            : Akan return error no sequence elements jika tablenya kosong
//            // QuerySingle: Akan mereturn 1 row dari multiple row=>SELECT TOP 1 * FROM Table
//            //            : Akan return error no sequence elements jika tablenya kosong
//            // QuerySingleOrDefault : Akan mereturn 1 row dari multiple row=>SELECT TOP 1 * FROM Table
//            //                      : akan return null jika table kosong/empty
//            //                      : HARUS mereturn hanya 1 row/record, jika lebih akan terjadi error

//            Console.WriteLine(student.StudentCode + " " + student.StudentName);
//        }
//    }
//}



//class Program
//{
//    static void Main()
//    {
//        var conString = @"Server=DESKTOP-QEO3NAA\SQLEXPRESS;Database=SchoolDB;Trusted_Connection=True;";

//        using (var connection = new SqlConnection(conString))
//        {
//            var sql = "SELECT * FROM Students WHERE StudentId = @Id;" +
//                "SELECT * FROM StudentAddresses WHERE StudentId = @Id";

//            var multiStudent = connection.QueryMultiple(sql, new { Id = 2 });

//            var student = multiStudent.ReadFirstOrDefault<Students>();
//            Console.WriteLine(student.StudentCode + " " + student.StudentName);

//            var studentAddresses = multiStudent.Read<StudentAddress>().ToList();

//            foreach (var studentAddress in studentAddresses)
//            {
//                Console.WriteLine(studentAddress.Address1 + " " + studentAddress.Address2);
//            }
//        }
//    }
//}

//class Program
//{
//    static void Main()
//    {
//        var conString = @"Server=DESKTOP-QEO3NAA\SQLEXPRESS;Database=SchoolDB;Trusted_Connection=True;";

//        using (var connection = new SqlConnection(conString))
//        {
//            var sql = @"SELECT a.StudentCode, a.StudentName, b.Address1 FROM Students a
//                        JOIN StudentAddresses b ON a.StudentId = b.StudentId
//                        WHERE a.StudentId = @Id";

//            var student = connection.QueryFirstOrDefault<StudentAddrDto>(sql, new { Id = 2 });

//            // DTO = Data Transform Object

//            Console.WriteLine(student.StudentCode + " " + student.StudentName + " " + student.Address1);
//        }
//    }
//}

//class Program
//{
//    private readonly static string conString = @"Server=DESKTOP-QEO3NAA\SQLEXPRESS;Database=SchoolDB;Trusted_Connection=True;";
//    static void Main()
//    {

//        //GetData(); // ada error di bagian if checkData (misalnya)
//        GetDataWithAddress2(); // perbaikan ketika checkData
//    }

//    static void GetData() // buatan orang lain
//    {
//        using (var connection = new SqlConnection(conString))
//        {
//            var sql = @"SELECT a.StudentCode, a.StudentName, b.Address1 FROM Students a
//                        JOIN StudentAddresses b ON a.StudentId = b.StudentId
//                        WHERE a.StudentId = @Id";

//            var student = connection.QueryFirstOrDefault<StudentAddrDto>(sql, new { Id = 2 });

//            // DTO = Data Transform Object

//            Console.WriteLine(student.StudentCode + " " + student.StudentName + " " + student.Address1);
//        }
//    }

//    static void GetDataWithAddress2() // buatan kalian
//    {
//        using (var connection = new SqlConnection(conString))
//        {
//            var sql = @"SELECT a.StudentCode, a.StudentName, b.Address1, b.Address2 FROM Students a
//                        JOIN StudentAddresses b ON a.StudentId = b.StudentId
//                        WHERE a.StudentId = @Id";

//            var student = connection.QueryFirstOrDefault<StudentAddr2Dto>(sql, new { Id = 2 });

//            // DTO = Data Transform Object

//            Console.WriteLine(student.StudentCode + " " + student.StudentName + " " + student.Address1);
//        }
//    }
//}

//class Program
//{
//    static void Main()
//    {
//        string conString = @"Server=DESKTOP-QEO3NAA\SQLEXPRESS;Database=SchoolDB;Trusted_Connection=True;";
//        using (var connection = new SqlConnection(conString))
//        {
//            var sql = @"INSERT INTO Teacher(FirstName, LastName) VALUES
//                        (@FirstName, @LastName)";

//            var student = connection.Execute(sql, new { FirstName = "Anton", LastName = "Uchiha" });

//            Console.WriteLine("Insert Success");
//        }
//    }
//}

//class Program
//{
//    static void Main()
//    {
//        string conString = @"Server=DESKTOP-QEO3NAA\SQLEXPRESS;Database=SchoolDB;Trusted_Connection=True;";
//        using (var connection = new SqlConnection(conString))
//        {
//            var sql = @"UPDATE Teacher SET FirstName = @FirstName, 
//                        LastName=@LastName 
//                        WHERE TeacherId = @Id";

//            var student = connection.Execute(sql, new { Id = 4, FirstName = "Anton", LastName = "Kratos" });

//            Console.WriteLine("Insert Success");
//        }
//    }
//}

//class Program
//{
//    static void Main()
//    {
//        string conString = @"Server=DESKTOP-QEO3NAA\SQLEXPRESS;Database=SchoolDB;Trusted_Connection=True;";
//        using (var connection = new SqlConnection(conString))
//        {
//            var sql = @"DELETE FROM Teacher WHERE TeacherId = @Id";

//            var student = connection.Execute(sql, new { Id = 4 });

//            Console.WriteLine("Insert Success");
//        }
//    }
//}

//class Program
//{
//    static void Main()
//    {
//        string conString = @"Server=DESKTOP-QEO3NAA\SQLEXPRESS;Database=SchoolDB;Trusted_Connection=True;";
//        using (var connection = new SqlConnection(conString))
//        {
//            var sql = @"SELECT * FROM Teacher";

//            var student = connection.ExecuteReader(sql);

//            while (student.Read())
//            {
//                int id = student.GetInt32(0);
//                string fName = student.GetString(1);
//                string lName = student.GetString(2);

//                Console.WriteLine(id + "-" + fName + "-" + lName);
//            }
//        }
//    }
//}

//class Program
//{
//    static void Main()
//    {
//        string conString = @"Server=DESKTOP-QEO3NAA\SQLEXPRESS;Database=SchoolDB;Trusted_Connection=True;";
//        using (var connection = new SqlConnection(conString))
//        {
//            var sql = @"SELECT * FROM Teacher";

//            var reader = connection.ExecuteReader(sql);

//            DataTable table = new DataTable();
//            table.Load(reader);

//           foreach(DataRow row in table.Rows)
//            {

//                Console.WriteLine(row[0] + "-" + row[1] + "-" + row[2]);
//            }
//        }
//    }
//}


//class Program
//{
//    static void Main()
//    {
//        string conString = @"Server=DESKTOP-QEO3NAA\SQLEXPRESS;Database=SchoolDB;Trusted_Connection=True;";
//        using (var connection = new SqlConnection(conString))
//        {
//            var sql = @"EXEC GetAllTeacher";

//            var teachers = connection.Query<Teacher>(sql).ToList();

//            foreach (Teacher teacher in teachers)
//            {

//                Console.WriteLine(teacher.TeacherId + "-" + teacher.FirstName + "-" + teacher.LastName);
//            }
//        }
//    }
//}

//class Program
//{
//    static void Main()
//    {
//        string conString = @"Server=DESKTOP-QEO3NAA\SQLEXPRESS;Database=SchoolDB;Trusted_Connection=True;";
//        using (var connection = new SqlConnection(conString))
//        {
//            var sql = @"GetAllTeacher";

//            var teachers = connection.Query<Teacher>(sql, commandType: CommandType.StoredProcedure).ToList();

//            foreach (Teacher teacher in teachers)
//            {

//                Console.WriteLine(teacher.TeacherId + "-" + teacher.FirstName + "-" + teacher.LastName);
//            }
//        }
//    }
//}

//class Program
//{
//    static void Main()
//    {
//        string conString = @"Server=DESKTOP-QEO3NAA\SQLEXPRESS;Database=SchoolDB;Trusted_Connection=True;";
//        using (var connection = new SqlConnection(conString))
//        {
//            // menggunakan transaction
//            var transaction = connection.BeginTransaction(); // memulai transaction
//            try
//            {
//                var sql = @"INSERT INTO Teacher(FirstName, LastName) VALUES
//                        (@FirstName, @LastName)";

//                var student = connection.Execute(sql, new { FirstName = "Anton", LastName = "Uchiha" });

//                Console.WriteLine("Insert Success");
//                transaction.Commit(); // commit
//            }
//            catch(Exception ex)
//            {
//                Console.WriteLine(ex.Message);
//                transaction.Rollback(); // rollback
//            }
//        }
//    }
//}

//public interface IDataAccess
//{
//List<Teacher> GetAllTeacher();
//Teacher GetTeacherById(int id);
//void InsertTeacher(Teacher Teacher);
//void UpdateTeacher(Teacher Teacher);
//void DeleteTeacher(int id);
//}

//public abstract class DataAccessBase : IDataAccess
//{
//    public abstract void DeleteTeacher(int id);

//    public abstract List<Teacher> GetAllTeacher();

//    public abstract Teacher GetTeacherById(int id);

//    public abstract void InsertTeacher(Teacher Teacher);

//    public abstract void UpdateTeacher(Teacher Teacher);
//}

//public class AdoNetDataAccess : DataAccessBase
//{
//    private readonly SqlConnection _connection;

//    public AdoNetDataAccess(SqlConnection connection)
//    {
//        _connection = connection;
//    }

//    public override void DeleteTeacher(int id)
//    {
//        var transaction = _connection.BeginTransaction();
//        try
//        {
//            var sql = @"DELETE FROM Teacher WHERE TeacherId = @Id";

//            var student = _connection.Execute(sql, new { Id = id }, transaction);

//            transaction.Commit();
//            Console.WriteLine("Delete Success");
//        }
//        catch(DbException dbx)
//        {
//            transaction.Rollback();
//            Console.WriteLine("Delete Failed");
//        }        
//    }

//    public override List<Teacher> GetAllTeacher()
//    {
//        var sql = "SELECT * FROM Teacher";

//        var teachers = _connection.Query<Teacher>(sql).ToList();

//        return teachers;
//    }

//    public override Teacher GetTeacherById(int id)
//    {
//        var sql = "SELECT * FROM Teacher WHERE TeacherId = @Id";
//        var teacher = _connection.QueryFirstOrDefault<Teacher>(sql, new { Id = id });

//        return teacher;
//    }

//    public override void InsertTeacher(Teacher teacher)
//    {
//        var transaction = _connection.BeginTransaction();
//        try
//        {
//            _connection.Execute("INSERT INTO Teacher (FirstName, LastName) VALUES (@FirstName, @LastName) ",
//                new
//                {
//                    teacher.FirstName,
//                    teacher.LastName,
//                }, transaction);
//            transaction.Commit();
//        }
//        catch (DbException dbex)
//        {
//            transaction.Rollback();
//        }
//        _connection.Close();
//    }

//    public override void UpdateTeacher(Teacher teacher)
//    {
//        _connection.Open();
//        var transaction = _connection.BeginTransaction();
//        try
//        {
//            _connection.Execute("UPDATE Teacher SET FirstName = @FirstName, LastName = @LastName WHERE TeacherId = @id",
//                new
//                {
//                    teacher.TeacherId,
//                    teacher.FirstName,
//                    teacher.LastName,
//                }, transaction);
//            transaction.Commit();
//        }
//        catch (DbException dbex)
//        {
//            transaction.Rollback();
//        }
//        _connection.Close();
//    }
//}

class Program
{
    private static readonly string ConString =
    //ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
    @"Server=DESKTOP-QEO3NAA\SQLEXPRESS;Database=SchoolDB;Trusted_Connection=True;";

    public static void Main()
    {
        SqlConnection sqlConnection = new SqlConnection(ConString);
        sqlConnection.Open();

        // Create an instance of the data access layer
        IDataAccess dataAccess = new AdoNetDataAccess(sqlConnection);

        while (true)
        {
            Console.Clear();
            Console.WriteLine("1. View all Teachers");
            Console.WriteLine("2. View Teacher by ID");
            Console.WriteLine("3. Add new Teacher");
            Console.WriteLine("4. Update Teacher");
            Console.WriteLine("5. Delete Teacher");
            Console.WriteLine("6. Exit");
            Console.WriteLine();
            Console.Write("Enter your choice: ");

            int choice = int.Parse(Console.ReadLine());

            Console.WriteLine();

            if (choice == 1)
            {
                InsertTeacher();

                // Call the GetAllTeachers method
                //List<Teacher> Teachers = dataAccess.GetAllTeacher();

                //// Display the Teachers
                //foreach (Teacher Teacher in Teachers)
                //{
                //    Console.WriteLine("ID: {0}", Teacher.TeacherId);
                //    Console.WriteLine("Name: {0}", Teacher.FirstName);
                //    Console.WriteLine("Age: {0}", Teacher.LastName);
                //    Console.WriteLine();
                //}

                //Console.ReadKey();
            }
            else if (choice == 2)
            {
                // View Teacher By Id
                Console.Clear();
                Console.WriteLine("Get Teacher");
                Console.WriteLine("---------------");
                Console.Write("Id:");
                int id = int.Parse(Console.ReadLine());

                var teacher = dataAccess.GetTeacherById(id);

                if (teacher != null)
                {
                    Console.WriteLine("ID: {0}", teacher.TeacherId);
                    Console.WriteLine("Name: {0}", teacher.FirstName);
                    Console.WriteLine("Age: {0}", teacher.LastName);
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("Teacher Not Found!!");
                }

                Console.ReadKey();
            }
            else if (choice == 3)
            {
                // Insert Teacher
                Console.Clear();
                Console.WriteLine("Create Teacher");
                Console.WriteLine("---------------");
                Console.Write("First Name:");
                string firstName = Console.ReadLine();
                Console.Write("Last Name:");
                string lastName = Console.ReadLine();

                var Teacher = new Teacher()
                {
                    FirstName = firstName,
                    LastName = lastName
                };

                dataAccess.InsertTeacher(Teacher);

                Console.ReadKey();
            }
            else if (choice == 4)
            {
                // Update Teachers
                Console.Clear();
                Console.WriteLine("Update Teacher");
                Console.WriteLine("---------------");
                Console.Write("Search By Id:");
                int id = int.Parse(Console.ReadLine());

                var teacher = dataAccess.GetTeacherById(id);

                if (teacher != null)
                {
                    Console.Write("First Name : ");
                    string firstName = Console.ReadLine();
                    Console.Write("Last Name : ");
                    string lastName = Console.ReadLine();

                    teacher.FirstName = firstName;
                    teacher.LastName = lastName;

                    dataAccess.UpdateTeacher(teacher);
                }
                else
                {
                    Console.WriteLine("Teacher Not Found!!");
                }

                Console.ReadKey();
            }
            else if (choice == 5)
            {
                // Delete Teachers
                Console.Clear();
                Console.WriteLine("Delete Teacher");
                Console.WriteLine("---------------");
                Console.Write("Search By Id:");
                int id = int.Parse(Console.ReadLine());

                var teacher = dataAccess.GetTeacherById(id);

                if (teacher != null)
                {
                    dataAccess.DeleteTeacher(teacher.TeacherId);
                }
                else
                {
                    Console.WriteLine("Teacher Not Found!!");
                }

                Console.ReadKey();
            }
            else if (choice == 6)
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid selection");
            }
        }
        
        void InsertTeacher()
        {
            List<Teacher> Teachers = dataAccess.GetAllTeacher();

            // Display the Teachers
            foreach (Teacher Teacher in Teachers)
            {
                Console.WriteLine("ID: {0}", Teacher.TeacherId);
                Console.WriteLine("Name: {0}", Teacher.FirstName);
                Console.WriteLine("Age: {0}", Teacher.LastName);
                Console.WriteLine();
            }

            Console.ReadKey();
        }

        void InsertTeacherV2()
        {
            List<Teacher> Teachers = dataAccess.GetAllTeacher();

            // Display the Teachers
            foreach (Teacher Teacher in Teachers)
            {
                Console.WriteLine("ID: {0}", Teacher.TeacherId);
                Console.WriteLine("Name: {0}", Teacher.FirstName);
                Console.WriteLine("Age: {0}", Teacher.LastName);
                Console.WriteLine();
            }

            Console.ReadKey();
        }
    }    
}