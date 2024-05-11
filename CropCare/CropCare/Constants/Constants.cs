namespace CropCare.Constants
{
    public static class Constants
    {
        public static readonly Dictionary<string, string> Icons = new Dictionary<string, string>();

        static Constants()
        {
            Constants.PopulateIconsDictionary();
        }

        static void PopulateIconsDictionary()
        {
            Icons.Add("Flower", "flower_farm_icon.svg");
            Icons.Add("Leaf", "flower_farm_icon.svg");
            Icons.Add("Water Drop", "waterdrop_farm_icon.svg");
            Icons.Add("Tulip", "tulip_farm_icon.svg");
            Icons.Add("Sunflower", "sunflower_farm_icon.svg");
            Icons.Add("Litte Plant", "littleplant_farm_icon.svg");
            Icons.Add("Grass", "grass_farm_icon.svg");
            Icons.Add("Potted Plant", "pottedplant_farm_icon.svg");
            Icons.Add("Tractor", "tractor_farm_icon.svg");
            Icons.Add("Tree", "tree_farm_icon.svg");
        }

        // Method to retrieve an icon path by its key
        public static string GetIconPath(string key)
        {
            if (Icons.ContainsKey(key))
                return Icons[key];
            else
                return null;
        }
    }
}
