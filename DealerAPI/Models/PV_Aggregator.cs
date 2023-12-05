using DealerAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class PV_Aggregator
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string PurchaseAmount { get; set; }

    // Assuming Make, Model, YearOfRegistration, and Variant are foreign keys
    [ForeignKey("PVA_Make")]
    public int MakeId { get; set; }
    public virtual PVA_Make? Make { get; set; }


    [ForeignKey("PVA_Model")]
    public int ModelId { get; set; }
    public virtual PVA_Model? Model { get; set; }


    [ForeignKey("PVA_YearOfReg")]
    public int YearOfRegistration { get; set; }
    public virtual PVA_YearOfReg? PVA_YearOfReg {  get; set; }


    [ForeignKey("PVA_Variant")]
    public int VariantId { get; set; }
    public virtual PVA_Variant? Variant { get; set; }


    // Image paths or URIs
    public string PriceBreak { get; set; }
    public string StockIn { get; set; }
    public string RCAvailable { get; set; }

    [ForeignKey("UserInfo")]
    public int UserInfoId { get; set; }
    public virtual UserInfo UserInfos { get; set; }
}
