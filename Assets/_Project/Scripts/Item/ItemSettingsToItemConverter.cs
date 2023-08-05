namespace HOT.Inventory.Item
{
    public static class ItemSettingsToItemConverter
    {
        public static Equipment GetEquipmentItem(EquipmentSettings settings)
        {
            if (settings is WeaponSettings weaponSettings)
            {
                return new Weapon(weaponSettings.Type, weaponSettings.Id, weaponSettings.ItemName, 
                    weaponSettings.EquipmentType, weaponSettings.MinDamage, weaponSettings.MaxDamage);   
            }

            return new Equipment(settings.Type, settings.Id, settings.ItemName, settings.EquipmentType);
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