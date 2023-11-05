﻿namespace Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public double TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }

        public int UserId { get; set; }
    }
}