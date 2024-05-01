using Newtonsoft.Json;

namespace FoodRecipeProvider.Models.APIRecipeResponse
{
    public class TotalDaily
    {
        public ENERCKCAL ENERC_KCAL { get; set; }
        public FAT FAT { get; set; }
        public FASAT FASAT { get; set; }
        public CHOCDF CHOCDF { get; set; }
        public FIBTG FIBTG { get; set; }
        public PROCNT PROCNT { get; set; }
        public CHOLE CHOLE { get; set; }
        public NA NA { get; set; }
        public CA CA { get; set; }
        public MG MG { get; set; }
        public K K { get; set; }
        public FE FE { get; set; }
        public ZN ZN { get; set; }
        public P P { get; set; }
        public VITARAE VITA_RAE { get; set; }
        public VITC VITC { get; set; }
        public THIA THIA { get; set; }
        public RIBF RIBF { get; set; }
        public NIA NIA { get; set; }
        public VITB6A VITB6A { get; set; }
        public FOLDFE FOLDFE { get; set; }
        public VITB12 VITB12 { get; set; }
        public VITD VITD { get; set; }
        public TOCPHA TOCPHA { get; set; }
        public VITK1 VITK1 { get; set; }
    }

    public class TotalNutrients
    {
        public ENERCKCAL ENERC_KCAL { get; set; }
        public FAT FAT { get; set; }
        public FASAT FASAT { get; set; }
        public FATRN FATRN { get; set; }
        public FAMS FAMS { get; set; }
        public FAPU FAPU { get; set; }
        public CHOCDF CHOCDF { get; set; }

        [JsonProperty("CHOCDF.net")]
        public CHOCDFNet CHOCDFnet { get; set; }
        public FIBTG FIBTG { get; set; }
        public SUGAR SUGAR { get; set; }

        [JsonProperty("SUGAR.added")]
        public SUGARAdded SUGARadded { get; set; }
        public PROCNT PROCNT { get; set; }
        public CHOLE CHOLE { get; set; }
        public NA NA { get; set; }
        public CA CA { get; set; }
        public MG MG { get; set; }
        public K K { get; set; }
        public FE FE { get; set; }
        public ZN ZN { get; set; }
        public P P { get; set; }
        public VITARAE VITA_RAE { get; set; }
        public VITC VITC { get; set; }
        public THIA THIA { get; set; }
        public RIBF RIBF { get; set; }
        public NIA NIA { get; set; }
        public VITB6A VITB6A { get; set; }
        public FOLDFE FOLDFE { get; set; }
        public FOLFD FOLFD { get; set; }
        public FOLAC FOLAC { get; set; }
        public VITB12 VITB12 { get; set; }
        public VITD VITD { get; set; }
        public TOCPHA TOCPHA { get; set; }
        public VITK1 VITK1 { get; set; }
        public WATER WATER { get; set; }
    }
    public class CA
    {
        public string label { get; set; }
        public double quantity { get; set; }
        public string unit { get; set; }
    }

    public class CHOCDF
    {
        public string label { get; set; }
        public double quantity { get; set; }
        public string unit { get; set; }
    }

    public class CHOCDFNet
    {
        public string label { get; set; }
        public double quantity { get; set; }
        public string unit { get; set; }
    }

    public class CHOLE
    {
        public string label { get; set; }
        public double quantity { get; set; }
        public string unit { get; set; }
    }
    public class ENERCKCAL
    {
        public string label { get; set; }
        public double quantity { get; set; }
        public string unit { get; set; }
    }

    public class FAMS
    {
        public string label { get; set; }
        public double quantity { get; set; }
        public string unit { get; set; }
    }

    public class FAPU
    {
        public string label { get; set; }
        public double quantity { get; set; }
        public string unit { get; set; }
    }

    public class FASAT
    {
        public string label { get; set; }
        public double quantity { get; set; }
        public string unit { get; set; }
    }

    public class FAT
    {
        public string label { get; set; }
        public double quantity { get; set; }
        public string unit { get; set; }
    }

    public class FATRN
    {
        public string label { get; set; }
        public double quantity { get; set; }
        public string unit { get; set; }
    }

    public class FE
    {
        public string label { get; set; }
        public double quantity { get; set; }
        public string unit { get; set; }
    }

    public class FIBTG
    {
        public string label { get; set; }
        public double quantity { get; set; }
        public string unit { get; set; }
    }

    public class FOLAC
    {
        public string label { get; set; }
        public double quantity { get; set; }
        public string unit { get; set; }
    }

    public class FOLDFE
    {
        public string label { get; set; }
        public double quantity { get; set; }
        public string unit { get; set; }
    }

    public class FOLFD
    {
        public string label { get; set; }
        public double quantity { get; set; }
        public string unit { get; set; }
    }
    public class K
    {
        public string label { get; set; }
        public double quantity { get; set; }
        public string unit { get; set; }
    }

    public class MG
    {
        public string label { get; set; }
        public double quantity { get; set; }
        public string unit { get; set; }
    }

    public class NA
    {
        public string label { get; set; }
        public double quantity { get; set; }
        public string unit { get; set; }
    }

    public class NIA
    {
        public string label { get; set; }
        public double quantity { get; set; }
        public string unit { get; set; }
    }

    public class P
    {
        public string label { get; set; }
        public double quantity { get; set; }
        public string unit { get; set; }
    }

    public class PROCNT
    {
        public string label { get; set; }
        public double quantity { get; set; }
        public string unit { get; set; }
    }
    public class RIBF
    {
        public string label { get; set; }
        public double quantity { get; set; }
        public string unit { get; set; }
    }
    public class SUGAR
    {
        public string label { get; set; }
        public double quantity { get; set; }
        public string unit { get; set; }
    }

    public class SUGARAdded
    {
        public string label { get; set; }
        public double quantity { get; set; }
        public string unit { get; set; }
    }

    public class THIA
    {
        public string label { get; set; }
        public double quantity { get; set; }
        public string unit { get; set; }
    }

    public class TOCPHA
    {
        public string label { get; set; }
        public double quantity { get; set; }
        public string unit { get; set; }
    }
    public class VITARAE
    {
        public string label { get; set; }
        public double quantity { get; set; }
        public string unit { get; set; }
    }

    public class VITB12
    {
        public string label { get; set; }
        public double quantity { get; set; }
        public string unit { get; set; }
    }

    public class VITB6A
    {
        public string label { get; set; }
        public double quantity { get; set; }
        public string unit { get; set; }
    }

    public class VITC
    {
        public string label { get; set; }
        public double quantity { get; set; }
        public string unit { get; set; }
    }

    public class VITD
    {
        public string label { get; set; }
        public double quantity { get; set; }
        public string unit { get; set; }
    }

    public class VITK1
    {
        public string label { get; set; }
        public double quantity { get; set; }
        public string unit { get; set; }
    }

    public class WATER
    {
        public string label { get; set; }
        public double quantity { get; set; }
        public string unit { get; set; }
    }

    public class ZN
    {
        public string label { get; set; }
        public double quantity { get; set; }
        public string unit { get; set; }
    }
}
