using api_gpt.Validations;
using System.ComponentModel.DataAnnotations;

namespace api_gpt.DTOs;

public class QueryParameters
{
    public const int maxPageSize = 20;
    public string? CountryName { get; set; } = null;
    public int? Population { get; set; } = null;

    [ValidateSortBy]
    public string? SortBy { get; set; } = null;

    public int PageNumber { get; set; } = 1;
    
    private int _pageSize = 10;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > maxPageSize) ? maxPageSize : value;  
    }
}