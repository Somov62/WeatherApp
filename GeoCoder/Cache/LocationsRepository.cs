using GeoCoder.Models;

namespace GeoCoder.Cache
{
    internal static class LocationsRepository
    {
        public static List<GeoLocation> Find(string place)
        {
            using (GeoCoderDatabase db = new GeoCoderDatabase())
            {
                return db.Locations.Where(p => p.Name.Contains(place)).ToList();
                db.SaveChanges();
            }
        }

        public static void SaveArrange(List<GeoLocation> locations)
        {
            using (GeoCoderDatabase db = new GeoCoderDatabase())
            {
                foreach (var item in locations)
                {
                    if (db.Locations.Find(item.Latitude, item.Longitude) == null)
                        db.Locations.Add(item);
                }
                db.SaveChanges();
            }
        }

    }
}
