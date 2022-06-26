
using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class CustomMapper : ClassMapper<Teacher>
    {
        public CustomMapper()
        {
/*            Table("Teacher");

            Map(f => f.CombinationSubjectName).Ignore();*/
        }
    }
}
