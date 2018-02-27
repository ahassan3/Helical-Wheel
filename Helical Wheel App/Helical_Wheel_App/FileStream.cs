using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helical_Wheel_App
{
    public static class FileStream
    {
        public static string PathApp { get; set; }
        public static string FileName
        { get
            {
                return "ProteinList";
            }
          set
            {
                FileName = "ProteinList";
            }
        }
        public static string RootName
        {
            get
            {
                return "protein_list";
            }
            set
            {
                FileName = "protein_list";
            }
        }
    }
}
