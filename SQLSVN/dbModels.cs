using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbModels
{
    public class tAuthType
    {
        public byte Id { get; set; }
        public string Text { get; set; }
    }
    [Table("tProject")]
    public class tProject
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
    }
    [Table("tScript")]
    public class tScript
    {
        public int Id { get; set; }
        public int scriptId { get; set; }        
        public int tProjectId { get; set; }        
        public string sql { get; set; }
    }
    [Table("tSQLSVN_Version")]
    public class tSQLSVNVersion
    {
        public int Id { get; set; }
        public int VerNumber { get; set; }
    }
}
