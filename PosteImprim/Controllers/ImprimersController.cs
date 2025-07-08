using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PosteImprim.Data;
using PosteImprim.Models;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PosteImprim.Controllers
{
    public class ImprimersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ImprimersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Imprimers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Imprimers.Include(i => i.Positions).AsNoTracking().ToListAsync());
        }

        // GET: Imprimers/Create
        public IActionResult Create()
        {
            var model = new Imprimer(); // Initialiser le modèle ici
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAndPrint(string[] TextEntries)
        {
            if (TextEntries == null || TextEntries.Length == 0)
            {
                ModelState.AddModelError("", "Au moins un texte est requis.");
                return View(new Imprimer()); // Retourne un nouveau modèle vide
            }

            var imprimer = new Imprimer
            {
                DateImpression = DateTime.Now,
                Positions = new List<Position>()
            };

            foreach (var text in TextEntries)
            {
                if (string.IsNullOrWhiteSpace(text))
                {
                    ModelState.AddModelError("", "Le champ de texte ne peut pas être vide.");
                    return View(imprimer);
                }

                var textParts = text.Split('|');
                if (textParts.Length >= 4)
                {
                    if (int.TryParse(textParts[textParts.Length - 2].Trim(), out int x) &&
                        int.TryParse(textParts[textParts.Length - 1].Trim(), out int y))
                    {
                        imprimer.Positions.Add(new Position { X = x, Y = y, Text = text, ImprimerId = imprimer.Id });
                    }
                    else
                    {
                        ModelState.AddModelError("", "Format de texte invalide.");
                        return View(imprimer);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Format de texte invalide.");
                    return View(imprimer);
                }
            }

            _context.Add(imprimer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Méthode pour exporter l'historique en Excel
        [HttpGet]
        public IActionResult ExportToExcel()
        {
            var imprimerList = _context.Imprimers.Include(i => i.Positions).ToList();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Imprimers");
                var currentRow = 1;

                // Ajouter les en-têtes
                worksheet.Cell(currentRow, 1).Value = "X";
                worksheet.Cell(currentRow, 2).Value = "Y";
                worksheet.Cell(currentRow, 3).Value = "Texte";
                worksheet.Cell(currentRow, 4).Value = "Date Impression";
                worksheet.Cell(currentRow, 5).Value = "ID Impression";

                // Ajouter les données
                foreach (var imprimer in imprimerList)
                {
                    foreach (var position in imprimer.Positions)
                    {
                        currentRow++;
                        worksheet.Cell(currentRow, 1).Value = position.X;
                        worksheet.Cell(currentRow, 2).Value = position.Y;
                        worksheet.Cell(currentRow, 3).Value = position.Text;
                        worksheet.Cell(currentRow, 4).Value = imprimer.DateImpression.ToString("yyyy-MM-dd HH:mm:ss"); // Format lisible pour DateTime
                        worksheet.Cell(currentRow, 5).Value = imprimer.Id.ToString(); // Convertir Guid en chaîne
                    }
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Imprimers.xlsx");
                }
            }
        }


        // GET: Imprimers/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imprimer = await _context.Imprimers
                .Include(i => i.Positions)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (imprimer == null)
            {
                return NotFound();
            }

            return View(imprimer);
        }

        // GET: Imprimers/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imprimer = await _context.Imprimers
                .Include(i => i.Positions)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (imprimer == null)
            {
                return NotFound();
            }

            return View(imprimer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var imprimer = await _context.Imprimers.Include(i => i.Positions).FirstOrDefaultAsync(m => m.Id == id);
            if (imprimer != null)
            {
                foreach (var position in imprimer.Positions.ToList())
                {
                    _context.Positions.Remove(position);
                }

                _context.Imprimers.Remove(imprimer);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ImprimerExists(Guid id)
        {
            return _context.Imprimers.Any(e => e.Id == id);
        }
    }
}
