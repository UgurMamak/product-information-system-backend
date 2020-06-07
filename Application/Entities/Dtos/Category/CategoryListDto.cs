using Application.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Entities.Dtos.Category
{
    public class CategoryListDto:IDto
    {
        public string Id { get; set; }
        public string CategoryName { get; set; }
    }
}
