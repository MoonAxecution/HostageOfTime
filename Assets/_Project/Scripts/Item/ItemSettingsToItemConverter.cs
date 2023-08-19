namespace HOT.Inventory.Item
{
    public static class ItemSettingsToItemConverter
    {
        public static Equipment GetEquipmentItem(EquipmentSettings settings)
        {
            return settings switch
            {
                WeaponSettings weaponSettings => GetWeaponItem(weaponSettings),
                
                HelmetSettings helmetSettings => new Armor(helmetSettings.Type, helmetSettings.Id,
                    helmetSettings.ItemName, helmetSettings.EquipmentType, helmetSettings.AdditionalHealth),
                
                _ => new Equipment(settings.Type, settings.Id, settings.ItemName, settings.EquipmentType)
            };
        }
        
        public static Item GetItem(ItemSettings settings)
        {
            return settings switch
            {
                WeaponSettings weaponSettings => GetWeaponItem(weaponSettings),
                
                HelmetSettings helmetSettings => GetArmorItem(helmetSettings),
                
                _ => new Item(settings.Type, settings.Id, settings.ItemName)
            };
        }

        private static Weapon GetWeaponItem(WeaponSettings settings)
        {
            return new Weapon(settings.Type, settings.Id, settings.ItemName, settings.EquipmentType, settings.Skills);   
        }
        
        private static Armor GetArmorItem(HelmetSettings settings)
        {
            return new Armor(settings.Type, settings.Id, settings.ItemName, settings.EquipmentType, settings.AdditionalHealth);
        }
    }
}