﻿using Application.Query.GetCategory;
using Application.Services;
using MediatR;

namespace Application.Query.GetCategories
{
    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, IEnumerable<GetCategoryQueryResponse>>
    {
        private readonly ICategoryReadService _categoryRepository;
        public GetCategoriesQueryHandler(ICategoryReadService categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<GetCategoryQueryResponse>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var listOfCategories = await _categoryRepository.GetCategoriesAsync();
            return listOfCategories;
        }
    }
}