﻿namespace SingluarSystems.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public decimal SalePrice { get; set; }
        public string? Category { get; set; }
        public string? Image { get; set; }
    }
}
