using Microsoft.AspNetCore.Mvc;
using Nest;

namespace ElasticSearchExample.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController: ControllerBase
{
    private readonly IElasticClient _elasticClient;
    
    public ProductsController(IElasticClient elasticClient)
    {
        _elasticClient = elasticClient;
    }
    
    [HttpPost]
    public async Task<IActionResult> IndexProduct(Product product)
    {
        var response = await _elasticClient.IndexDocumentAsync(product);
        return Ok(response.Result);
    }

    [HttpGet]
    public async Task<IActionResult> Search(string query)
    {
        var response = await _elasticClient.SearchAsync<Product>(s => s
            .Query(q => q
                .Match(m => m
                    .Field(f => f.Name)
                    .Query(query)
                )
            )
        );

        return Ok(response.Documents);
    }
    
}