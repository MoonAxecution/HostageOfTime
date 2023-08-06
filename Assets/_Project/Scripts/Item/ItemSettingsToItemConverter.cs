namespace HOT.Inventory.Item
{
    public static class ItemSettingsToItemConverter
    {
        public static Equipment GetEquipmentItem(EquipmentSettings settings)
        {
            return settings switch
            {
                WeaponSettings weaponSettings => new Weapon(weaponSettings.Type, weaponSettings.Id,
                    weaponSettings.ItemName, weaponSettings.EquipmentType, weaponSettings.MinDamage,
                    weaponSettings.MaxDamage),
                
                HelmetSettings helmetSettings => new Armor(helmetSettings.Type, helmetSettings.Id,
                    helmetSettings.ItemName, helmetSettings.EquipmentType, helmetSettings.AdditionalHealth),
                
                _ => new Equipment(settings.Type, settings.Id, settings.ItemName, settings.EquipmentType)
            };
        }
        
        public static Item GetItem(ItemSettings settings)
        {
            if (settings is WeaponSettings weaponSettings)
            {
                return new Weapon(weaponSettings.Type, weaponSettings.Id, weaponSettings.ItemName, 
                    weaponSettings.EquipmentType, weaponSettings.MinDamage, weaponSettings.MaxDamage);   
            }

            return new Item(settings.Type, settings.Id, settings.ItemName);
        }
    }
}