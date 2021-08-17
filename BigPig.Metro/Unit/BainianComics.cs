using BigPig.Metro.Model;
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
    /// 百年漫画
    /// </summary>
    public class BainianComics
    {
        public static string UrlPath = "https://www.bnmanhua.com";
        //获取主页搜索分类节点信息
        public static List<MenuData> GetSearchItem()
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

        //分类节点下的漫画列表
        public static List<ListModel> GetSreachList(string AppUrl, out int Index,out string PageTxt)
        {
            Index = 0;
            PageTxt = string.Empty;
            try
            {
                string URL = UrlPath + AppUrl;
                string HtmlText = Disarray.HttpGetFrom(URL);
                HtmlDocument HTMLDocument = new HtmlDocument();
                HTMLDocument.LoadHtml(HtmlText);

                HtmlNodeCollection PageOne = HTMLDocument.DocumentNode.SelectNodes("//div[@class=\"pagination\"]");
                PageTxt = PageOne[0].InnerText.Split(new string[] { "页"},StringSplitOptions.None)[2];


                HTMLDocument.LoadHtml(PageOne[0].InnerHtml);
                HtmlNodeCollection PageTwo = HTMLDocument.DocumentNode.SelectNodes("//a[@class=\"page-link\"]");
                Index = Convert.ToInt32(PageTwo[PageTwo.Count - 2].InnerText);


                HTMLDocument.LoadHtml(HtmlText);
                HtmlNodeCollection ListOne = HTMLDocument.DocumentNode.SelectNodes("//ul[@id=\"list_img\"]");
                HTMLDocument.LoadHtml(ListOne[0].InnerHtml);

                HtmlNodeCollection ListTwo = HTMLDocument.DocumentNode.SelectNodes("//li");
                List<ListModel> ListData = new List<ListModel>();

                foreach (HtmlNode item in ListTwo)
                {

                    HtmlDocument DocumentTwo = new HtmlDocument();
                    DocumentTwo.LoadHtml(item.InnerHtml);

                    ListModel m = new ListModel();
                    HtmlNodeCollection ListThree = DocumentTwo.DocumentNode.SelectNodes("//a");
                    m.ContentPath = ListThree[0].Attributes[0].Value;

                    HtmlNodeCollection ListFour = DocumentTwo.DocumentNode.SelectNodes("//img");
                    m.ImagePath = ListFour[0].Attributes.FirstOrDefault(a => a.Name == "src").Value;

                    HtmlNodeCollection ListFive = DocumentTwo.DocumentNode.SelectNodes("//span");
                    m.NewAnthology = "最新话："+ ListFive[0].InnerText;

                    HtmlNodeCollection ListSix = DocumentTwo.DocumentNode.SelectNodes("//p");
                    m.Name = ListSix[0].InnerText;

                    HtmlNodeCollection ListSeven = DocumentTwo.DocumentNode.SelectNodes("//em");
                    m.Update = "更新时间："+ ListSeven[0].InnerText.Replace(" ", "").Replace("\r", "").Replace("\n", "");

                    ListData.Add(m);
                }





                return ListData;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }

        }

        //漫画详情
        public static AnthologyModel GetSearchAnthology(string AppUrl)
        {
            try
            {
                string URL = UrlPath + AppUrl;
                string HtmlText = Disarray.HttpGetFrom(URL);
                HtmlDocument HTMLDocument = new HtmlDocument();
                HTMLDocument.LoadHtml(HtmlText);


                AnthologyModel m = new AnthologyModel();//拆分

                HtmlNodeCollection PageOne = HTMLDocument.DocumentNode.SelectNodes("//div[@style=\"height:257px;\"]");





                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
    }
}
