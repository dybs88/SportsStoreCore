﻿using Microsoft.AspNetCore.Builder;
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

            if (!context.DocumentTypes.Any(dt => dt.DocumentKind == DocumentKind.SalesOrder))
            {
                context.DocumentTypes.Add(new DocumentType { Code = "SportsStore.SalesOrder", Symbol = "ZS", Name = "Zamówienie sprzedaży", DocumentKind = DocumentKind.SalesOrder });
            }

            if (!context.DocumentTypes.Any(dt => dt.DocumentKind == DocumentKind.Receipt))
            {
                context.DocumentTypes.Add(new DocumentType { Code = "SportsStore.Receipt", Symbol = "PAR", Name = "Paragon", DocumentKind = DocumentKind.Receipt });
            }

            if (!context.DocumentTypes.Any(dt => dt.DocumentKind == DocumentKind.SalesInvoice))
            {
                context.DocumentTypes.Add(new DocumentType { Code = "SportsStore.SalesInvoice", Symbol = "FS", Name = "Faktura sprzedaży", DocumentKind = DocumentKind.SalesInvoice });
            }

            context.SaveChanges();
        }
    }
}
