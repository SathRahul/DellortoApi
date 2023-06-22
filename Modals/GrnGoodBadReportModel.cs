namespace DellortoApi.Modals
{
    public class GrnGoodBadReportModel
    {
        public string vendor_invoice_date { get; set; }
        public string mrn_lot_no { get; set;}
        public int qty { get; set;} 
        public string ok_reject { get; set;}
    }
}
