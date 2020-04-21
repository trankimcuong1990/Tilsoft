namespace Module.BifaCompanyMng.DTO
{
    public class EditFormData
    {
        public BifaCompany BifaCompany { get; set; }

        public EditFormData()
        {
            BifaCompany = new BifaCompany();
        }
    }
}
