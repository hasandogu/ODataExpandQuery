// Copyright (c) Microsoft Corporation.  All rights reserved.
// Licensed under the MIT License.  See License.txt in the project root for license information.

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ODataExpandQuery.Models
{
    public class Subcategory
    {
        public int Id { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        public string Name { get; set; }

        public virtual Category Category { get; set; }
    }

    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Subcategory> Subcategories { get; set; }
    }

    public class SubcategoryDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CategoryId { get; set; }
    }

    public class CategoryDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<SubcategoryDto> Subcategories { get; set; }
    }
}
