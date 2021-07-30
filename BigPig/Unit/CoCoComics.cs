using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static BigPig.Model.MenuModel;

namespace BigPig.Unit
{
    /// <summary>
    /// CoCo漫画
    /// </summary>
    public class CoCoComics
    {
        public static string UrlPath = "https://www.cocomanhua.com/show";
        //获取主页搜索节点信息
        public List<MenuDisVision> GetSearchItem()
        {
            try
            {
                string HtmlText = Disarray.HttpGetFrom(UrlPath);
                HtmlDocument HTMLDocument = new HtmlDocument();
                HTMLDocument.LoadHtml(HtmlText);
                HtmlNodeCollection mainCategoryOne = HTMLDocument.DocumentNode.SelectNodes("//dl[@style=\"white-space:nowrap\"]");
                HTMLDocument.LoadHtml(mainCategoryOne[0].InnerHtml);
                HtmlNodeCollection mainCategoryTwo = HTMLDocument.DocumentNode.SelectNodes("//a");

                List<MenuDisVision> CoCoMenuList = new List<MenuDisVision>();
                List<MenuItemData> mList = new List<MenuItemData>();
                foreach (HtmlNode item in mainCategoryTwo)
                {
                    if (item.Attributes.Count > 1) continue;
                    MenuItemData m = new MenuItemData();
                    m.ItemName = item.InnerText;
                    string[] Att = item.Attributes[0].Value.Split('&');
                    m.ItemValue = Att[Att.Length - 1];
                    mList.Add(m);
                }

                CoCoMenuList.Add(new MenuDisVision() { MenuItems = mList, Overlap = true });

                List<MenuItemData> mListTwo = new List<MenuItemData>();
                mListTwo.Add(new MenuItemData() { ItemName = "连载中", ItemValue = "status=1" });
                mListTwo.Add(new MenuItemData() { ItemName = "已完结", ItemValue = "status=2" });
                CoCoMenuList.Add(new MenuDisVision() { MenuItems = mListTwo, Overlap = true });
                List<MenuItemData> mListThree = new List<MenuItemData>();
                mListThree.Add(new MenuItemData() { ItemName = "更新日", ItemValue = "orderBy=update" });
                mListThree.Add(new MenuItemData() { ItemName = "收录日", ItemValue = "orderBy=create" });
                mListThree.Add(new MenuItemData() { ItemName = "日点击", ItemValue = "orderBy=dailyCount" });
                mListThree.Add(new MenuItemData() { ItemName = "周点击", ItemValue = "orderBy=weeklyCount" });
                mListThree.Add(new MenuItemData() { ItemName = "月点击", ItemValue = "orderBy=monthlyCount" });
                CoCoMenuList.Add(new MenuDisVision() { MenuItems = mListThree, Overlap = true });

                return CoCoMenuList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
    }
}
