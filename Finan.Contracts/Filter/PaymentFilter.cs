using Finan.Contracts.Enums;

namespace Finan.Contracts.Filters
{
    public class TransactionFilter : BaseFilter
    {
        public TransactionType TransactionType { get; set; }
        public DateType DateType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Canceled { get; set; }

        public TransactionFilter()
        {
            DateType = DateType.Due;
            StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
        }
    }
}
