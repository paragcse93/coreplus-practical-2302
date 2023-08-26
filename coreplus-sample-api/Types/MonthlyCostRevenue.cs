namespace Coreplus.Sample.Api.Types
{
    public class MonthlyCostRevenue
    {
        public string Month { get; set; }
        public decimal TotalCost { get; set; }
        public decimal TotalRevenue { get; set; }

        public static implicit operator MonthlyCostRevenue(MonthlyCostRevenueHolder v)
        {
            throw new NotImplementedException();
        }
    }


    public class MonthlyCostRevenueHolder
    {
        public string PractinerName { get; set; }
        public List<MonthlyCostRevenue> MonthWiseData { get; set; }
     


    }

}
