﻿using System;

namespace Order.Domain.Common
{
    public abstract class EntityBase
    {
        public int Id { get; protected set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int LastModifiedBy { get; set; }

        public DateTime? LastModifiedDate { get; set; }
    }
}