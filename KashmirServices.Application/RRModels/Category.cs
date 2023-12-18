using AutoMapper.Configuration.Annotations;
using KashmirServices.Domain.Entities;
using KashmirServices.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashmirServices.Application.RRModels;

public class CategoryRequest
{
    [Required(ErrorMessage = "Brand Name is required")]
    public string Name { get; set; } = null!;


    public Guid ManagerId { get; set; }

    public string Description { get; set; } = string.Empty;


    public IFormFile? File { get; set; } = null!;
}





public class CategoryResponse 
{
    public Guid Id { get; set; }


    public string Name { get; set; } = null!;


    public string Description { get; set; } = string.Empty;


    public string FilePath { get; set; } = null!;

    public Guid ManagerId { get; set; }


     public DateTimeOffset CreatedOn { get; set; }

    public string ManagerName { get; set; } = string.Empty;
}



public class UpdateCategoryRequest : CategoryRequest
{
    public Guid Id { get; set; }
}