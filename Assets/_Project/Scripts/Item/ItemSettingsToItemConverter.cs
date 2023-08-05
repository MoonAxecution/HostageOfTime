namespace HOT.Inventory.Item
{
    public static class ItemSettingsToItemConverter
    {
        public static Item GetItem(ItemSettings itemSettings)
        {
            if (itemSettings is WeaponSettings weaponSettings)
                return new Weapon(weaponSettings.Type, weaponSettings.Id, weaponSettings.ItemName, weaponSettings.MinDamage, weaponSettings.MaxDamage);

            return new Item(itemSettings.Type, itemSettings.Id, itemSettings.ItemName);
        }
    }
}