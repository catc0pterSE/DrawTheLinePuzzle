using Data;
using Infrastructure.Services;
using UI.MainMenu;

namespace Infrastructure.Progression
{
    public interface IProgressionService : IService
    {
        public LevelsCompleteData LevelsCompleteData { get; }
    }
}