using System;
using System.Linq;
namespace pavlovLab.Models
{
    public class MyModData
    {
        public string id_spare { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public int quantity { get; set; }
        public int delivery_time { get; set; }
    public BaseModelValidationResult Validate()
       {
           var validationResult = new BaseModelValidationResult();
           if (string.IsNullOrWhiteSpace(id_spare)) validationResult.Append($"ID spare cannot be empty");

           if (string.IsNullOrWhiteSpace(name)) validationResult.Append($"Name cannot be empty");
           if (!string.IsNullOrEmpty(name) && !char.IsUpper(name.FirstOrDefault())) validationResult.Append($"Name {name} should start from capital letter");

           if (!(0 < price && price < 100001)) validationResult.Append($"Price {price} is out of range (1..100000)");
           if (!(0 < quantity && quantity < 101)) validationResult.Append($"Quantity {quantity} is out of range (1..100)");
           if (!(0 < delivery_time && delivery_time < 31)) validationResult.Append($"Delivery time {delivery_time} is out of range (1..30)");
         
           return validationResult;
       }

       public override string ToString()
       {
           return $" Spare '{name}' with id {id_spare}, price: {price}; quantity in stock: {quantity}; average delivery time: {delivery_time}";
       }
   }
}
