using Data;
using Infrastructure.SaveLoad;
using UI.MainMenu;
using Utility.Static;

namespace Infrastructure.Progression
{
    public class ProgressionService : IProgressionService
    {
        private readonly ISaveLoadService _saveLoadService;
        
        private LevelsCompleteData _levelsCompleteData;

        public ProgressionService(ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
            InitializeLevelsCompleteData();
        }

        private void InitializeLevelsCompleteData()
        {
            _levelsCompleteData = _saveLoadService.TryLoad(out LevelsCompleteData levelsCompleteData)
                ? levelsCompleteData
                : new LevelsCompleteData();
            
            _levelsCompleteData.Update(NumericConstants.TotalLevelsNumber);
            _saveLoadService.RegisterSavable(_levelsCompleteData);
        }

        public LevelsCompleteData LevelsCompleteData => _levelsCompleteData;
    }
}