using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Excel2Any.Winform
{
    public class UIEntityHelper
    {
        private static Dictionary<Type, UIEntity> uiEntityMap = new Dictionary<Type, UIEntity>();
        static UIEntityHelper()
        {
            var entitys = ExcelHelper.GetSubTypesInAssemblys(typeof(UIEntity)).ToList();
            foreach (var item in ExcelHelper.GetAllEntityTypes())
            {
                var find = entitys.FindAll((type) =>
                {
                    var attr = type.GetCustomAttributes(typeof(EntityAttribute), false)[0] as EntityAttribute;
                    return attr.entityType == item;
                });
                var uiEntity = Activator.CreateInstance(find.First()) as UIEntity;
                uiEntity.page.SetEntityType(item);
                uiEntityMap.Add(item, uiEntity);
            };
            SettingHelper.SetPlan();
        }

        public static string GetSettingExtension(Type entityType)
        {
            return uiEntityMap[entityType].setting.GetType().Name;
        }

        public static UIEntity GetUIEntity(Type entityType)
        {
            if (uiEntityMap.ContainsKey(entityType))
            {
                return uiEntityMap[entityType];
            }
            return null;
        }

        public static void ReadSetting()
        {
            foreach (var item in uiEntityMap.Keys)
            {
                var setting = (BaseSetting)SettingHelper.GetSetting(item);
                var uiEntity = uiEntityMap[item];
                if (setting == null)
                {
                    setting = (BaseSetting)Activator.CreateInstance(uiEntity.setting.GetType());
                }
                uiEntity.setting = setting;
                ExcelHelper.GetEntity(item).SetSetting(setting);
            }
        }
    }
}
