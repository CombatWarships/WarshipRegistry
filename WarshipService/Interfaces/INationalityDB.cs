using WarshipRegistryAPI.Nationality;

namespace Warships.Interfaces
{
    public interface INationalityDB
   {
      List<Nationality> GetFullList();
   }
}