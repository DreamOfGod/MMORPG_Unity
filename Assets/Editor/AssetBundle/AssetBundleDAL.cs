//===============================================
//作    者：
//创建时间：2022-03-23 15:14:25
//备    注：
//===============================================

using System.Collections.Generic;
using System.Xml.Linq;

public class AssetBundleDAL
{
    public static List<AssetBundleEntity> GetList(string xmlPath)
    {
        //读取xml
        XDocument xDoc = XDocument.Load(xmlPath);
        XElement root = xDoc.Root;
        XElement assetBundleNode = root.Element("AssetBundle");

        IEnumerable<XElement> lst = assetBundleNode.Elements("Item");
        int key = 1;
        List<AssetBundleEntity> list = new List<AssetBundleEntity>();
        foreach(XElement item in lst)
        {
            AssetBundleEntity entity = new AssetBundleEntity();
            entity.Key = "key" + key++;
            entity.Name = item.Attribute("Name").Value;
            entity.Tag = item.Attribute("Tag").Value;
            entity.Version = item.Attribute("Version").Value.ToInt();
            entity.Size = item.Attribute("Size").Value.ToLong();
            entity.ToPath = item.Attribute("ToPath").Value;

            IEnumerable<XElement> pathList = item.Elements("Path");
            foreach(XElement path in pathList) {
                entity.PathList.Add(string.Format("Assets/{0}", path.Attribute("Value").Value));
            }

            list.Add(entity);
        }
        return list;
    }
}