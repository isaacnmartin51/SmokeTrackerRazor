using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SmokeTracker.Data;

namespace SmokeTracker.Pages;

public class LogGroup(DateOnly SmokedDate, int Count)
{
    public DateOnly SmokedDate { get; } = SmokedDate;
    public int Count { get; } = Count;
}


public class IndexModel(StContext context) : PageModel
{
    public IEnumerable<LogGroup> Logs { get; set; }
    [BindProperty]
    public Log CreateLog { get; set; }
    public async Task OnGetAsync()
    {
        Logs = await context.Logs.GroupBy(l => new DateOnly(l.DateSmoked.Year, l.DateSmoked.Month, l.DateSmoked.Day), l => l.Id ).Select(lg => new LogGroup(lg.Key, lg.Count())).ToListAsync();
    }
    
    public async Task OnPostAsync()
    {
        context.Add(CreateLog);
        await context.SaveChangesAsync();
        Logs = await context.Logs.GroupBy(l => new DateOnly(l.DateSmoked.Year, l.DateSmoked.Month, l.DateSmoked.Day), l => l.Id ).Select(lg => new LogGroup(lg.Key, lg.Count())).ToListAsync();
    }
}
