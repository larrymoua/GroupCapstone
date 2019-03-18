namespace GroupCapstone.Controllers
{
    internal class StripeChargeCreateOptions
    {
        public int Amount { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
        public object Source { get; set; }
    }
}