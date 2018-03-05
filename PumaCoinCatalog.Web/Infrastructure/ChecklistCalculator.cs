using PumaCoinCatalog.Web.Models.Checklist;
using System.Linq;

namespace PumaCoinCatalog.Web.Infrastructure
{
    public class ChecklistCalculator
    {
        private readonly ChecklistModel _checklist;

        public ChecklistCalculator(ChecklistModel checklist)
        {
            _checklist = checklist;
        }

        public int GetNumberOfCoinsInChecklist()
        {
            return _checklist.ChecklistCoins.Count(x => !x.ShouldExclude);
        }

        public int GetNumberOfCoinsCollectedInChecklist()
        {
            return _checklist.ChecklistCoins.Count(x => x.InCollection && !x.ShouldExclude);
        }

        public decimal CalculateFaceValueTotal()
        {
            return _checklist.FaceValue * _checklist.ChecklistCoins.Count(x => x.InCollection && !x.ShouldExclude);
        }

        public decimal CalculateBullionValueTotal()
        {
            return _checklist.BullionValue * _checklist.ChecklistCoins.Count(x => x.InCollection && !x.ShouldExclude);
        }

        public decimal CalculateEstimatedValueTotal()
        {
            var total = _checklist.ChecklistCoins
                                  .Where(x => !x.ShouldExclude)
                                  .Sum(x => x.ValueEstimate);
            return total.Value;
        }

        public decimal CalculateCollectionValueTotal()
        {
            var total = 0m;

            foreach(var coin in _checklist.ChecklistCoins)
            {
                if (!coin.InCollection) continue;
                if (coin.ShouldExclude) continue;

                if (coin.ValueEstimate.HasValue && coin.ValueEstimate.Value > 0)
                {
                    total += coin.ValueEstimate.Value;
                }
                else if (_checklist.BullionValue > 0)
                {
                    total += _checklist.BullionValue;
                }
                else
                {
                    total += _checklist.FaceValue;
                }
            }

            return total;
        }
    }
}