namespace ForeignExchange.Infrastructure
{
    using ViewModels;

    class InstanceLocator
    {
        public  MainViewModel Main { get; set; }
        public InstanceLocator()
        {
            Main = new MainViewModel();
        }
    }
}
