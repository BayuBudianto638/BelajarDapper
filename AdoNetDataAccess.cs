using Dapper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelajarDapper
{
    internal class AdoNetDataAccess : DataAccessBase
    {
        private readonly SqlConnection _connection;

        public AdoNetDataAccess(SqlConnection connection)
        {
            _connection = connection;
        }

        public override void DeleteTeacher(int id)
        {
            var transaction = _connection.BeginTransaction();
            try
            {
                var sql = @"DELETE FROM Teacher WHERE TeacherId = @Id";

                var student = _connection.Execute(sql, new { Id = id }, transaction);

                transaction.Commit();
                Console.WriteLine("Delete Success");
            }
            catch (DbException dbx)
            {
                transaction.Rollback();
                Console.WriteLine("Delete Failed");
            }
        }

        public override List<Teacher> GetAllTeacher()
        {
            var sql = "SELECT * FROM Teacher";

            var teachers = _connection.Query<Teacher>(sql).ToList();

            return teachers;
        }

        public override Teacher GetTeacherById(int id)
        {
            var sql = "SELECT * FROM Teacher WHERE TeacherId = @Id";
            var teacher = _connection.QueryFirstOrDefault<Teacher>(sql, new { Id = id });

            return teacher;
        }

        public override void InsertTeacher(Teacher teacher)
        {
            var transaction = _connection.BeginTransaction();
            try
            {
                _connection.Execute("INSERT INTO Teacher (FirstName, LastName) VALUES (@FirstName, @LastName) ",
                    new
                    {
                        teacher.FirstName,
                        teacher.LastName,
                    }, transaction);
                transaction.Commit();
            }
            catch (DbException dbex)
            {
                transaction.Rollback();
            }
            _connection.Close();
        }

        public override void UpdateTeacher(Teacher teacher)
        {
            _connection.Open();
            var transaction = _connection.BeginTransaction();
            try
            {
                _connection.Execute("UPDATE Teacher SET FirstName = @FirstName, LastName = @LastName WHERE TeacherId = @id",
                    new
                    {
                        teacher.TeacherId,
                        teacher.FirstName,
                        teacher.LastName,
                    }, transaction);
                transaction.Commit();
            }
            catch (DbException dbex)
            {
                transaction.Rollback();
            }
            _connection.Close();
        }
    }
}
