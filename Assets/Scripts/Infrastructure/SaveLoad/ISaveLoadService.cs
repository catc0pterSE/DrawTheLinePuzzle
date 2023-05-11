using Infrastructure.Services;

namespace Infrastructure.SaveLoad
{
    public interface ISaveLoadService : IService

    {
    public bool TryLoad<T>(out T loadedObject) where T : class, ISavable;
    public void RegisterSavable(ISavable savable);
    public void SaveAll();
    }
}