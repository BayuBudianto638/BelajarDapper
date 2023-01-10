using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelajarDapper
{
    internal abstract class DataAccessBase : IDataAccess
    {
        public abstract void DeleteTeacher(int id);

        public abstract List<Teacher> GetAllTeacher();

        public abstract Teacher GetTeacherById(int id);

        public abstract void InsertTeacher(Teacher Teacher);

        public abstract void UpdateTeacher(Teacher Teacher);
    }
}
