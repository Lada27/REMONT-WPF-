using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CURSACH.Classes
{
    public class Request
    {
        public int requestID;
        public DateTime startDate;
        public string homeTechType;
        public string homeTechModel;
        public string problemDescryption;
        public string requestStatus;
        public DateTime completionDate;
        public string repairParts;
        public int? masterID; // Может быть null
        public int clientID;

        public static implicit operator Request(Request v)
        {
            throw new NotImplementedException();
        }
    }
}
