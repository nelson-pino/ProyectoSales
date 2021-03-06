﻿namespace Sales.Common.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        [Required]
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool Isavalaible { get; set; }
        public DateTime PublishOn { get; set; }
        public override string ToString()
        {
            return this.Description;
        }
    }
}
