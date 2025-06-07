namespace RM_STO_DIPLOM.ViewModels
{
    public class Commodity_B
    {
        string ComB_VP { get; }
        string ComB_Summ { get; }
        string ComB_INN { get; }

        public Commodity_B(string comB_VP, string comB_Summ, string comB_INN)
        {
            ComB_VP = comB_VP;
            ComB_Summ = comB_Summ;
            ComB_INN = comB_INN;
        }
    }
}

