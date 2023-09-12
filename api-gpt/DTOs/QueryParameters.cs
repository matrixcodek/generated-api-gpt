using api_gpt.Validations;
using System.ComponentModel.DataAnnotations;

namespace api_gpt.DTOs;

public class QueryParameters
{
    public string? CountryName { get; set; } = null;
    public int? Population { get; set; } = null;

    //[RegularExpression("^ascend$|^descend$", ErrorMessage = "The sortBy filter must be either 'ascend' or 'descend' only")]

    [ValidateSortBy]
    public string? SortBy { get; set; } = null;
}