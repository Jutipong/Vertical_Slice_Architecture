﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Entities;

public partial class Config_WrongAnswer
{
    [Key]
    public Guid ID { get; set; }

    [StringLength(10)]
    public string ChannelCode { get; set; } = null!;

    [StringLength(10)]
    public string LevelCode { get; set; } = null!;

    public int ResultTotal { get; set; }

    public int WrongAnswer { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreateDate { get; set; }

    [StringLength(20)]
    public string CreateBy { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }

    [StringLength(20)]
    public string? UpdateBy { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string IsActive { get; set; } = null!;
}
