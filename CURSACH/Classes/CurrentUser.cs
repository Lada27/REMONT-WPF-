using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CURSACH
{
    public static class CurrentUser
    {
        private static int userId;

        public static int UserId { get => userId; set => userId = value; }

    }
}
