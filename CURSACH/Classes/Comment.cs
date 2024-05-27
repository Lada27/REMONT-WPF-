using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CURSACH.Classes
{
    public class Comment
    {
        public int commentID;
        public string message;
        public int? masterID; // Может быть null
        public int requestID;
    }
}
