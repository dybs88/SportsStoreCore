using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SportsStore.DAL.Contexts;
using SportsStore.Domain;
using SportsStore.Models.DictionaryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Infrastructure.Start.SeedDatas
{
    public class DictionarySeddData
    {
        public static void PopulateDictionaries(IApplicationBuilder app)
        {
            DictionaryDbContext context = app.ApplicationServices.GetRequiredService<DictionaryDbContext>();

            if(!context.DocumentTypes.Any(dt => dt.Type == DocumentTypes.SalesOrder))
            {
                context.DocumentTypes.Add(new DocumentType { Code = "SportsStore.SalesOrder", Symbol = "ZS", Name = "Zamówienie sprzedaży", Type = DocumentTypes.SalesOrder });
            }
            
            if(!context.DocumentTypes.Any(dt => dt.Type == DocumentTypes.Receipt))
            {
                context.DocumentTypes.Add(new DocumentType { Code = "SportsStore.Receipt", Symbol = "PAR", Name = "Paragon", Type = DocumentTypes.Receipt });
            }

            if(!context.DocumentTypes.Any(dt => dt.Type == DocumentTypes.SalesInvoice))
            {
                context.DocumentTypes.Add(new DocumentType { Code = "SportsStore.SalesInvoice", Symbol = "FS", Name = "Faktura sprzedaży", Type = DocumentTypes.SalesInvoice });
            }

            context.SaveChanges();
        }
    }
}
