﻿using System;
using GeekShopping.Web.Models;

namespace GeekShopping.Web.Services.IServices
{
	public interface IProductService
	{
		Task<IEnumerable<ProductModel>> FindAllProdcuts();
		Task<ProductModel> FindProductById(long id);
		Task<ProductModel> CreateProduct(ProductModel productModel);
		Task<ProductModel> UpdateProduct(ProductModel productModel);
		Task<bool> DeleteProductById(long id);
	}
}

