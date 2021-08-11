using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static BigPig.Metro.Model.MenuModel;

namespace BigPig.Metro.Unit
{
    /// <summary>
    /// CoCo漫画
    /// </summary>
    public class BainianComics
    {
        public static string UrlPath = "https://www.bnmanhua.com";
        //获取主页搜索节点信息
        public List<MenuData> GetSearchItem()
        {
            try
            {
                string MainUrl = UrlPath + "/page/wanjie.html/";

                string HtmlText = Disarray.HttpGetFrom(MainUrl);
                HtmlDocument HTMLDocument = new HtmlDocument();
                HTMLDocument.LoadHtml(HtmlText);
                HtmlNodeCollection mainCategoryOne = HTMLDocument.DocumentNode.SelectNodes("//div[@class=\"select l mb10\"]");
                HTMLDocument.LoadHtml(mainCategoryOne[0].InnerHtml);


                HtmlNodeCollection mainCategoryTwo = HTMLDocument.DocumentNode.SelectNodes("//dl");


                List<string> Html = new List<string>();
                mainCategoryTwo.Nodes().ToList().ForEach(a => Html.Add(a.InnerHtml));

                List<MenuData> mList = new List<MenuData>();
                foreach (string item in Html)
                {

                    HTMLDocument.LoadHtml(item);
                    HtmlNodeCollection mainCategoryThree = HTMLDocument.DocumentNode.SelectNodes("//a[@href]");

                    if (mainCategoryThree == null) continue;
                    if (mainCategoryThree[0].Attributes.Count > 1) continue;
                    MenuData md = new MenuData();
                    md.ItemName = mainCategoryThree[0].InnerText;
                    md.ItemValue = mainCategoryThree[0].Attributes[0].Value;
                    mList.Add(md);

                }


                return mList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
    }
}
