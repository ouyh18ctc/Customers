using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Customers.Domain.ViewHelper;

namespace Customers.Domain.TypeDef
{
    public enum CityDef : int
    {
        Shenzhen,
        Guangzhou,
        Dongguan,
        Foshan,
        Zhongshan,
        Huizhou,
        Shantou,
        Jiangmen,
        Zhuhai,
        Jieyang,
        Zhanjiang,
        Zhaoqing,
        Chaozhou,
        Maoming,
        Meizhou,
        Shaoguan,
        Heyuan,
        Qingyuan,
        Yangjiang,
        Shanwei,
        Yunfu,
        Undefined
    }

    public static class CityDefQueries
    {
        private static Dictionary<CityDef, string> list = new Dictionary<CityDef, string>(){
            { CityDef.Shenzhen, "深圳" },
            { CityDef.Guangzhou, "广州" },
            { CityDef.Dongguan, "东莞" },
            { CityDef.Foshan, "佛山" },
            { CityDef.Zhongshan, "中山" },
            { CityDef.Huizhou, "惠州" },
            { CityDef.Shantou, "汕头" },
            { CityDef.Jiangmen, "江门" },
            { CityDef.Jieyang, "揭阳" },
            { CityDef.Zhuhai, "珠海" },
            { CityDef.Zhanjiang, "湛江" },
            { CityDef.Chaozhou, "潮州" },
            { CityDef.Zhaoqing, "肇庆" },
            { CityDef.Shaoguan, "韶关" },
            { CityDef.Maoming, "茂名" },
            { CityDef.Meizhou, "梅州" },
            { CityDef.Heyuan, "河源" },
            { CityDef.Qingyuan, "清远" },
            { CityDef.Yangjiang, "阳江" },
            { CityDef.Shanwei, "汕尾" },
            { CityDef.Yunfu, "云浮" },
            { CityDef.Undefined, "未定义" }
        };

        public static IEnumerable<ListItem> ListItems
        {
            get
            {
                return list.Select(x => new ListItem()
                {
                    Value = x.Value,
                    Text = x.Value
                }).Where(x => x.Text != "未定义");
            }
        }
        
        public static string GetCityName(this CityDef city)
        {
            return list[city];
        }

        public static CityDef GetCityIndex(this string name)
        {
            return list.ContainsValue(name) ? list.FirstOrDefault(x => x.Value == name).Key : CityDef.Undefined;
        }
    }
}
