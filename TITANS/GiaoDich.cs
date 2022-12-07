using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TITANS
{
    public class GiaoDich
    {
            public static List<GiaoDich> cacGiaoDich = new List<GiaoDich>();


        public string name { get; set; }
        public List<noiDung> content { get; set; }
    }



    public class noiDung
    {
        public string STT { get; set; }
        public string MaCK { get; set; }
        public string TongKLGDMua { get; set; }
        public string TongGTGDMua { get; set; }
        public string TongKLGDBan { get; set; }
        public string TongGTGDBan { get; set; }
        public string TongKLGDTuDoanh { get; set; }
        public string TongGTGDTuDoanh { get; set; }

    }
}
