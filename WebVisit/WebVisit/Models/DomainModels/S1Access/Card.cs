using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[Table("Card")]
[Index("CardId", Name = "UX_CardID", IsUnique = true)]
public partial class Card
{
    /// <summary>
    /// 초기값 : 1 , 증가값 : 1
    /// </summary>
    [Column("CardID")]
    public int CardId { get; set; }

    /// <summary>
    /// 전체 Full 
    /// </summary>
    [Key]
    [StringLength(50)]
    [Unicode(false)]
    public string CardNo { get; set; } = null!;

    /// <summary>
    /// 사원ID
    /// </summary>
    [Column("PID")]
    public int? Pid { get; set; }

    [Column("CardStatusID")]
    public int CardStatusId { get; set; }

    [StringLength(50)]
    public string? CardStatusName { get; set; }

    [Column("CardTypeID")]
    public int CardTypeId { get; set; }

    [StringLength(50)]
    public string? CardTypeName { get; set; }

    [Column("FingerAuthTypeID")]
    public int FingerAuthTypeId { get; set; }

    [Column("FaceAuthTypeID")]
    public int FaceAuthTypeId { get; set; }

    [Column("FaceReaderAuthTypeID")]
    public int FaceReaderAuthTypeId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime ValidDate { get; set; }

    [StringLength(50)]
    public string? CardName { get; set; }

    [Column("EqMasterID")]
    public int? EqMasterId { get; set; }

    public int AccessRelease { get; set; }

    [StringLength(50)]
    public string? CardUser1 { get; set; }

    [StringLength(50)]
    public string? CardUser2 { get; set; }

    [StringLength(50)]
    public string? CardUser3 { get; set; }

    [StringLength(50)]
    public string? CardUser4 { get; set; }

    [StringLength(50)]
    public string? CardUser5 { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string UserCardNo { get; set; } = null!;

    [StringLength(68)]
    [Unicode(false)]
    public string? MobileCardPhoneNum { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime InsertDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    [Column("UpdateID")]
    public int UpdateId { get; set; }

    [Column("UpdateIP")]
    [StringLength(15)]
    [Unicode(false)]
    public string UpdateIp { get; set; } = null!;

    public int MasterCard { get; set; }

    public int AccessPattern { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime AccessTime { get; set; }

    [ForeignKey("CardStatusId")]
    [InverseProperty("Cards")]
    public virtual CardStatus CardStatus { get; set; } = null!;

    [ForeignKey("FaceAuthTypeId")]
    [InverseProperty("Cards")]
    public virtual FaceAuthType FaceAuthType { get; set; } = null!;

    [ForeignKey("FingerAuthTypeId")]
    [InverseProperty("Cards")]
    public virtual FingerAuthType FingerAuthType { get; set; } = null!;

    [ForeignKey("Pid")]
    [InverseProperty("Cards")]
    public virtual Person? PidNavigation { get; set; }

    [ForeignKey("UpdateId")]
    [InverseProperty("Cards")]
    public virtual EqUser Update { get; set; } = null!;
}
