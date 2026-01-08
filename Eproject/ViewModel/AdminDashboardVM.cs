namespace Eproject.ViewModel
{
    public class AdminDashboardVM
    {
        public int TotalProducts { get; set; }
        public int TotalOrders { get; set; }
        public int TotalCustomers { get; set; }
        public List<RecentOrderVM> RecentOrders { get; set; }
    }
}
