﻿using PumaCoinCatalog.Data;
using PumaCoinCatalog.Models.UsaCoinBook.Checklists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PumaCoinCatalog.Services.UsCoinBook
{
    public class CbCollectionService
    {
        private readonly DataContext _context;

        public CbCollectionService()
        {
            _context = new DataContext();
        }

        public CbCollectionService(DataContext context)
        {
            _context = context;
        }

        public IList<CbCollection> GetAllCollections()
        {
            var data = _context.CbCollections.ToList();
            return data;
        }

        public CbCollection GetCollection(int id)
        {
            var data = _context.CbCollections.SingleOrDefault(x => x.Id == id);
            if (data == null) throw new Exception("Collection not found.");
            return data;
        }

        public CbCollection CreateCollection(string title)
        {
            title = title.Trim();

            // check for duplicate
            var exist = _context.CbCollections.FirstOrDefault(x => x.Title == title);
            if (exist != null) throw new Exception($"Collection with name {title} already exists.");

            // create the new collection
            var newCollection = new CbCollection { Title = title };
            _context.CbCollections.Add(newCollection);

            _context.SaveChanges();

            // query new collection and return it
            var collection = _context.CbCollections.Single(x => x.Title == title);
            return collection;
        }
    }
}