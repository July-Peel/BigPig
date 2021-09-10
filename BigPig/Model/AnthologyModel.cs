using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigPig.Model
{
    public class AnthologyModel
    {
        public string Name { get; set; }//名称
        public string ImagePath { get; set; }//封面地址
        public string gengxinzhi { get; set; }//更新至
        public string manhuafenlei { get; set; }//漫画分类
        public string manhuazuozhe { get; set; }//漫画作者
        public string manhuadiqu { get; set; }//漫画地区
        public string zuihougengxin { get; set; }//最后更新
        public List<AnthologyList> AnthologyBtn { get; set; }
    }
    public class AnthologyList
    {
        public string Name { get; set; }
        public string URL { get; set; }
    }
}
