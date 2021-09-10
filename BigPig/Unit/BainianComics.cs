using BigPig.Model;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using static BigPig.Model.MenuModel;

namespace BigPig.Unit
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
                HTMLDocument.LoadHtml(PageOne[0].InnerHtml);
                HtmlNodeCollection PageTwo = HTMLDocument.DocumentNode.SelectNodes("//img");
                m.ImagePath = PageTwo[0].Attributes[1].Value;
                HtmlNodeCollection PageThree = HTMLDocument.DocumentNode.SelectNodes("//li");


                string[] manhuaXq = PageThree[0].InnerText.Replace(" ", "").Replace("&nbsp;", "").Split(new string[] { "\r\n" }, StringSplitOptions.None);
                m.Name = manhuaXq[0];
                m.gengxinzhi = manhuaXq[1];
                m.manhuafenlei = manhuaXq[2];
                m.manhuazuozhe = manhuaXq[3];
                m.manhuadiqu = manhuaXq[4];
                m.zuihougengxin = manhuaXq[5];

                
                List<AnthologyList> AnthologyBtn = new List<AnthologyList>();
                HTMLDocument.LoadHtml(HtmlText);
                HtmlNodeCollection PageFive = HTMLDocument.DocumentNode.SelectNodes("//ul[@class=\"jslist01\"]");
                HTMLDocument.LoadHtml(PageFive[0].InnerHtml);
                HtmlNodeCollection PageSix = HTMLDocument.DocumentNode.SelectNodes("//a");

                foreach (HtmlNode item in PageSix)
                {
                    AnthologyList b = new AnthologyList();
                    b.Name = item.InnerText;
                    b.URL = item.Attributes[0].Value;
                    AnthologyBtn.Add(b);
                }
                m.AnthologyBtn = AnthologyBtn;

                return m;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        //漫画内容图片
        public static List<string> GetSearchManhuaImage(string ImageUrl)
        {
            try
            {
                string ImgPath = UrlPath + ImageUrl;
                string HtmlText = Disarray.HttpGetFrom(ImgPath);

                MatchCollection MatchStr = Regex.Matches(HtmlText, @"(\[.+?])");
                List<string> ImageList = new List<string>();
                foreach (Match item in MatchStr)
                {
                    if (item.Value.Contains(".jpg"))
                    {
                        string[] Images = item.Value.Replace("[", "").Replace("]", "").Replace("\"", "").Split(',');
                        foreach (string m in Images)
                        {
                            ImageList.Add("https://img.jiequegongchang.com/" + m);
                        }
                        break;
                    }
                }

                return ImageList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
    }
}
