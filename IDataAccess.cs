using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelajarDapper
{
    internal interface IDataAccess
    {
        List<Teacher> GetAllTeacher();
        Teacher GetTeacherById(int id);
        void InsertTeacher(Teacher Teacher);
        void UpdateTeacher(Teacher Teacher);
        void DeleteTeacher(int id);
    }
}
