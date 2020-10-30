// Copyright (c) Microsoft Corporation.  All rights reserved.
// Licensed under the MIT License.  See License.txt in the project root for license information.

using Microsoft.AspNet.OData.Builder;
using Microsoft.OData.Edm;

namespace ODataExpandQuery.Models
{
    public static class EdmModelBuilder
    {
        private static IEdmModel _edmModel;

        public static IEdmModel GetEdmModel()
        {
            if (_edmModel == null)
            {
                var builder = new ODataConventionModelBuilder();

                // This is what's causing the ignored query options in expansion
                builder.EnableLowerCamelCase(NameResolverOptions.ProcessReflectedPropertyNames);
                
                builder.EntitySet<CategoryDto>("Categories");
                builder.EntitySet<SubcategoryDto>("Subcategories");

                _edmModel = builder.GetEdmModel();
            }

            return _edmModel;
        }

    }
}