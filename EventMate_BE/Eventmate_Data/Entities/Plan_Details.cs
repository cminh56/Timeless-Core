﻿using EventMate_Common.Status;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventMate_Data.Entities
{
    public class Plan_Details
    {
        [Key]
        public Guid PlanDetailsId { get; set; }
        [Required]
        public Guid PlanId { get; set; }
        
        [Required]
        public string Content { get; set; } = string.Empty;

        public DateTime? Schedule { get; set; }

        [Required]
        public PlanDetailStatus Status { get; set; }
        [ForeignKey("PlanId")] public virtual Plans Plan { get; set; }

    }
}
