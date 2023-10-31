using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.S1Access;

[Table("AccessCommonToEqMaster")]
public partial class AccessCommonToEqMaster
{
    [Key]
    public int Idx { get; set; }

    /// <summary>
    /// 출입문, 방범의 ID
    /// </summary>
    [Column("EqMasterID")]
    public int EqMasterId { get; set; }

    /// <summary>
    /// 방범 : 0 / 출입: 일반 0 , 마스터 1
    /// </summary>
    public int IsMaster { get; set; }

    /// <summary>
    /// 0~7번 출입모드
    /// </summary>
    public int ModeNum { get; set; }

    /// <summary>
    /// 권한설정 화면번호
    /// </summary>
    [Column("UIID")]
    public int Uiid { get; set; }

    /// <summary>
    /// 0 권한없음, 1 권한있음. 
    /// </summary>
    public int HasAccess { get; set; }

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
}
