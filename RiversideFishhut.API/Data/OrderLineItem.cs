﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RiversideFishhut.API.Data
{
    public class OrderLineItem
    {
        public int OrderLineItemId { get; set; }

        [ForeignKey(nameof(OrderId))]
        public int OrderId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }
}