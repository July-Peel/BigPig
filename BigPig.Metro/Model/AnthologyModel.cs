using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigPig.Metro.Model
{
    public class AnthologyModel
    {
        public string Name { get; set; }
        public string gengxinzhi { get; set; }
        public string manhuafenlei { get; set; }
        public string manhuazuozhe { get; set; }
        public string manhuadiqu { get; set; }
        public string zuihougengxin { get; set; }
        public List<AnthologyList> AnthologyBtn { get; set; }
    }
    public class AnthologyList
    {
        public string Name { get; set; }
        public string URL { get; set; }
    }
}
