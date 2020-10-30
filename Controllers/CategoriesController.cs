// Copyright (c) Microsoft Corporation.  All rights reserved.
// Licensed under the MIT License.  See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ODataExpandQuery.Models;
using AutoMapper;
using AutoMapper.AspNet.OData;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ODataExpandQuery.Controllers
{
    public class CategoriesController : ODataController
    {
        private readonly CategoryContext _context;
        private readonly IMapper _mapper;

        private readonly List<Category> _categories;

        public CategoriesController(CategoryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;


            _categories = new List<Category>
                {
                    new Category
                    {
                        Id = 1,
                        Name = "Cat1",
                        Subcategories = new List<Subcategory>()
                        {
                            new Subcategory
                            {
                                Id = 1,
                                Name = "Sub1"
                            },
                            new Subcategory
                            {
                                Id = 2,
                                Name = "Sub2"
                            },
                            new Subcategory
                            {
                                Id = 3,
                                Name = "Sub3"
                            }
                        }
                    },
                    new Category
                    {
                        Id = 2,
                        Name = "Cat2",
                        Subcategories = new List<Subcategory>()
                        {
                            new Subcategory
                            {
                                Id = 4,
                                Name = "Sub4"
                            },
                            new Subcategory
                            {
                                Id = 5,
                                Name = "Sub5"
                            },
                            new Subcategory
                            {
                                Id = 6,
                                Name = "Sub6"
                            }
                        }
                    }
                };


            if (_context.Categories.Count() == 0)
            {
                foreach (var category in _categories)
                {
                    _context.Categories.Add(category);
                    _context.Subcategories.AddRange(category.Subcategories);
                }

                _context.SaveChanges();
            }
        }

        // Run this query and observe that all the elements are returned in the subcollections instead of 1
        // http://localhost:15580/odata/categories?$expand=subcategories($orderby=id;$top=1)
        // This is because we are enabling lower camel case with OData 
        // When $top or $skip are not ignored, they should also require orderby. If you remove orderby clause
        // You see that it doesn't throw an exception

        [AllowAnonymous, HttpGet]
        public async Task<IActionResult> Get(ODataQueryOptions<CategoryDto> options)
        {
            var query1 = await _context.Categories.GetQueryAsync(_mapper, options);

//            var query2 = await _categories.AsQueryable().GetQueryAsync(_mapper, options);

            return Ok(query1);
        }

    }
}
