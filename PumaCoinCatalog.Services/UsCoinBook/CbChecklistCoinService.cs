﻿using PumaCoinCatalog.Data;
using PumaCoinCatalog.Models.UsaCoinBook.Checklists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PumaCoinCatalog.Services.UsCoinBook
{
    public class CbChecklistCoinService
    {
        private readonly DataContext _context;

        public CbChecklistCoinService()
        {
            _context = new DataContext();
        }

        public CbChecklistCoinService(DataContext context)
        {
            _context = context;
        }

        public IList<CbChecklistCoin> GetCoinsByChecklistId(int checklistId)
        {
            var coins = _context.CbChecklistCoins
                                .Include("Coin")
                                .Where(x => x.Checklist.Id == checklistId)
                                .ToList();
            return coins;
        }
    }
}
