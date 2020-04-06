using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DatingApp.API.Data;
using Microsoft.EntityFrameworkCore;
using DatingApp.API.Models;
using DatingApp.API.DTO;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
namespace DatingApp.API.Helpers
{
    public class PagedList<T> : List<T>
    {
     public int CurrentPage { get; set; }

     public int TotalPages { get; set; }

     public int PageSize { get; set; }   

     public int TotalCount { get; set; }

     public PagedList(List <T> items, int count, int pageNumber, int pageSize)
     {
         TotalCount = count;
         PageSize = pageSize;
         CurrentPage = pageNumber;
         TotalPages = (int)Math.Ceiling(count / (double)PageSize);
         this.AddRange(items);
     }

     public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
     {
         var count = await source.CountAsync();
         var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
         return new PagedList<T>(items, count, pageNumber, pageSize); 
     } 
    }
}