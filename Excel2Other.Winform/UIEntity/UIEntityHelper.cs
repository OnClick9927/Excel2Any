using System;
using System.Collections.Generic;
using System.Linq;

namespace Excel2Other.Winform
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
                var uiEnity = Activator.CreateInstance(find.First()) as UIEntity;
                uiEnity.page.SetEntityType(item);
                uiEntityMap.Add(item, uiEnity);
                var setting = (BaseSetting)SettingHelper.GetSetting(item);
                if (setting == null)
                {
                    setting = (BaseSetting)Activator.CreateInstance(uiEnity.setting.GetType());
                    SettingHelper.SaveSetting(setting);
                }
                uiEnity.setting = setting;
                ExcelHelper.GetEntity(item).SetSetting(setting);
            };
        }

        public static string GetSettingExtension(Type entityType)
        {
            return uiEntityMap[entityType].setting.GetType().Name;
        }

        public static UIEntity GetUIEntity(Type entityType)
        {
            return uiEntityMap[entityType];
        }

    }
}
