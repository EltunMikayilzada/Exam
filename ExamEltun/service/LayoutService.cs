using ExamEltun.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ExamEltun.service
{
    public class LayoutService
    {
        private readonly AppDbcontext _context;

        public LayoutService(AppDbcontext context)
        {
           _context = context;
        }
        public async Task<Dictionary<string,string>> GetConnectingAsync() 
        {
            var settings =await  _context.Settings.ToDictionaryAsync(s => s.Key, s => s.Value);
            return settings;
        }
    }
}
