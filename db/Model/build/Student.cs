using common.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace db.Model
{
    public partial class Student : BaseModel
    {
        public int Sid { get; set; }

        public string Sname { get; set; }

        public DateTime AddTime { get; set; }

        public static readonly string TableName = "student";

        public static readonly string[] Fields = { "sid", "sname", "addTime"};
    }
}
