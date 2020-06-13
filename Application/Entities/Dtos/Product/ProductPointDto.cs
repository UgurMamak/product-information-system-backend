using Application.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Entities.Dtos.Product
{
    public class ProductPointDto:IDto
    {
        public double Point { get; set; }
        public string Message { get; set; }//hangi işlem yapıldığını dönecek.
    }
}
